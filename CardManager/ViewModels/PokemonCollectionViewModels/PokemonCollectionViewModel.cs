using BlazorBootstrap;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public interface IPokemonCollectionViewModel : IViewModel, IDisposable
{
    List<IPokemonCardViewModel> Cards { get; set; }

    event PokemonCollectionViewModel.EditCardPressedHandler? EditCardPressed;
    event PokemonCollectionViewModel.GridDataChangedHandler? GridDataChanged;

    void AddCard();
    void CardEditClicked(IPokemonCardViewModel card);
    void SaveTable();
    void RetrieveAppraisals();
    Task<GridDataProviderResult<IPokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<IPokemonCardViewModel> request);
    Task CardRowDoubleClicked(GridRowEventArgs<IPokemonCardViewModel> args);
    Task OnSelectedCardsChanged(HashSet<IPokemonCardViewModel> args);
}

public class PokemonCollectionViewModel(IViewModelsFactory viewModelsFactory)
    : BaseViewModel, IPokemonCollectionViewModel
{
    private readonly IViewModelsFactory viewModelsFactory = viewModelsFactory;

    public delegate Task EditCardPressedHandler(IPokemonCardViewModel card);
    public delegate Task GridDataChangedHandler();

    public event EditCardPressedHandler? EditCardPressed;
    public event GridDataChangedHandler? GridDataChanged;

    public Grid<IPokemonCardViewModel> GridReference { get; set; } = default!;

    public List<IPokemonCardViewModel> Cards { get; set; } = new List<IPokemonCardViewModel>();

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
