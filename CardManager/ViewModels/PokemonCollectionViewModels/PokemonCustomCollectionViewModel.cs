using CardManager.Models.CardCollections;
using static CardManager.ViewModels.PokemonCollectionViewModels.PokemonCustomCollectionViewModelEvents;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public static class PokemonCustomCollectionViewModelEvents
{
    public delegate void NewCollectionRequestHandler();
    public delegate void CollectionSelectedHandler(string collectionName);
}

public interface IPokemonCustomCollectionViewModel : IPokemonCollectionViewModel
{
    event NewCollectionRequestHandler? NewCollectionSubmitted;
    event CollectionSelectedHandler? CollectionSelected;

    string NewCollectionName { get; set; }
    bool CollectionNameUnavailable { get; set; }
    string CollectionNameError { get; }

    void CreateCollection();
    void DisplayCollection(string collectionName);
    void OnCollectionModalHidden();
}

public class PokemonCustomCollectionViewModel : PokemonCollectionViewModel, IPokemonCustomCollectionViewModel
{
    public event NewCollectionRequestHandler? NewCollectionSubmitted;
    public event CollectionSelectedHandler? CollectionSelected;

    public PokemonCustomCollectionViewModel(
        IViewModelsFactory viewModelsFactory,
        IPokemonCardCollection pokemonCardCollection)
        : base(viewModelsFactory, pokemonCardCollection)
    {
    }

    public string NewCollectionName { get; set; } = string.Empty;

    public bool CollectionNameUnavailable { get; set; } = false;

    public string CollectionNameError { get; private set; } = string.Empty;

    public override bool CanCreateCards { get; } = false;

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
        this.NewCollectionSubmitted?.Invoke();
    }

    public void DisplayCollection(string collectionName) => this.CollectionSelected?.Invoke(collectionName);
}
