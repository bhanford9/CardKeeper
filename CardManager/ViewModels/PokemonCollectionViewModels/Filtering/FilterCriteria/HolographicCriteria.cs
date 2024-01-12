using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface IHolographicCriteria : IFilterCriteria { }
public class HolographicCriteria : BaseFilterCriteria, IHolographicCriteria
{
    public override string Name { get; } = "Holographic";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.TBD;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [];
}
