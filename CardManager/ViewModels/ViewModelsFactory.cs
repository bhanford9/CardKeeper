using CardManager.ViewModels.ModalViewModels;

namespace CardManager.ViewModels;

public class ViewModelsFactory : IViewModelsFactory
{
    public IEditCardModalViewModel NewEditCardModal(Func<Task> onSubmit, Func<Task> onCancel)
        => new EditCardModalViewModel(onSubmit, onCancel);
    public IEditCardModalViewModel DefaultEditCardModal()
        => new EditCardModalViewModel(() => { return Task.CompletedTask; }, () => { return Task.CompletedTask; });
}
