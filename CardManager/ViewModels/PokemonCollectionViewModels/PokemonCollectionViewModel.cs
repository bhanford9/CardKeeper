using BlazorBootstrap;
using CardManager.Models.CardCollections;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering;
using CardManager.ViewModels.UtilityViewModels.Filtering;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
using static CardManager.ViewModels.PokemonCollectionViewModels.CardCollectionEvents;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public class CardCollectionEvents
{
    public delegate Task EditCardPressedHandler(IPokemonCardViewModel card);
    public delegate Task GridDataChangedHandler();
    public delegate Task DeleteCardHandler(IPokemonCardViewModel card);
    public delegate Task RemoveCardHandler(IPokemonCardViewModel card);
    public delegate Task CustomCollectionsUpdatedHandler();
    public delegate Task AddFilterHandler();
}

public interface IPokemonCollectionViewModel : IViewModel, IDisposable
{
    List<IPokemonCardViewModel> Cards { get; set; }
    double AverageMin { get; set; }
    double AverageAverage { get; set; }
    double AverageMax { get; set; }
    double TotalMin { get; set; }
    double TotalAverage { get; set; }
    double TotalMax { get; set; }
    bool CollectionsVisible { get; set; }
    string CollectionName { get; set; }
    List<string> CollectionNames { get; set; }
    ICollectionActionPermissionsViewModel CollectionPermissions { get; set; }
    IAddFilterViewModel AddFilterViewModel { get; set; }
    List<IPokemonCardViewModel> FilteredCards { get; set; }
    List<StoredFilter<IPokemonCardViewModel>> Filters { get; set; }

    event EditCardPressedHandler? EditCardPressed;
    event GridDataChangedHandler? GridDataChanged;
    event DeleteCardHandler? DeleteCard;
    event RemoveCardHandler? RemoveCard;
    event CustomCollectionsUpdatedHandler? CustomCollectionsUpdated;
    event AddFilterHandler? AddFilter;

    Task AddCard(IPokemonCardViewModel? card = null);
    void SaveFullCollection();
    Task RetrieveAppraisals();
    Task DuplicateSelectedCard();
    Task DeleteSelectedCard();
    Task<GridDataProviderResult<IPokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<IPokemonCardViewModel> request);
    Task CardRowDoubleClicked(GridRowEventArgs<IPokemonCardViewModel> args);
    Task OnSelectedCardsChanged(HashSet<IPokemonCardViewModel> args);
    void ShowCollections(bool doShow);
    Task LoadFromFile();
    Task AddCards(IEnumerable<IPokemonCardViewModel> cards);
    Task PokemonCollectionViewModelRowDataChanged();
    Task OnGridDataChanged();
    void AddToCollection(string name);
    void SaveCustomCollection();
    Task RemoveFromCollection(Guid id);
    Task RemoveSelectedCard();
    void OnAddFilterClicked();

    void DoNothing() { }

    Task RemoveFilter(StoredFilter<IPokemonCardViewModel> filter);
}

