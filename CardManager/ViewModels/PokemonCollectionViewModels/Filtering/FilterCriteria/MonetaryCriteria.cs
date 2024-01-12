using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface IMonetaryCriteria : IFilterCriteria { }
public class MonetaryCriteria : BaseFilterCriteria, IMonetaryCriteria
{
    public override string Name { get; } = "Monetary";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.TBD;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [];
}
