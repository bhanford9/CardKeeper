namespace CardManager.ViewModels;

public abstract partial class BaseViewModel : IViewModel
{
    public virtual async Task OnInitializedAsync()
    {
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
