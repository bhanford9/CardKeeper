using BlazorBootstrap;
using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.Components.Pages;

public partial class PokemonCollectionsManagerView : BaseInjectView<IPokemonCollectionsManagerViewModel>, IDisposable
{
    private Modal newCollectionModal = default!;

    public void Dispose()
    {
        this.ViewModel.NewCollectionSubmitted -= this.ViewModelNewCollectionSubmitted;
    }

    protected override void OnInitialized()
    {
        this.ViewModel.NewCollectionSubmitted += this.ViewModelNewCollectionSubmitted;
    }

    private void ViewModelNewCollectionSubmitted()
    {
        this.newCollectionModal.HideAsync();
        this.StateHasChanged();
    }
}
