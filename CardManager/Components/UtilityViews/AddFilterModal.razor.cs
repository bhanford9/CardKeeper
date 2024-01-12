using BlazorBootstrap;
using CardManager.Components.Pages;
using CardManager.ViewModels.UtilityViewModels.Filtering;

namespace CardManager.Components.UtilityViews;

public partial class AddFilterModal : BaseInjectView<IAddFilterViewModel>, IDisposable
{
    private Modal filterModal = default!;

    protected override void OnInitialized()
    {
    }

    public Task ShowAsync() => this.filterModal.ShowAsync();

    public void Dispose()
    {
    }
}
