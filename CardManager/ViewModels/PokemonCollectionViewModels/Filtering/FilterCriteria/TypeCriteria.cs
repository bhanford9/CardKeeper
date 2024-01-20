using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface ITypeCriteria : IFilterCriteria { }
public class TypeCriteria : BaseFilterCriteria, ITypeCriteria
{
    public override string Name { get; } = "Type";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.Collection;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [];
}