using CardManager.Models.MonetaryData;
using CardManager.Models.StorageSpecifications.Location;
using CardManager.ViewModels.ModalViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;
using CardManager.ViewModels.StorageSpecViewModels;

namespace CardManager.ViewModels;
public interface IViewModelsFactory
{
    IEditCardModalViewModel DefaultEditCardModal();
    IStorageMediaViewModel NewBinderStorage();
    IBoxLocationViewModel NewBoxLocation();
    IStorageMediaViewModel NewBoxStorage();
    IEditCardModalViewModel NewEditCardModal(Func<Task> onSubmit, Func<Task> onCancel);
    IMavinMonetaryViewModel NewMavinMonetary(IMavinMonetaryData model);
    NoLocationViewModel NewNoLocation();
    IStorageMediaViewModel NewNoStorage();
    IPokemonCardViewModel NewPokemonCard();
    ISleeveLocationViewModel NewSleeveLocation();
}