
using System.ComponentModel;

namespace CardManager.ViewModels;

public interface IViewModel : INotifyPropertyChanged
{
    Task OnInitializedAsync();
}
