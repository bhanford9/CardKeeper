using CardManager.Models.CardCollections;
using static CardManager.ViewModels.PokemonCollectionViewModels.PokemonCollectionsManagerViewModelEvents;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public static class PokemonCollectionsManagerViewModelEvents
{
    public delegate void NewCollectionCreatedHandler();
}

public interface IPokemonCollectionsManagerViewModel : IViewModel
{
    event NewCollectionCreatedHandler? NewCollectionCreated;
    Dictionary<string, IPokemonCustomCollectionViewModel> CustomCollections { get; set; }
    IPokemonCollectionViewModel FullCollection { get; set; }
    IPokemonCustomCollectionViewModel CurrentCustom { get; set; }
}

public class PokemonCollectionsManagerViewModel
    : BaseViewModel, IPokemonCollectionsManagerViewModel, IDisposable
{
    private const string DEFAULT_COLLECTION_NAME = "No Collection Selected";

    public event NewCollectionCreatedHandler? NewCollectionCreated;

    public PokemonCollectionsManagerViewModel(
        IViewModelsFactory viewModelsFactory,
        IPokemonCardCollection pokemonCardCollection)
    {
        this.FullCollection = viewModelsFactory.NewPokemonCollection(pokemonCardCollection);
        this.CurrentCustom = viewModelsFactory.NewPokemonCustomCollection(pokemonCardCollection);
        this.CurrentCustom.SelectedCollection.CollectionName = DEFAULT_COLLECTION_NAME;

        this.FullCollection.LoadFromFile();
        this.CurrentCustom.LoadFromFile();
    }

    public void Dispose()
    {
    }

    public IPokemonCollectionViewModel FullCollection { get; set; }

    public IPokemonCustomCollectionViewModel CurrentCustom { get; set; }

    public Dictionary<string, IPokemonCustomCollectionViewModel> CustomCollections { get; set; } = new();
}
