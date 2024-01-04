using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.Grading.BeckettGrading;
using CardManager.Models.Grading.CgcGrading;
using CardManager.Models.Grading.PsaGrading;
using CardManager.Models.StorageSpecifications;
using CardManager.Models.StorageSpecifications.Location;
using CardManager.Models.StorageSpecifications.Media;
using CardManager.ViewModels.GradingViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.StorageSpecViewModels;
using CardManager.ViewModels.UtilityViewModels;
using static CardManager.ViewModels.PokemonCollectionViewModels.PokemonCardViewModel;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public interface IPokemonCardViewModel : IViewModel, IDisposable
{
    event RowDataChangedHandler? RowDataChanged;

    int CreationYear { get; set; }
    Guid Id { get; set; }
    string Name { get; set; }
    string Number { get; set; }

    IEnumSelectorViewModel<StorageMediaType> MediaType { get; set; }

    IEnumSelectorViewModel<StorageLocationType> LocationType { get; set; }
    IEnumSelectorViewModel<PokemonHolographic> Holographic { get; set; }
    IEnumSelectorViewModel<PokemonRarity> Rarity { get; set; }
    IEnumSelectorViewModel<PokemonSeries> Series { get; set; }
    IStorageLocationViewModel StorageLocation { get; }
    IStorageMediaViewModel StorageMedia { get; }
    IEnumSelectorViewModel<ElementType> Type { get; set; }
    IGradingAggregateViewModel Grading { get; set; }
    bool IsSelected { get; set; }
    IMonetaryAggregateViewModel MonetaryData { get; set; }

    void RetrieveAppraisal();
    IPokemonCard ToModel();
}

public class PokemonCardViewModel : BaseViewModel, IPokemonCardViewModel
{
    private readonly IViewModelsFactory viewModelsFactory;
    private readonly IStorageSpecFactory storageSpecFactory;
    private readonly IPokemonCard pokemonCardModel;
    public delegate void RowDataChangedHandler();

    public event RowDataChangedHandler? RowDataChanged;

