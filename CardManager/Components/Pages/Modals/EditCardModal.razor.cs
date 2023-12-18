using BlazorBootstrap;
using CardManager.ViewModels.ModalViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.Components.Pages.Modals;

public partial class EditCardModal : BaseView<IEditCardModalViewModel>
{
    private Modal cardModal = default!;
    private PokemonCardViewModel cardViewModel = new();

    protected override void OnInitialized()
    {
        this.ViewModel = this.viewModelsFactory.DefaultEditCardModal();
        base.OnInitialized();
    }

    public async Task ShowAsync(
        PokemonCardViewModel cardViewModel,
        Func<Task> onSubmit = default!,
        Func<Task> onCancel = default!)
    {
        this.cardViewModel = cardViewModel;
        this.ViewModel = this.viewModelsFactory.NewEditCardModal(onSubmit, onCancel);
        this.ViewModel.EditModalCompleted += this.cardModal.HideAsync;
        await this.cardModal.ShowAsync();
    }
}
