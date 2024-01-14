using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface INumberFilterCriteria : IFilterCriteria
{
    bool IsString();
}
public class NumberFilterCriteria : BaseFilterCriteria, INumberFilterCriteria
{
    public override string Name { get; } = "Number";

    public override FilterCriteriaType Type => this.IsString() ? FilterCriteriaType.String : FilterCriteriaType.Integer;

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

    public bool IsString() => this.SelectedEvaluation switch
    {
        ContainsViewModel => true,
        EqualViewModel<string> => true,
        StartsWithViewModel => true,
        EndsWithViewModel => true,
        NumeratorLessThan => false,
        NumeratorGreaterThan => false,
        DenominatorLessThan => false,
        DenominatorGreaterThan => false,
        _ => false
    };
}
