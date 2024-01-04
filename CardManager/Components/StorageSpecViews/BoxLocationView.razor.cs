using CardManager.Components.Pages;
using CardManager.ViewModels.StorageSpecViewModels;
using Microsoft.AspNetCore.Components;

namespace CardManager.Components.StorageSpecViews;

public partial class BoxLocationView : BaseView<IStorageLocationViewModel>
{
    private IBoxLocationViewModel boxViewModel => (IBoxLocationViewModel)this.ViewModel;

    [Parameter]
    public string CssClasses { get; set; } = string.Empty;
}
