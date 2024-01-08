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
    : BaseViewModel, IPokemonCollectionsManagerViewModel
{
    private readonly IViewModelsFactory viewModelsFactory;
    private IPokemonCustomCollectionViewModel currentCustom = default!;

    public event NewCollectionCreatedHandler? NewCollectionCreated;

    public PokemonCollectionsManagerViewModel(
        IViewModelsFactory viewModelsFactory,
        IPokemonCollectionViewModel fullCollection)
    {
        this.viewModelsFactory = viewModelsFactory;
        this.FullCollection = fullCollection;
        this.FullCollection.LoadFromFile();
        this.CurrentCustom = this.viewModelsFactory.NewCustomPokemonCollection();
        this.currentCustom.CollectionName = "No Collection Selected";
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
        this.FullCollection.CustomCollections = this.CustomCollections;
        this.CurrentCustom.CustomCollections = this.CustomCollections;

        this.NewCollectionCreated?.Invoke();
    }
}
