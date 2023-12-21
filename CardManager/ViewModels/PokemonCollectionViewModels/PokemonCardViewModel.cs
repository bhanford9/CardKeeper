using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.StorageSpecifications;
using CardManager.ViewModels.GradingViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.UtilityViewModels;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public interface IPokemonCardViewModel : IViewModel
{
    int CreationYear { get; set; }
    Guid Id { get; set; }
    string Name { get; set; }
    string Number { get; set; }
    IEnumSelectorViewModel<PokemonHolographic> Holographic { get; set; }
    IEnumSelectorViewModel<PokemonRarity> Rarity { get; set; }
    IEnumSelectorViewModel<PokemonSeries> Series { get; set; }
    IStorageSpecification StorageSpec { get; set; }
    IEnumSelectorViewModel<ElementType> Type { get; set; }
    IGradingAggregateViewModel Grading { get; set; }
}

public class PokemonCardViewModel(IViewModelsFactory viewModelsFactory, IPokemonCard cardModel)
    : BaseViewModel, IPokemonCardViewModel
{
    private readonly IPokemonCard pokemonCardModel = cardModel;

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public int CreationYear { get; set; } = 9999;

    public IStorageSpecification StorageSpec { get; set; } = StorageSpecification.Default;

    public IGradingAggregateViewModel Grading { get; set; } = new GradingAggregateViewModel();

    public IMonetaryAggregateViewModel MonetaryData { get; set; } = new MonetaryAggregateViewModel(viewModelsFactory, cardModel.Monetary.Mavin);

    public IEnumSelectorViewModel<PokemonSeries> Series { get; set; } = new EnumSelectorViewModel<PokemonSeries>();

    public IEnumSelectorViewModel<PokemonRarity> Rarity { get; set; } = new EnumSelectorViewModel<PokemonRarity>();

    public IEnumSelectorViewModel<PokemonHolographic> Holographic { get; set; } = new EnumSelectorViewModel<PokemonHolographic>();

    public IEnumSelectorViewModel<ElementType> Type { get; set; } = new EnumSelectorViewModel<ElementType>();
}
