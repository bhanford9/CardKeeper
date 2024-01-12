using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface INameFilterCriteria : IFilterCriteria { }
public class NameFilterCriteria : BaseFilterCriteria, INameFilterCriteria
{
    public override string Name { get; } = "Name";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.String;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [
        new ContainsViewModel(),
        new EqualViewModel<string>(),
        new NotEqualViewModel<string>(),
        new StartsWithViewModel(),
        new EndsWithViewModel(),
    ];
}
