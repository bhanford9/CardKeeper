using BlazorBootstrap;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public interface IPokemonCollectionViewModel : IViewModel
{
    IQueryable<PokemonCardViewModel> Cards { get; set; }

    event PokemonCollectionViewModel.EditCardPressedHandler? EditCardPressed;
    event PokemonCollectionViewModel.GridDataChangedHandler? GridDataChanged;

    void AddCard();
    void CardEditClicked(PokemonCardViewModel card);
    void SaveTable();
    Task<GridDataProviderResult<PokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<PokemonCardViewModel> request);
    Task CardRowDoubleClicked(GridRowEventArgs<PokemonCardViewModel> args);
}

public class PokemonCollectionViewModel : BaseViewModel, IPokemonCollectionViewModel
{
    public delegate Task EditCardPressedHandler(PokemonCardViewModel card);
    public delegate Task GridDataChangedHandler();

    public event EditCardPressedHandler? EditCardPressed;
    public event GridDataChangedHandler? GridDataChanged;

    public Grid<PokemonCardViewModel> GridReference { get; set; } = default!;

    public IQueryable<PokemonCardViewModel> Cards { get; set; } = new List<PokemonCardViewModel>().AsQueryable();

    public async Task<GridDataProviderResult<PokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<PokemonCardViewModel> request)
        => await Task.FromResult(request.ApplyTo(Cards));

    public void AddCard()
    {
        this.Cards = this.Cards.Append(new());
        this.GridDataChanged?.Invoke();
    }

    public void SaveTable()
    {

    }

    public void CardEditClicked(PokemonCardViewModel card) => this.EditCardPressed?.Invoke(card);

    public async Task CardRowDoubleClicked(GridRowEventArgs<PokemonCardViewModel> args)
    {
        if (this.EditCardPressed != null)
        {
            await this.EditCardPressed.Invoke(args.Item);
        }
    }
}
