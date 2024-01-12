using BlazorBootstrap;
using CardManager.Components.Pages.Modals;
using CardManager.Components.UtilityViews;
using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.Components.Pages;

public partial class PokemonCardsView : BaseView<IPokemonCollectionViewModel>, IDisposable
{
    private ConfirmDialog dialog = new();
    private EditCardModal editCardModal = new();
    private AddFilterModal addFilterModal = new();
    private Grid<IPokemonCardViewModel> cardsGrid = default!;
    private int[] pageSizeSelectors = [10, 20, 50];

    protected override void OnInitialized()
    {
        this.AttachViewModel();
    }

    protected override void OnParametersSet()
    {
        this.ViewModel.RemoveCard += this.OnRemoveCard;
        this.ViewModel.GridDataChanged += this.OnGridDataChanged;
        this.cardsGrid?.RefreshDataAsync();
        base.OnParametersSet();
    }

    public void Dispose()
    {
        this.DetachViewModel();
    }

    public async Task OnGridDataChanged()
    {
        await this.cardsGrid.RefreshDataAsync();

        // lazily updating everything to get the totals and averages to update
        this.StateHasChanged();
    }

    private async Task OnCardEditRequest(IPokemonCardViewModel card)
    {
        // TODO --> pass by reference means cancel will save data as well
        await this.editCardModal.ShowAsync(card, onSubmit: this.OnGridDataChanged);
    }

    private async Task OnAddFilter()
    {
        await this.addFilterModal.ShowAsync();
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

    private async Task OnRemoveCard(IPokemonCardViewModel card)
    {
        var isConfirmed = await this.dialog.ShowAsync(
            title: "Are you sure you want to remove this card?",
            message1: $"Card: {card.Name}, {card.Number}",
            message2: $"Rarity: {card.Rarity}, {card.Holographic} Holo");

        if (isConfirmed)
        {
            await this.ViewModel.RemoveFromCollection(card.Id);
        }
    }

    private void AttachViewModel()
    {
        this.ViewModel.EditCardPressed += this.OnCardEditRequest;
        this.ViewModel.GridDataChanged += this.OnGridDataChanged;
        this.ViewModel.DeleteCard += this.OnDeleteCard;
        this.ViewModel.RemoveCard += this.OnRemoveCard;
        this.ViewModel.AddFilter += this.OnAddFilter;
    }

    private void DetachViewModel()
    {
        this.ViewModel.EditCardPressed -= this.OnCardEditRequest;
        this.ViewModel.GridDataChanged -= this.OnGridDataChanged;
        this.ViewModel.DeleteCard -= this.OnDeleteCard;
        this.ViewModel.RemoveCard -= this.OnRemoveCard;
        this.ViewModel.AddFilter -= this.OnAddFilter;
    }
}