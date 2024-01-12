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
            }

            this.viewModel = value;

            this.viewModel.NewCollectionSubmitted += this.ViewModelNewCollectionSubmitted;
            this.StateHasChanged();
        }
    }

    public void Dispose()
    {
        this.ViewModel.NewCollectionSubmitted -= this.ViewModelNewCollectionSubmitted;
        this.ViewModel.CollectionSelected -= this.ViewModelCollectionSelected;
    }

    protected override void OnInitialized()
    {
        this.ViewModel.NewCollectionSubmitted += this.ViewModelNewCollectionSubmitted;
        this.ViewModel.CollectionSelected += this.ViewModelCollectionSelected;
    }

    private Task ViewModelCollectionSelected(string collectionName) => this.UpdateGrid();

    private async Task ViewModelNewCollectionSubmitted()
    {
        await this.newCollectionModal.HideAsync();
        await this.UpdateGrid();
    }

    private async Task UpdateGrid()
    {
        if (this.cardsView != null)
        {
            await this.cardsView.OnGridDataChanged();
            this.StateHasChanged();
        }
    }
}
