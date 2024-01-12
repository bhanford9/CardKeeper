using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface IYearFilterCriteria : IFilterCriteria { }
public class YearFilterCriteria : BaseFilterCriteria, IYearFilterCriteria
{
    public override string Name { get; } = "Year";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.Integer;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [
        new EqualViewModel<int>(),
        new NotEqualViewModel<int>(),
        new GreaterThanViewModel<int>(),
        new LessThanViewModel<int>(),
    ];
}
