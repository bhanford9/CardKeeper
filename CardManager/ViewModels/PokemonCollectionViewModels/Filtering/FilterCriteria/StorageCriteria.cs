using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public interface IStorageCriteria : IFilterCriteria { }
public class StorageCriteria : BaseFilterCriteria, IStorageCriteria
{
    public override string Name { get; } = "Storage";

    public override FilterCriteriaType Type { get; } = FilterCriteriaType.TBD;

    public override IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; } = [];
}
