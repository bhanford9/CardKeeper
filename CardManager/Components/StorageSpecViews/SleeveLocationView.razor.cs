using CardManager.Components.Pages;
using CardManager.ViewModels.StorageSpecViewModels;
using Microsoft.AspNetCore.Components;

namespace CardManager.Components.StorageSpecViews;

public partial class SleeveLocationView : BaseView<IStorageLocationViewModel>
{
    private ISleeveLocationViewModel sleeveViewModel => (ISleeveLocationViewModel)this.ViewModel;

    [Parameter]
    public string CssClasses { get; set; } = string.Empty;
}
