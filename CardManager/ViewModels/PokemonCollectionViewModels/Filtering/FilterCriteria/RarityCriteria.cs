using CardManager.Models.Cards.PokemonCards;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface IRarityCriteria : IFilterCriteria { }
public class RarityCriteria : BaseFilterCriteria, IRarityCriteria
{
    public override string Name { get; } = "Rarity";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.Collection;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [
        new EnumWithinEvaluation<PokemonRarity>(),
    ];
}