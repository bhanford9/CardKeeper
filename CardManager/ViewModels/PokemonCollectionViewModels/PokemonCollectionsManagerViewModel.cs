using static CardManager.ViewModels.PokemonCollectionViewModels.PokemonCollectionsManagerViewModelEvents;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public static class PokemonCollectionsManagerViewModelEvents
{
    public delegate void NewCollectionRequestHandler();
}

public interface IPokemonCollectionsManagerViewModel : IViewModel
{
    event NewCollectionRequestHandler? NewCollectionSubmitted;

    Dictionary<string, IPokemonCollectionViewModel> CustomCollections { get; set; }
    IPokemonCollectionViewModel FullCollection { get; set; }
    IPokemonCollectionViewModel CurrentCustom { get; set; }
    string NewCollectionName { get; set; }
    bool CollectionNameUnavailable { get; set; }
    string CollectionNameError { get; }

    void CreateCollection();
    void DisplayCollection(string collectionName);
    void OnCollectionModalHidden();
}

public class PokemonCollectionsManagerViewModel
    : BaseViewModel, IPokemonCollectionsManagerViewModel
{
    private readonly IViewModelsFactory viewModelsFactory;

    public PokemonCollectionsManagerViewModel(
    IViewModelsFactory viewModelsFactory,
    IPokemonCollectionViewModel fullCollection)
    {
        this.viewModelsFactory = viewModelsFactory;
        this.FullCollection = fullCollection;
        this.FullCollection.LoadFromFile();
    }

    public event NewCollectionRequestHandler? NewCollectionSubmitted;

    public IPokemonCollectionViewModel FullCollection { get; set; }

    public IPokemonCollectionViewModel CurrentCustom { get; set; } = default!;

    public string NewCollectionName { get; set; } = string.Empty;

    public bool CollectionNameUnavailable { get; set; } = false;

    public string CollectionNameError { get; private set; } = string.Empty;

    public Dictionary<string, IPokemonCollectionViewModel> CustomCollections { get; set; } = new();

    public void DisplayCollection(string collectionName)
    {
        this.CurrentCustom = this.CustomCollections[collectionName];
    }

    public void OnCollectionModalHidden()
    {
        this.NewCollectionName = string.Empty;
        this.CollectionNameUnavailable = false;
    }

    public void CreateCollection()
    {
        if (this.CustomCollections.ContainsKey(this.NewCollectionName))
        {
            this.CollectionNameUnavailable = true;
            this.CollectionNameError = "Collection with name already exists";
            return;
        }
        else if (string.IsNullOrEmpty(this.NewCollectionName))
        {
            this.CollectionNameUnavailable = true;
            this.CollectionNameError = "Collection name cannot be empty";
            return;
        }

        this.CollectionNameUnavailable = false;
        this.CollectionNameError = string.Empty;
        this.CurrentCustom = this.viewModelsFactory.NewPokemonCollection();
        this.CustomCollections.Add(this.NewCollectionName, this.CurrentCustom);
        this.FullCollection.CustomCollections = this.CustomCollections;
        this.CurrentCustom.CustomCollections = this.CustomCollections;
        this.NewCollectionSubmitted?.Invoke();
    }
}
