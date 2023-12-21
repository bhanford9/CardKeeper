using CardManager.Models.Cards.PokemonCards;
using CardManager.ViewModels.ModalViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.ViewModels;
public interface IViewModelsFactory
{
    IEditCardModalViewModel DefaultEditCardModal();
    IEditCardModalViewModel NewEditCardModal(Func<Task> onSubmit, Func<Task> onCancel);
    IPokemonCardViewModel NewPokemonCardViewModel(IPokemonCard card);
}