using BlazorBootstrap;
using CardManager.Models.CardCollections;
using static CardManager.ViewModels.PokemonCollectionViewModels.CardCollectionEvents;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public class CardCollectionEvents
{
    public delegate Task EditCardPressedHandler(IPokemonCardViewModel card);
    public delegate Task GridDataChangedHandler();
}

public interface IPokemonCollectionViewModel : IViewModel, IDisposable
{
    List<IPokemonCardViewModel> Cards { get; set; }

    event EditCardPressedHandler? EditCardPressed;
    event GridDataChangedHandler? GridDataChanged;

    void AddCard();
    void CardEditClicked(IPokemonCardViewModel card);
    void SaveTable();
    void RetrieveAppraisals();
    Task<GridDataProviderResult<IPokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<IPokemonCardViewModel> request);
    Task CardRowDoubleClicked(GridRowEventArgs<IPokemonCardViewModel> args);
    Task OnSelectedCardsChanged(HashSet<IPokemonCardViewModel> args);
}

public class PokemonCollectionViewModel(
    IViewModelsFactory viewModelsFactory,
    IPokemonCardCollection pokemonCardCollection)
    : BaseViewModel, IPokemonCollectionViewModel
{
    private readonly IViewModelsFactory viewModelsFactory = viewModelsFactory;
    private IPokemonCardCollection pokemonCards = pokemonCardCollection;

    public event EditCardPressedHandler? EditCardPressed;
    public event GridDataChangedHandler? GridDataChanged;

    public Grid<IPokemonCardViewModel> GridReference { get; set; } = default!;

    public List<IPokemonCardViewModel> Cards { get; set; } = pokemonCardCollection.Cards
        .Select(viewModelsFactory.NewPokemonCard)
        .ToList();

    public void Dispose()
    {
        this.Cards.ForEach(card => card.RowDataChanged -= this.PokemonCollectionViewModelRowDataChanged);
    }

    public async Task<GridDataProviderResult<IPokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<IPokemonCardViewModel> request)
        => await Task.FromResult(request.ApplyTo(this.Cards));

    public void AddCard()
    {
        this.Cards.Add(this.viewModelsFactory.NewPokemonCard());
        this.Cards.Last().RowDataChanged += this.PokemonCollectionViewModelRowDataChanged;
        this.GridDataChanged?.Invoke();
    }

    public void SaveTable()
    {
        this.pokemonCards.Cards = this.Cards.Select(x => x.ToModel()).ToList();
        this.pokemonCards.Save();
    }

    public void RetrieveAppraisals()
    {
        foreach (var card in this.Cards.Where(x => x.IsSelected))
        {
            card.RetrieveAppraisal();
        }
    }

    public void CardEditClicked(IPokemonCardViewModel card) => this.EditCardPressed?.Invoke(card);

    public async Task CardRowDoubleClicked(GridRowEventArgs<IPokemonCardViewModel> args)
    {
        if (this.EditCardPressed != null)
        {
            await this.EditCardPressed.Invoke(args.Item);
        }
    }

    public Task OnSelectedCardsChanged(HashSet<IPokemonCardViewModel> args)
    {
        foreach (var card in this.Cards)
        {
            card.IsSelected = args.Contains(card);
        }

        return Task.CompletedTask;
    }
    
    private void PokemonCollectionViewModelRowDataChanged() => this.GridDataChanged?.Invoke();
}
