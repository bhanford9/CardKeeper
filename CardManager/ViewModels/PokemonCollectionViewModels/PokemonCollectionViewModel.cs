using BlazorBootstrap;
using CardManager.Models.Cards.PokemonCards;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public interface IPokemonCollectionViewModel : IViewModel
{
    IQueryable<IPokemonCardViewModel> Cards { get; set; }

    event PokemonCollectionViewModel.EditCardPressedHandler? EditCardPressed;
    event PokemonCollectionViewModel.GridDataChangedHandler? GridDataChanged;

    void AddCard();
    void CardEditClicked(IPokemonCardViewModel card);
    void SaveTable();
    Task<GridDataProviderResult<IPokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<IPokemonCardViewModel> request);
    Task CardRowDoubleClicked(GridRowEventArgs<IPokemonCardViewModel> args);
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

    public IQueryable<IPokemonCardViewModel> Cards { get; set; } = new List<IPokemonCardViewModel>().AsQueryable();

    public async Task<GridDataProviderResult<IPokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<IPokemonCardViewModel> request)
        => await Task.FromResult(request.ApplyTo(this.Cards));

    public void AddCard()
    {
        this.Cards = this.Cards.Append(
            viewModelsFactory.NewPokemonCardViewModel(new PokemonCard()));
        this.GridDataChanged?.Invoke();
    }

    public void SaveTable()
    {

    }

    public void CardEditClicked(IPokemonCardViewModel card) => this.EditCardPressed?.Invoke(card);

    public async Task CardRowDoubleClicked(GridRowEventArgs<IPokemonCardViewModel> args)
    {
        if (this.EditCardPressed != null)
        {
            await this.EditCardPressed.Invoke(args.Item);
        }
    }
}