    public PokemonCardViewModel(
        IViewModelsFactory viewModelsFactory,
        IStorageSpecFactory storageSpecFactory,
        IPokemonCard cardModel,
        IGradingAggregateViewModel gradingAggregate)
    {
        this.viewModelsFactory = viewModelsFactory;
        this.storageSpecFactory = storageSpecFactory;
        this.pokemonCardModel = cardModel;
        this.Grading = gradingAggregate;

        this.Id = cardModel.Id;
        this.Name = cardModel.Name;
        this.Number = cardModel.Number;
        this.CreationYear = cardModel.CreationYear;
        this.Rarity.SelectedValue = cardModel.Rarity;
        this.Series.SelectedValue = cardModel.Series;
        this.Holographic.SelectedValue = cardModel.Holographic;
        this.LocationType.SelectedValue = cardModel.StorageSpec.Location.Type;
        this.MediaType.SelectedValue = cardModel.StorageSpec.Media.Type;
        this.Type.SelectedValue = cardModel.Type;

        switch (cardModel.Grade)
        {
            case BeckettGrade b:
                this.Grading.SelectedGradingHost = Models.Grading.GradingHost.Beckett;
                this.Grading.BeckettGrading = this.viewModelsFactory.NewBeckettGrading(b);
                break;
            case CgcGrade cgc:
                this.Grading.SelectedGradingHost = Models.Grading.GradingHost.Cgc;
                this.Grading.CgcGrading = this.viewModelsFactory.NewCgcGrading(cgc);
                break;
            case PsaGrade psa:
                this.Grading.SelectedGradingHost = Models.Grading.GradingHost.Psa;
                this.Grading.PsaGrading = this.viewModelsFactory.NewPsaGrading(psa);
                break;
        }

        this.StorageLocation = cardModel.StorageSpec.Location.Type switch
        {
            StorageLocationType.Box => this.viewModelsFactory.NewBoxLocation((IBoxLocation)cardModel.StorageSpec.Location),
            StorageLocationType.Sleeve => this.viewModelsFactory.NewSleeveLocation((ISleeveLocation)cardModel.StorageSpec.Location),
            _ => this.viewModelsFactory.NewNoLocation((NoLocation)cardModel.StorageSpec.Location),
        };

        this.StorageMedia = cardModel.StorageSpec.Media.Type switch
        {
            StorageMediaType.Box => this.viewModelsFactory.NewBoxStorage((IBox)cardModel.StorageSpec.Media),
            StorageMediaType.Binder => this.viewModelsFactory.NewBinderStorage((IBinder)cardModel.StorageSpec.Media),
            _ => this.viewModelsFactory.NewNoStorage((NoStorageMedia)cardModel.StorageSpec.Media),
        };
        
        this.MonetaryData = this.viewModelsFactory.NewMonetaryAggregate(cardModel.Monetary.Mavin);
        this.pokemonCardModel.AppraisalReceived += this.PokemonCardModelAppraisalReceived;
        this.MediaType.ValueSelected += this.MediaTypeValueSelected;
        this.LocationType.ValueSelected += this.LocationTypeValueSelected;
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public int CreationYear { get; set; } = 9999;

    public bool IsSelected { get; set; } = false;

    public IStorageLocationViewModel StorageLocation { get; private set; }

    public IStorageMediaViewModel StorageMedia { get; private set; }

    public IGradingAggregateViewModel Grading { get; set; }

    public IMonetaryAggregateViewModel MonetaryData { get; set; }

    public IEnumSelectorViewModel<StorageMediaType> MediaType { get; set; }
        = new EnumSelectorViewModel<StorageMediaType>();

    public IEnumSelectorViewModel<StorageLocationType> LocationType { get; set; }
        = new EnumSelectorViewModel<StorageLocationType>();

    public IEnumSelectorViewModel<PokemonSeries> Series { get; set; }
        = new EnumSelectorViewModel<PokemonSeries>();

    public IEnumSelectorViewModel<PokemonRarity> Rarity { get; set; }
        = new EnumSelectorViewModel<PokemonRarity>();

    public IEnumSelectorViewModel<PokemonHolographic> Holographic { get; set; }
        = new EnumSelectorViewModel<PokemonHolographic>();

    public IEnumSelectorViewModel<ElementType> Type { get; set; }
        = new EnumSelectorViewModel<ElementType>();

    public void RetrieveAppraisal()
    {
        this.StoreToModel();
        this.pokemonCardModel.RetrieveAppraisal().ConfigureAwait(false);
    }

    public void Dispose()
    {
        this.pokemonCardModel.AppraisalReceived -= this.PokemonCardModelAppraisalReceived;
    }

    public IPokemonCard ToModel()
    {
        this.StoreToModel();
        return this.pokemonCardModel;
    }

    private void PokemonCardModelAppraisalReceived()
    {
        this.MonetaryData.MavinViewModel =
            this.viewModelsFactory.NewMavinMonetary(this.pokemonCardModel.Monetary.Mavin);

        this.RowDataChanged?.Invoke();
    }

    private void LocationTypeValueSelected()
    {
        this.StorageLocation = this.LocationType.SelectedValue switch
        {
            StorageLocationType.Sleeve => this.viewModelsFactory.NewSleeveLocation(),
            StorageLocationType.Box => this.viewModelsFactory.NewBoxLocation(),
            _ => this.viewModelsFactory.NewNoLocation(),
        };

        this.NotifyPropertyChanged(nameof(this.StorageLocation));
    }

    private void MediaTypeValueSelected()
    {
        this.StorageMedia = this.MediaType.SelectedValue switch
        {
            StorageMediaType.Box => this.viewModelsFactory.NewBoxStorage(),
            StorageMediaType.Binder => this.viewModelsFactory.NewBinderStorage(),
            _ => this.viewModelsFactory.NewNoStorage(),
        };

        this.NotifyPropertyChanged(nameof(this.StorageMedia));
    }

    private void StoreToModel()
    {
        this.pokemonCardModel.Id = this.Id;
        this.pokemonCardModel.Name = this.Name;
        this.pokemonCardModel.Number = this.Number;
        this.pokemonCardModel.CreationYear = this.CreationYear;
        this.pokemonCardModel.StorageSpec = this.storageSpecFactory.NewStorageSpec(
            this.StorageMedia.ToModel(),
            this.StorageLocation.ToModel());
        this.pokemonCardModel.Rarity = this.Rarity.SelectedValue;
        this.pokemonCardModel.Series = this.Series.SelectedValue;
        this.pokemonCardModel.Grade = this.Grading.ToModel();
        this.pokemonCardModel.Holographic = this.Holographic.SelectedValue;
        this.pokemonCardModel.Type = this.Type.SelectedValue;
    }
}
