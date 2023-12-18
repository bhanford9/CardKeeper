namespace CardManager.ViewModels.ModalViewModels;

public interface IEditCardModalViewModel : IViewModel
{
    event EditCardModalViewModel.EditModalCompletedHandler? EditModalCompleted;

    void Cancel();
    void OnHidden();
    void Submit();
}

public class EditCardModalViewModel(Func<Task> onSubmit, Func<Task> onCancel) : BaseViewModel, IEditCardModalViewModel
{
    private readonly Func<Task> onSubmit = onSubmit;
    private readonly Func<Task> onCancel = onCancel;
    private bool cancelled = true;

    public delegate Task EditModalCompletedHandler();
    public event EditModalCompletedHandler? EditModalCompleted;

    public void Cancel()
    {
        this.cancelled = true;
        EditModalCompleted?.Invoke();
    }

    public void Submit()
    {
        this.cancelled = false;
        EditModalCompleted?.Invoke();
    }

    public void OnHidden()
    {
        if (this.cancelled)
        {
            this.onCancel?.Invoke();
        }
        else
        {
            this.onSubmit?.Invoke();
        }
    }
}
