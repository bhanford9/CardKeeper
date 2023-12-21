using BlazorBootstrap;
using CardManager.Components.Pages.Modals;
using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.Components.Pages;

public partial class PokemonCardsView : BaseInjectView<IPokemonCollectionViewModel>, IDisposable
{
    private EditCardModal editCardModal = new();
    private Grid<IPokemonCardViewModel> cardsGrid = default!;
    private int[] pageSizeSelectors = [10, 20, 50];

    protected override void OnInitialized()
    {
        this.ViewModel.EditCardPressed += this.OnCardEditRequest;
        this.ViewModel.GridDataChanged += this.OnGridDataChanged;
    }

    public void Dispose()
    {
        this.ViewModel.EditCardPressed -= this.OnCardEditRequest;
        this.ViewModel.GridDataChanged -= this.OnGridDataChanged;
    }

    private async Task OnCardEditRequest(IPokemonCardViewModel card)
    {
        // TODO --> pass by reference means cancel will save data as well
        await this.editCardModal.ShowAsync(card, onSubmit: this.OnGridDataChanged);
    }

    private async Task OnGridDataChanged()
    {
        await this.cardsGrid.RefreshDataAsync();
    }
}