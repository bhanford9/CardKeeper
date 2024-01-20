using CardManager.Models.CardCollections;
using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.Grading.BeckettGrading;
using CardManager.Models.Grading.CgcGrading;
using CardManager.Models.Grading.PsaGrading;
using CardManager.Models.MonetaryData;
using CardManager.Models.StorageSpecifications;
using CardManager.Models.StorageSpecifications.Location;
using CardManager.Models.StorageSpecifications.Media;
using CardManager.ViewModels.GradingViewModels;
using CardManager.ViewModels.ModalViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;
using CardManager.ViewModels.StorageSpecViewModels;
using CardManager.ViewModels.UtilityViewModels.Filtering;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;

namespace CardManager.ViewModels;

public class ViewModelsFactory(
    IStorageSpecFactory storageSpecFactory,
    IServiceProvider serviceProvider)
    : IViewModelsFactory
{
    private readonly IStorageSpecFactory storageSpecFactory = storageSpecFactory;
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public IPokemonCollectionViewModel NewPokemonCollection(IPokemonCardCollection? collection = null)
        => collection == null
         ? this.GetService<IPokemonCollectionViewModel>()
         : new PokemonCollectionViewModel(
             this,
             collection,
             this.GetService<IFullCollectionActionPermissionsViewModel>(),
             this.GetService<IAddFilterViewModel>(),
             this.GetService<IFilterPackageBuilderRepository<IPokemonCardViewModel>>());

    public IPokemonCustomCollectionViewModel NewPokemonCustomCollection(
        IPokemonCardCollection? collection = null,
        IPokemonCollectionViewModel? selectedCollection = null)
        => collection == null
         ? this.GetService<IPokemonCustomCollectionViewModel>()
         : new PokemonCustomCollectionViewModel(
             this,
             collection,
             selectedCollection ?? this.NewPokemonCollection(),
             this.GetService<ICustomCollectionActionPermissionsViewModel>());

    public IEditCardModalViewModel NewEditCardModal(Func<Task> onSubmit, Func<Task> onCancel)
        => new EditCardModalViewModel(onSubmit, onCancel);

    public IEditCardModalViewModel DefaultEditCardModal()
        => new EditCardModalViewModel(() => { return Task.CompletedTask; }, () => { return Task.CompletedTask; });

    public IPokemonCardViewModel NewPokemonCard()
        => new PokemonCardViewModel(
            this,
            this.storageSpecFactory,
            this.GetService<IPokemonCard>()!,
            this.GetService<IGradingAggregateViewModel>()!);

    public IPokemonCardViewModel NewPokemonCard(IPokemonCard card)
        => new PokemonCardViewModel(
            this,
            this.storageSpecFactory,
            card,
            this.GetService<IGradingAggregateViewModel>()!);

    public IMavinMonetaryViewModel NewMavinMonetary(IMavinMonetaryData model)
        => new MavinMonetaryViewModel(model);

    public ISleeveLocationViewModel NewSleeveLocation(ISleeveLocation? sleeve = null)
        => sleeve == null
         ? this.GetService<ISleeveLocationViewModel>()
         : new SleeveLocationViewModel(sleeve);

    public IBoxLocationViewModel NewBoxLocation(IBoxLocation? box = null)
        => box == null
         ? this.GetService<IBoxLocationViewModel>()
         : new BoxLocationViewModel(box);

    public NoLocationViewModel NewNoLocation(NoLocation? none = null)
        => none == null 
         ? new NoLocationViewModel(this.storageSpecFactory.NewNoLocation())
         : new NoLocationViewModel(none);

    public IStorageMediaViewModel NewBoxStorage(IBox? box = null)
        => box == null
         ? new StorageMediaViewModel(this.storageSpecFactory.NewBox())
         : new StorageMediaViewModel(box);

    public IStorageMediaViewModel NewBinderStorage(IBinder? binder = null)
        => binder == null
         ? new StorageMediaViewModel(this.storageSpecFactory.NewBinder())
         : new StorageMediaViewModel(binder);

    public IStorageMediaViewModel NewNoStorage(NoStorageMedia? none = null)
        => none == null
         ? new StorageMediaViewModel(this.storageSpecFactory.NewNoStorage())
         : new StorageMediaViewModel(none);

    public IBeckettGradingViewModel NewBeckettGrading(IBeckettGrade model)
        => new BeckettGradingViewModel(model);
    public ICgcGradingViewModel NewCgcGrading(ICgcGrade model)
        => new CgcGradingViewModel(model);
    public IPsaGradingViewModel NewPsaGrading(IPsaGrade model)
        => new PsaGradingViewModel(model);

    public IMonetaryAggregateViewModel NewMonetaryAggregate(IMavinMonetaryData mavin)
        => new MonetaryAggregateViewModel(this, mavin);

    private T GetService<T>() where T : notnull => this.serviceProvider.GetRequiredService<T>()!;
}
