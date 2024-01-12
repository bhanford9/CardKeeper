using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface IGradeCriteria : IFilterCriteria { }
public class GradeCriteria : BaseFilterCriteria, IGradeCriteria
{
    public override string Name { get; } = "Grade";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.TBD;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [];
}
