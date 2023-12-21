using BlazorBootstrap;
using CardManager.Models.Cards.PokemonCards;
using CardManager.ViewModels.ModalViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.Components.Pages.Modals;

public partial class EditCardModal : BaseView<IEditCardModalViewModel>
{
    private Modal cardModal = default!;
    private IPokemonCardViewModel cardViewModel = default!;

    protected override void OnInitialized()
    {
        this.cardViewModel = this.viewModelsFactory.NewPokemonCardViewModel(new PokemonCard());
        this.ViewModel = this.viewModelsFactory.DefaultEditCardModal();
        base.OnInitialized();
    }

    public async Task ShowAsync(
        IPokemonCardViewModel cardViewModel,
        Func<Task> onSubmit = default!,
        Func<Task> onCancel = default!)
    {
        this.cardViewModel = cardViewModel;
        this.ViewModel = this.viewModelsFactory.NewEditCardModal(onSubmit, onCancel);
        this.ViewModel.EditModalCompleted += this.cardModal.HideAsync;
        await this.cardModal.ShowAsync();
    }
}
