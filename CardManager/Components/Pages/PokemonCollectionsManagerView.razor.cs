using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.Components.Pages;

public partial class PokemonCollectionsManagerView : BaseInjectView<IPokemonCollectionsManagerViewModel>, IDisposable
{
    public void Dispose()
    {
        this.ViewModel.NewCollectionCreated -= this.ViewModelNewCollectionSubmitted;
    }

    protected override void OnInitialized()
    {
        this.ViewModel.NewCollectionCreated += this.ViewModelNewCollectionSubmitted;
    }

    private void ViewModelNewCollectionSubmitted()
    {
        // lazy update everything
        this.StateHasChanged();
    }
}
