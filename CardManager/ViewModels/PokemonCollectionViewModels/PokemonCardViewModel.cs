using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.Grading;
using CardManager.Models.StorageSpecification;
using CardManager.ViewModels.GradingViewModels;
using CardManager.ViewModels.UtilityViewModels;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public interface IPokemonCardViewModel : IViewModel
{
    BeckettGradingViewModel BeckettGrading { get; set; }
    CgcGradingViewModel CgcGrading { get; set; }
    int CreationYear { get; set; }
    EnumSelectorViewModel<PokemonHolographic> Holographic { get; set; }
    Guid Id { get; set; }
    string Name { get; set; }
    string Number { get; set; }
    PsaGradingViewModel PsaGrading { get; set; }
    EnumSelectorViewModel<PokemonRarity> Rarity { get; set; }
    GradingHost SelectedGradingHost { get; set; }
    EnumSelectorViewModel<PokemonSeries> Series { get; set; }
    IStorageSpecification StorageSpecification { get; set; }
    EnumSelectorViewModel<ElementType> Type { get; set; }
}

public class PokemonCardViewModel : BaseViewModel, IPokemonCardViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public int CreationYear { get; set; } = 9999;

    public IStorageSpecification StorageSpecification { get; set; }

    public GradingHost SelectedGradingHost { get; set; }

    public BeckettGradingViewModel BeckettGrading { get; set; } = new();

    public CgcGradingViewModel CgcGrading { get; set; } = new();

    public PsaGradingViewModel PsaGrading { get; set; } = new();

    public EnumSelectorViewModel<PokemonSeries> Series { get; set; } = new();

    public EnumSelectorViewModel<PokemonRarity> Rarity { get; set; } = new();

    public EnumSelectorViewModel<PokemonHolographic> Holographic { get; set; } = new();

    public EnumSelectorViewModel<ElementType> Type { get; set; } = new();
}
