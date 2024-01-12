using CardManager.Models.CardCollections;
using static CardManager.ViewModels.PokemonCollectionViewModels.PokemonCustomCollectionViewModelEvents;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public static class PokemonCustomCollectionViewModelEvents
{
    public delegate Task NewCollectionRequestHandler();
    public delegate Task CollectionSelectedHandler(string collectionName);
}

public interface IPokemonCustomCollectionViewModel : IViewModel
{
    event NewCollectionRequestHandler? NewCollectionSubmitted;
    event CollectionSelectedHandler? CollectionSelected;

    string NewCollectionName { get; set; }
    bool CollectionNameUnavailable { get; set; }
    string CollectionNameError { get; }

    IPokemonCollectionViewModel SelectedCollection { get; set; }
    Dictionary<string, IPokemonCollectionViewModel> CustomCollections { get; set; }
    ICollectionActionPermissionsViewModel CollectionPermissions { get; set; }

    Task CreateCollection();
    void DisplayCollection(string collectionName);
    void OnCollectionModalHidden();
    void LoadFromFile();
}

public class PokemonCustomCollectionViewModel : BaseViewModel, IPokemonCustomCollectionViewModel
{
    private readonly IViewModelsFactory viewModelsFactory;
    private readonly IPokemonCardCollection pokemonCards;
    private IPokemonCollectionViewModel selectedCollection;

    public event NewCollectionRequestHandler? NewCollectionSubmitted;
    public event CollectionSelectedHandler? CollectionSelected;

    public PokemonCustomCollectionViewModel(
        IViewModelsFactory viewModelsFactory,
        IPokemonCardCollection pokemonCardCollection,
        IPokemonCollectionViewModel selectedCollection,
        ICustomCollectionActionPermissionsViewModel permissions)
    {
        this.viewModelsFactory = viewModelsFactory;
        this.pokemonCards = pokemonCardCollection;
        this.CollectionPermissions = permissions;
        this.selectedCollection = selectedCollection;
        this.SelectedCollection.CollectionPermissions = this.CollectionPermissions;
        this.pokemonCards.CustomCollectionsChanged += this.PokemonCardsCustomCollectionsChanged;
        this.pokemonCards.CustomCollectionAdded += this.PokemonCardsCustomCollectionAdded;
        this.pokemonCards.CustomCollectionUpdated += this.PokemonCardsCustomCollectionUpdated;
    }

    public string NewCollectionName { get; set; } = string.Empty;

    public bool CollectionNameUnavailable { get; set; } = false;

    public string CollectionNameError { get; private set; } = string.Empty;

    public IPokemonCollectionViewModel SelectedCollection
    {
        get => this.selectedCollection;
        set
        {
            this.selectedCollection = value;
            this.selectedCollection.CollectionPermissions = this.CollectionPermissions;
        }
    }

    public ICollectionActionPermissionsViewModel CollectionPermissions { get; set; }

    public Dictionary<string, IPokemonCollectionViewModel> CustomCollections { get; set; } = [];

    public void OnCollectionModalHidden()
    {
        this.NewCollectionName = string.Empty;
        this.CollectionNameUnavailable = false;
    }

    public async Task CreateCollection()
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

        this.pokemonCards.AddCustomCollection(this.NewCollectionName);

        var newCollection = this.viewModelsFactory.NewPokemonCollection(this.pokemonCards);
        newCollection.CollectionName = this.NewCollectionName;
            
        this.SelectedCollection = this.CustomCollections[this.NewCollectionName];

        if (this.NewCollectionSubmitted != default)
        {
            await this.NewCollectionSubmitted.Invoke();
        }
    }

    public void DisplayCollection(string collectionName)
    {
        this.SelectedCollection = this.CustomCollections[collectionName];
        this.CollectionSelected?.Invoke(collectionName);
    }

    public void LoadFromFile()
    {
        this.pokemonCards.LoadCustomCollections();

        this.CustomCollections = this.pokemonCards.CustomCollections
            .ToDictionary(
                collection => collection.Key,
                collection =>
                {
                    var cards = collection.Value
                        .Select(id => this.pokemonCards.Cards.First(card => card.Id.Equals(id)))
                        .Select(card => this.viewModelsFactory.NewPokemonCard(card))
                        .ToList();
                    var customCollection = this.viewModelsFactory.NewPokemonCollection();
                    customCollection.Cards = cards;
                    customCollection.CollectionName = collection.Key;
                    customCollection.PokemonCollectionViewModelRowDataChanged();
                    return customCollection;
                });
    }

    private void PokemonCardsCustomCollectionsChanged()
    {
        this.CustomCollections = this.pokemonCards.CustomCollections
            .ToDictionary(
                collection => collection.Key,
                collection => this.CreateCollection(collection.Key, collection.Value));
    }

    private void PokemonCardsCustomCollectionAdded(string name)
    {
        this.CustomCollections[name] = this.CreateCollection(name, this.pokemonCards.CustomCollections[name]);
        this.CustomCollections[name].PokemonCollectionViewModelRowDataChanged();
    }

    private void PokemonCardsCustomCollectionUpdated(string name, IEnumerable<Guid> cardIds)
    {
        var cards = cardIds
            .Select(id => this.pokemonCards.Cards.First(c => c.Id.Equals(id)))
            .Select(this.viewModelsFactory.NewPokemonCard)
            .ToList();
        this.CustomCollections[name].Cards.AddRange(cards);
        this.CustomCollections[name].PokemonCollectionViewModelRowDataChanged();
    }

    private IPokemonCollectionViewModel CreateCollection(string name, IEnumerable<Guid> cardIds)
    {
        var cards = cardIds
            .Select(id => this.pokemonCards.Cards.First(c => c.Id.Equals(id)))
            .Select(this.viewModelsFactory.NewPokemonCard)
            .ToList();

        var collection = this.viewModelsFactory.NewPokemonCollection();
        collection.CollectionName = name;
        collection.Cards = cards;
        collection.PokemonCollectionViewModelRowDataChanged();

        return collection;
    }
}
