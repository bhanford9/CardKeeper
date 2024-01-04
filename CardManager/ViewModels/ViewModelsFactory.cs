using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.MonetaryData;
using CardManager.Models.StorageSpecifications;
using CardManager.Services;
using CardManager.ViewModels.ModalViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;
using CardManager.ViewModels.StorageSpecViewModels;

namespace CardManager.ViewModels;

public class ViewModelsFactory(
    IWebScrapingService webScrapingService,
    IStorageSpecFactory storageSpecFactory)
    : IViewModelsFactory
{
    private readonly IWebScrapingService webScrapingService = webScrapingService;
    private readonly IStorageSpecFactory storageSpecFactory = storageSpecFactory;

    public IEditCardModalViewModel NewEditCardModal(Func<Task> onSubmit, Func<Task> onCancel)
        => new EditCardModalViewModel(onSubmit, onCancel);

    public IEditCardModalViewModel DefaultEditCardModal()
        => new EditCardModalViewModel(() => { return Task.CompletedTask; }, () => { return Task.CompletedTask; });

    public IPokemonCardViewModel NewPokemonCard()
        => new PokemonCardViewModel(this, new PokemonCard(this.webScrapingService));

    public IMavinMonetaryViewModel NewMavinMonetary(IMavinMonetaryData model)
        => new MavinMonetaryViewModel(model);

    public ISleeveLocationViewModel NewSleeveLocation()
        => new SleeveLocationViewModel(this.storageSpecFactory.NewSleeveLocation());

    public IBoxLocationViewModel NewBoxLocation()
        => new BoxLocationViewModel(this.storageSpecFactory.NewBoxLocation());

    public NoLocationViewModel NewNoLocation()
        => new NoLocationViewModel(this.storageSpecFactory.NewNoLocation());

    public IStorageMediaViewModel NewBoxStorage()
        => new StorageMediaViewModel(this.storageSpecFactory.NewBox());
    
    public IStorageMediaViewModel NewBinderStorage()
        => new StorageMediaViewModel(this.storageSpecFactory.NewBinder());

    public IStorageMediaViewModel NewNoStorage()
        => new StorageMediaViewModel(this.storageSpecFactory.NewNoStorage());
}
