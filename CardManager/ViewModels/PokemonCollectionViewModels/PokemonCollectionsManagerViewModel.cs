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
    private readonly IViewModelsFactory viewModelsFactory;
    private IPokemonCustomCollectionViewModel currentCustom = default!;
    private const string DEFAULT_COLLECTION_NAME = "No Collection Selected";

    public event NewCollectionCreatedHandler? NewCollectionCreated;

    public PokemonCollectionsManagerViewModel(
        IViewModelsFactory viewModelsFactory,
        IPokemonCollectionViewModel fullCollection)
    {
        this.viewModelsFactory = viewModelsFactory;
        this.CurrentCustom = this.viewModelsFactory.NewCustomPokemonCollection();
        this.currentCustom.CollectionName = DEFAULT_COLLECTION_NAME;
        this.FullCollection = fullCollection;
        this.FullCollection.CustomCollectionsUpdated += this.FullCollectionCustomCollectionsUpdated;
        this.FullCollection.LoadFromFile();
    }

    public void Dispose()
    {
        this.FullCollection.CustomCollectionsUpdated -= this.FullCollectionCustomCollectionsUpdated;
    }

    public IPokemonCollectionViewModel FullCollection { get; set; }

    public IPokemonCustomCollectionViewModel CurrentCustom
    {
        get => this.currentCustom;
        set
        {
            if (this.currentCustom != default)
            {
                this.currentCustom.NewCollectionSubmitted -= this.CreateNewCollection;
                this.currentCustom.CollectionSelected -= this.DisplayCollection;
            }

            this.currentCustom = value;
            this.currentCustom.NewCollectionSubmitted += this.CreateNewCollection;
            this.currentCustom.CollectionSelected += this.DisplayCollection;
        }
    }

    public Dictionary<string, IPokemonCustomCollectionViewModel> CustomCollections { get; set; } = new();

    private void DisplayCollection(string collectionName)
    {
        this.CurrentCustom = this.CustomCollections[collectionName];
    }

    private void CreateNewCollection()
    {
        string newName = this.CurrentCustom.NewCollectionName;
        this.CurrentCustom = this.viewModelsFactory.NewCustomPokemonCollection();
        this.CurrentCustom.CollectionName = newName;
        this.CustomCollections.Add(newName, this.CurrentCustom);
        this.CustomCollections.ToList().ForEach(kvp => kvp.Value.CustomCollections = this.CustomCollections);
        this.FullCollection.CustomCollections = this.CustomCollections;
        this.CurrentCustom.CustomCollections = this.CustomCollections;

        this.NewCollectionCreated?.Invoke();
    }

    private void FullCollectionCustomCollectionsUpdated()
    {
        if (this.CurrentCustom == default || this.CurrentCustom.CollectionName.Equals(DEFAULT_COLLECTION_NAME))
        {
            this.CustomCollections = this.FullCollection.CustomCollections;
            this.CurrentCustom = this.CustomCollections.Values.FirstOrDefault()! ?? this.CurrentCustom!;
            this.CurrentCustom.CustomCollections = this.CustomCollections;
            this.NewCollectionCreated?.Invoke();
        }
    }
}
