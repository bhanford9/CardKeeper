using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface INumberFilterCriteria : IFilterCriteria { }
public class NumberFilterCriteria : BaseFilterCriteria, INumberFilterCriteria
{
    public override string Name { get; } = "Number";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.Integer;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [
        new ContainsViewModel(),
        new EqualViewModel<string>(),
        new NotEqualViewModel<string>(),
        new StartsWithViewModel(),
        new EndsWithViewModel(),
        new NumeratorLessThan(),
        new NumeratorGreaterThan(),
        new DenominatorLessThan(),
        new DenominatorGreaterThan(),
    ];
}
