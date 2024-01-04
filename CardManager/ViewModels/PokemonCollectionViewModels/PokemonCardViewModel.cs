using CardManager.Models.Cards.PokemonCards;
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
}

public class PokemonCardViewModel : BaseViewModel, IPokemonCardViewModel
{
    private readonly IViewModelsFactory viewModelsFactory;
    private readonly IPokemonCard pokemonCardModel;
    public delegate void RowDataChangedHandler();

    public event RowDataChangedHandler? RowDataChanged;

    public PokemonCardViewModel(IViewModelsFactory viewModelsFactory, IPokemonCard cardModel)
    {
        this.viewModelsFactory = viewModelsFactory;
        this.pokemonCardModel = cardModel;

        this.StorageLocation = cardModel.StorageSpec.Location.Type switch
        {
            StorageLocationType.Box => new BoxLocationViewModel((IBoxLocation)cardModel.StorageSpec.Location),
            StorageLocationType.Sleeve => new SleeveLocationViewModel((ISleeveLocation)cardModel.StorageSpec.Location),
            _ => new NoLocationViewModel((NoLocation)cardModel.StorageSpec.Location),
        };

        this.StorageMedia = new StorageMediaViewModel(cardModel.StorageSpec.Media);
        this.MonetaryData = new MonetaryAggregateViewModel(viewModelsFactory, cardModel.Monetary.Mavin);
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

    public IGradingAggregateViewModel Grading { get; set; } = new GradingAggregateViewModel();

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
        this.pokemonCardModel.Id = this.Id;
        this.pokemonCardModel.Name = this.Name;
        this.pokemonCardModel.Number = this.Number;
        this.pokemonCardModel.StorageSpec = new StorageSpecification(
            this.StorageMedia.ToModel(),
            this.StorageLocation.ToModel());
        this.pokemonCardModel.Rarity = this.Rarity.SelectedValue;
        this.pokemonCardModel.Series = this.Series.SelectedValue;
        this.pokemonCardModel.Grade = this.Grading.ToModel();
        this.pokemonCardModel.CreationYear = this.CreationYear;
        this.pokemonCardModel.Holographic = this.Holographic.SelectedValue;
        this.pokemonCardModel.Type = this.Type.SelectedValue;
        this.pokemonCardModel.RetrieveAppraisal().ConfigureAwait(false);
    }

    public void Dispose()
    {
        this.pokemonCardModel.AppraisalReceived -= this.PokemonCardModelAppraisalReceived;
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
}
