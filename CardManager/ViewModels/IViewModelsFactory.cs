using CardManager.ViewModels.ModalViewModels;

namespace CardManager.ViewModels;
public interface IViewModelsFactory
{
    IEditCardModalViewModel DefaultEditCardModal();
    IEditCardModalViewModel NewEditCardModal(Func<Task> onSubmit, Func<Task> onCancel);
}