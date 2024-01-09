using BlazorBootstrap;
using CardManager.ViewModels.PokemonCollectionViewModels;
using Microsoft.AspNetCore.Components;

namespace CardManager.Components.Pages;

public partial class PokemonCustomCollectionView : BaseView<IPokemonCustomCollectionViewModel>, IDisposable
{
    private Modal newCollectionModal = default!;
    private PokemonCardsView cardsView = default!;
    private IPokemonCustomCollectionViewModel viewModel = default!;

    [Parameter]
    public override IPokemonCustomCollectionViewModel ViewModel
    {
        get => this.viewModel;
        set
        {
            if (this.viewModel != default)
            {
                this.viewModel.NewCollectionSubmitted -= this.ViewModelNewCollectionSubmitted;
                this.viewModel.GridDataChanged -= this.UpdateGrid;
            }

            this.viewModel = value;

            this.viewModel.NewCollectionSubmitted += this.ViewModelNewCollectionSubmitted;
            this.viewModel.GridDataChanged += this.UpdateGrid;
        }
    }

    public void Dispose()
    {
        this.ViewModel.NewCollectionSubmitted -= this.ViewModelNewCollectionSubmitted;
        this.ViewModel.GridDataChanged -= this.UpdateGrid;
    }

    protected override void OnInitialized()
    {
        this.ViewModel.NewCollectionSubmitted += this.ViewModelNewCollectionSubmitted;
        this.ViewModel.GridDataChanged += this.UpdateGrid;
    }

    private void ViewModelNewCollectionSubmitted()
    {
        this.newCollectionModal.HideAsync();
        this.StateHasChanged();
    }

    private async Task UpdateGrid()
    {
        await this.cardsView.OnGridDataChanged();
        this.StateHasChanged();
    }
}
