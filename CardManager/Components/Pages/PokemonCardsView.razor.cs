using BlazorBootstrap;
using CardManager.Components.Pages.Modals;
using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.Components.Pages;

public partial class PokemonCardsView : BaseView<IPokemonCollectionViewModel>, IDisposable
{
    private ConfirmDialog dialog = new();
    private EditCardModal editCardModal = new();
    private Grid<IPokemonCardViewModel> cardsGrid = default!;
    private int[] pageSizeSelectors = [10, 20, 50];

    protected override void OnInitialized()
    {
        this.ViewModel.EditCardPressed += this.OnCardEditRequest;
        this.ViewModel.GridDataChanged += this.OnGridDataChanged;
        this.ViewModel.DeleteCard += this.OnDeleteCard;
    }

    public void Dispose()
    {
        this.ViewModel.EditCardPressed -= this.OnCardEditRequest;
        this.ViewModel.GridDataChanged -= this.OnGridDataChanged;
        this.ViewModel.DeleteCard -= this.OnDeleteCard;
    }

    private async Task OnCardEditRequest(IPokemonCardViewModel card)
    {
        // TODO --> pass by reference means cancel will save data as well
        await this.editCardModal.ShowAsync(card, onSubmit: this.OnGridDataChanged);
    }

    private async Task OnGridDataChanged()
    {
        await this.cardsGrid.RefreshDataAsync();

        // lazily updating everything to get the totals and averages to update
        this.StateHasChanged();
    }

    private async Task OnDeleteCard(IPokemonCardViewModel card)
    {
        var isConfirmed = await this.dialog.ShowAsync(
            title: "Are you sure you want to delete this card?",
            message1: $"Card: {card.Name}, {card.Number}",
            message2: $"Rarity: {card.Rarity}, {card.Holographic} Holo");

        if (isConfirmed)
        {
            this.ViewModel.Cards.Remove(card);
        }
    }
}