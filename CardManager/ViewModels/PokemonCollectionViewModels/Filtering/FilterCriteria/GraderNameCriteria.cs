using CardManager.Models.Grading;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface IGraderNameCriteria : IFilterCriteria { }
public class GraderNameCriteria : BaseFilterCriteria, IGraderNameCriteria
{
    public override string Name { get; } = "Grader Name";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.Collection;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [
        new EnumWithinEvaluation<GradingHost>(),
    ];
}
