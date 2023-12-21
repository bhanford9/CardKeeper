using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.MonetaryData;
using CardManager.ViewModels.ModalViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;

namespace CardManager.ViewModels;
public interface IViewModelsFactory
{
    IEditCardModalViewModel DefaultEditCardModal();
    IEditCardModalViewModel NewEditCardModal(Func<Task> onSubmit, Func<Task> onCancel);
    IMavinMonetaryViewModel NewMavinMonetaryViewModel(IMavinMonetaryData model);
    IPokemonCardViewModel NewPokemonCardViewModel(IPokemonCard card);
}