public class PokemonCollectionViewModel
    : BaseViewModel, IPokemonCollectionViewModel
{
    private readonly IViewModelsFactory viewModelsFactory;
    private readonly IPokemonCardCollection pokemonCards;
    private readonly IFilterPackageBuilderRepository<IPokemonCardViewModel> filterRepo;
    private List<IPokemonCardViewModel> cards = [];

    private event GridDataChangedHandler? gridDataChanged;
    private event RemoveCardHandler? removeCard;

    public event EditCardPressedHandler? EditCardPressed;
    public event DeleteCardHandler? DeleteCard;
    public event GridDataChangedHandler? GridDataChanged
    {
        add
        {
            if (!this.gridDataChanged?.GetInvocationList().Contains(value) ?? true)
            {
                this.gridDataChanged += value;
            }
        }
        remove => this.gridDataChanged -= value;
    }
    public event RemoveCardHandler? RemoveCard
    {
        add
        {
            if (!this.removeCard?.GetInvocationList().Contains(value) ?? true)
            {
                this.removeCard += value;
            }
        }
        remove => this.removeCard -= value;
    }
    public event CustomCollectionsUpdatedHandler? CustomCollectionsUpdated;
    public event AddFilterHandler? AddFilter;

    public PokemonCollectionViewModel(
        IViewModelsFactory viewModelsFactory,
        IPokemonCardCollection pokemonCardCollection,
        IFullCollectionActionPermissionsViewModel permissions,
        IAddFilterViewModel addFilterViewModel,
        IFilterPackageBuilderRepository<IPokemonCardViewModel> filterRepo)
    {
        this.viewModelsFactory = viewModelsFactory;
        this.pokemonCards = pokemonCardCollection;
        this.filterRepo = filterRepo;
        this.CollectionPermissions = permissions;
        this.AddFilterViewModel = addFilterViewModel;
        this.pokemonCards.CustomCollectionAdded += this.PokemonCardsCustomCollectionsChanged;
        this.pokemonCards.CustomCollectionsChanged += this.PokemonCardsCustomCollectionsChanged;
        this.AddFilterViewModel.FilterApplied += this.AddFilterViewModelFilterApplied;
    }

    public List<string> CollectionNames { get; set; } = [];

    public List<IPokemonCardViewModel> Cards
    {
        get => this.cards;
        set
        {
            this.cards = value;
            this.FilteredCards = this.Filters.Count != 0
                ? this.Filters.SelectMany(f => f.Filter(this.cards)).ToList()
                : this.cards;
        }
    }

    public List<IPokemonCardViewModel> FilteredCards { get; set; } = [];

    public List<StoredFilter<IPokemonCardViewModel>> Filters { get; set; } = [];

    public string CollectionName { get; set; } = "All Pokemon Cards";

    public double AverageMin { get; set; }

    public double AverageAverage { get; set; }

    public double AverageMax { get; set; }

    public double TotalMin { get; set; }

    public double TotalAverage { get; set; }

    public double TotalMax { get; set; }

    public bool CollectionsVisible { get; set; }

    public ICollectionActionPermissionsViewModel CollectionPermissions { get; set; }

    public IAddFilterViewModel AddFilterViewModel { get; set; }

    public void Dispose()
    {
        this.Cards.ForEach(card => card.RowDataChanged -= this.PokemonCollectionViewModelRowDataChanged);
        // don't think this is necessary, but can't hurt
        this.FilteredCards.ForEach(card => card.RowDataChanged -= this.PokemonCollectionViewModelRowDataChanged);
        this.pokemonCards.CustomCollectionAdded -= this.PokemonCardsCustomCollectionsChanged;
    }

    public async Task<GridDataProviderResult<IPokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<IPokemonCardViewModel> request)
        => await Task.FromResult(request.ApplyTo(this.FilteredCards));

    public Task AddCard(IPokemonCardViewModel? card = null)
    {
        this.Cards.Add(card ?? this.viewModelsFactory.NewPokemonCard());
        this.Cards.Last().RowDataChanged += this.PokemonCollectionViewModelRowDataChanged;
        this.ApplyFilters();
        return this.PokemonCollectionViewModelRowDataChanged();
    }

    public Task AddCards(IEnumerable<IPokemonCardViewModel> cards)
    {
        foreach (var card in cards)
        {
            this.Cards.Add(card ?? this.viewModelsFactory.NewPokemonCard());
            this.Cards.Last().RowDataChanged += this.PokemonCollectionViewModelRowDataChanged;
        }

        this.ApplyFilters();

        return this.PokemonCollectionViewModelRowDataChanged();
    }

    public void SaveFullCollection()
    {
        this.pokemonCards.Cards = this.Cards.Select(x => x.ToModel()).ToList();
        this.pokemonCards.SaveFullCollection();
    }

    public void SaveCustomCollection()
    {
        this.pokemonCards.CustomCollections[this.CollectionName] = this.FilteredCards.Select(c => c.Id).ToList();
        this.pokemonCards.SaveCustomCollections();
    }

    public Task RetrieveAppraisals()
    {
        foreach (var card in this.Cards.Where(x => x.IsSelected))
        {
            card.RetrieveAppraisal();
        }

        return this.PokemonCollectionViewModelRowDataChanged();
    }

    public void ShowCollections(bool doShow)
    {
        this.CollectionsVisible = doShow;
    }

    public void AddToCollection(string name)
        => this.pokemonCards.AddToCustomCollection(name, this.FilteredCards.Where(x => x.IsSelected).Select(x => x.Id));

    public Task RemoveFromCollection(Guid id)
    {
        var toRemove = this.FilteredCards.First(c => c.Id.Equals(id));
        this.Cards.Remove(toRemove);
        this.ApplyFilters();
        this.pokemonCards.RemoveFromCustomCollection(this.CollectionName, id);
        return this.PokemonCollectionViewModelRowDataChanged();
    }

    public Task DuplicateSelectedCard()
    {
        var cardToDuplicate = this.FilteredCards.First(c => c.IsSelected);
        var duplicatedCard = this.viewModelsFactory.NewPokemonCard(cardToDuplicate.ToModel().DeepCopy());
        this.Cards.Add(duplicatedCard);
        this.ApplyFilters();
        return this.PokemonCollectionViewModelRowDataChanged();
    }

    public async Task DeleteSelectedCard()
    {
        if (this.DeleteCard != null)
        {
            await this.DeleteCard.Invoke(this.FilteredCards.First(c => c.IsSelected));
            await this.PokemonCollectionViewModelRowDataChanged();
        }
    }

    public async Task RemoveSelectedCard()
    {
        if (this.removeCard != null)
        {
            await this.removeCard.Invoke(this.FilteredCards.First(c => c.IsSelected));
            await this.PokemonCollectionViewModelRowDataChanged();
        }
    }

    public async Task CardRowDoubleClicked(GridRowEventArgs<IPokemonCardViewModel> args)
    {
        if (this.EditCardPressed != null)
        {
            await this.EditCardPressed.Invoke(args.Item);
        }
    }

    public Task OnSelectedCardsChanged(HashSet<IPokemonCardViewModel> args)
    {
        foreach (var card in this.FilteredCards)
        {
            card.IsSelected = args.Contains(card);
        }

        return Task.CompletedTask;
    }

    public Task LoadFromFile()
    {
        this.pokemonCards.LoadMasterCardList();
        this.Cards = this.pokemonCards.Cards.Select(this.viewModelsFactory.NewPokemonCard).ToList();

        return this.PokemonCollectionViewModelRowDataChanged();
    }

    public async Task PokemonCollectionViewModelRowDataChanged()
    {
        this.UpdateStats();
        await this.OnGridDataChanged();
    }

    public virtual Task OnGridDataChanged() => this.gridDataChanged?.Invoke() ?? Task.CompletedTask;

    public void OnAddFilterClicked() => this.AddFilterViewModel.IsHidden = false;

    public async Task RemoveFilter(StoredFilter<IPokemonCardViewModel> filter)
    {
        this.Filters.Remove(filter);
        this.ApplyFilters();
        await this.PokemonCollectionViewModelRowDataChanged();
    }

    private void UpdateStats()
    {
        this.AverageMin = Math.Round(this.FilteredCards.Average(c => c.MonetaryData.MavinViewModel.MinPrice), 2);
        this.AverageAverage = Math.Round(this.FilteredCards.Average(c => c.MonetaryData.MavinViewModel.AveragePrice), 2);
        this.AverageMax = Math.Round(this.FilteredCards.Average(c => c.MonetaryData.MavinViewModel.MaxPrice), 2);
        this.TotalMin = Math.Round(this.FilteredCards.Sum(c => c.MonetaryData.MavinViewModel.MinPrice), 2);
        this.TotalAverage = Math.Round(this.FilteredCards.Sum(c => c.MonetaryData.MavinViewModel.AveragePrice), 2);
        this.TotalMax = Math.Round(this.FilteredCards.Sum(c => c.MonetaryData.MavinViewModel.MaxPrice), 2);
    }

    private void PokemonCardsCustomCollectionsChanged(string name) =>
        this.CollectionNames = this.pokemonCards.CustomCollections.Keys.ToList();

    private void PokemonCardsCustomCollectionsChanged() =>
        this.CollectionNames = this.pokemonCards.CustomCollections.Keys.ToList();

    private async Task AddFilterViewModelFilterApplied()
    {
        IFilterEvaluationViewModel evaluation = this.AddFilterViewModel.SelectedFilterCriteria.SelectedEvaluation;
        IFilterPackage<IPokemonCardViewModel> filterPackage = this.filterRepo.GetFilterPackage(this.AddFilterViewModel);
        this.Filters.Add(new(evaluation.Passes, filterPackage));
        this.ApplyFilters();
        await this.PokemonCollectionViewModelRowDataChanged();
    }

    private void ApplyFilters() => this.FilteredCards = this.Filters
        .Aggregate(this.Cards.AsEnumerable(), (filteredCards, filter) => filter.Filter(filteredCards))
        .ToList();
}
