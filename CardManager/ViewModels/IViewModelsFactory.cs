using CardManager.Models.CardCollections;
using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.Grading.BeckettGrading;
using CardManager.Models.Grading.CgcGrading;
using CardManager.Models.Grading.PsaGrading;
using CardManager.Models.MonetaryData;
using CardManager.Models.StorageSpecifications.Location;
using CardManager.Models.StorageSpecifications.Media;
using CardManager.ViewModels.GradingViewModels;
using CardManager.ViewModels.ModalViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;
using CardManager.ViewModels.StorageSpecViewModels;

namespace CardManager.ViewModels;
public interface IViewModelsFactory
{
    IEditCardModalViewModel DefaultEditCardModal();
    IBeckettGradingViewModel NewBeckettGrading(IBeckettGrade model);
    IStorageMediaViewModel NewBinderStorage(IBinder? binder = null);
    IBoxLocationViewModel NewBoxLocation(IBoxLocation? box = null);
    IStorageMediaViewModel NewBoxStorage(IBox? box = null);
    ICgcGradingViewModel NewCgcGrading(ICgcGrade model);
    IEditCardModalViewModel NewEditCardModal(Func<Task> onSubmit, Func<Task> onCancel);
    IMavinMonetaryViewModel NewMavinMonetary(IMavinMonetaryData model);
    IMonetaryAggregateViewModel NewMonetaryAggregate(IMavinMonetaryData mavin);
    NoLocationViewModel NewNoLocation(NoLocation? none = null);
    IStorageMediaViewModel NewNoStorage(NoStorageMedia? none = null);
    IPokemonCardViewModel NewPokemonCard();
    IPokemonCardViewModel NewPokemonCard(IPokemonCard card);
    IPokemonCollectionViewModel NewPokemonCollection(IPokemonCardCollection collection);
    IPsaGradingViewModel NewPsaGrading(IPsaGrade model);
    ISleeveLocationViewModel NewSleeveLocation(ISleeveLocation? sleeve = null);
}