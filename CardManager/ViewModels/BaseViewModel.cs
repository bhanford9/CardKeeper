using System.ComponentModel;

namespace CardManager.ViewModels;

public abstract partial class BaseViewModel : IViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual async Task OnInitializedAsync()
    {
        await Task.CompletedTask.ConfigureAwait(false);
    }

    protected void NotifyPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
