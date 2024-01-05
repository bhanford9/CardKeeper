using BlazorBootstrap;
using CardManager.Models.CardCollections;
using static CardManager.ViewModels.PokemonCollectionViewModels.CardCollectionEvents;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public class CardCollectionEvents
{
    public delegate Task EditCardPressedHandler(IPokemonCardViewModel card);
    public delegate Task GridDataChangedHandler();
    public delegate Task DeleteCardHandler(IPokemonCardViewModel card);
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

    event EditCardPressedHandler? EditCardPressed;
    event GridDataChangedHandler? GridDataChanged;
    event DeleteCardHandler? DeleteCard;

    void AddCard();
    void SaveTable();
    void RetrieveAppraisals();
    void DuplicateSelectedCard();
    Task DeleteSelectedCard();
    Task<GridDataProviderResult<IPokemonCardViewModel>> CardsDataProvider(
        GridDataProviderRequest<IPokemonCardViewModel> request);
    Task CardRowDoubleClicked(GridRowEventArgs<IPokemonCardViewModel> args);
    Task OnSelectedCardsChanged(HashSet<IPokemonCardViewModel> args);
}

public class PokemonCollectionViewModel
    : BaseViewModel, IPokemonCollectionViewModel
{
    private readonly IViewModelsFactory viewModelsFactory;
    private readonly IPokemonCardCollection pokemonCards;

    public event EditCardPressedHandler? EditCardPressed;
    public event GridDataChangedHandler? GridDataChanged;
    public event DeleteCardHandler? DeleteCard;

    public PokemonCollectionViewModel(
        IViewModelsFactory viewModelsFactory,
        IPokemonCardCollection pokemonCardCollection)
    {
        this.viewModelsFactory = viewModelsFactory;
        this.pokemonCards = pokemonCardCollection;
        this.Cards = pokemonCardCollection.Cards
            .Select(viewModelsFactory.NewPokemonCard)
            .ToList();

        this.UpdateStats();
    }

    public Grid<IPokemonCardViewModel> GridReference { get; set; } = default!;

    public List<IPokemonCardViewModel> Cards { get; set; }

    public double AverageMin { get; set; }

    public double AverageAverage { get; set; }

    public double AverageMax { get; set; }

    public double TotalMin { get; set; }

    public double TotalAverage { get; set; }

    public double TotalMax { get; set; }

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

        this.PokemonCollectionViewModelRowDataChanged();
    }

    public void DuplicateSelectedCard()
    {
        var cardToDuplicate = this.Cards.First(c => c.IsSelected);
        var duplicatedCard = this.viewModelsFactory.NewPokemonCard(cardToDuplicate.ToModel().DeepCopy());
        this.Cards.Add(duplicatedCard);

        this.PokemonCollectionViewModelRowDataChanged();
    }

    public async Task DeleteSelectedCard()
    {
        if (this.DeleteCard != null)
        {
            await this.DeleteCard.Invoke(this.Cards.First(c => c.IsSelected));
            this.PokemonCollectionViewModelRowDataChanged();
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
        foreach (var card in this.Cards)
        {
            card.IsSelected = args.Contains(card);
        }

        return Task.CompletedTask;
    }

    private void PokemonCollectionViewModelRowDataChanged()
    {
        this.GridDataChanged?.Invoke();
        this.UpdateStats();
    }

    private void UpdateStats()
    {
        this.AverageMin = Math.Round(this.Cards.Average(c => c.MonetaryData.MavinViewModel.MinPrice), 2);
        this.AverageAverage = Math.Round(this.Cards.Average(c => c.MonetaryData.MavinViewModel.AveragePrice), 2);
        this.AverageMax = Math.Round(this.Cards.Average(c => c.MonetaryData.MavinViewModel.MaxPrice), 2);
        this.TotalMin = Math.Round(this.Cards.Sum(c => c.MonetaryData.MavinViewModel.MinPrice), 2);
        this.TotalAverage = Math.Round(this.Cards.Sum(c => c.MonetaryData.MavinViewModel.AveragePrice), 2);
        this.TotalMax = Math.Round(this.Cards.Sum(c => c.MonetaryData.MavinViewModel.MaxPrice), 2);
    }
}
