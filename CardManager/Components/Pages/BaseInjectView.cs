using CardManager.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CardManager.Components.Pages;

public interface IBaseView<TViewModel> where TViewModel : IViewModel
{
    TViewModel ViewModel { get; set; }
    EventCallback<TViewModel> ViewModelChanged { get; set; }
}

public partial class BaseView<TViewModel> : ComponentBase, IBaseView<TViewModel> where TViewModel : IViewModel
{
    [Parameter]
    public virtual TViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public EventCallback<TViewModel> ViewModelChanged { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override Task OnInitializedAsync()
    {
        if (this.ViewModel != null)
            return this.ViewModel.OnInitializedAsync();

        return base.OnInitializedAsync();
    }
}

public partial class BaseInjectView<TViewModel> : BaseView<TViewModel> where TViewModel : IViewModel
{
    [Inject]
    [Parameter]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public override TViewModel ViewModel { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
