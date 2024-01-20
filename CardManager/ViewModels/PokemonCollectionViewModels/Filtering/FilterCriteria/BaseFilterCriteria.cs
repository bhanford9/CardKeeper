using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

public enum FilterCriteriaType
{
    TBD,
    String,
    Integer,
    Collection,
}

public interface IFilterCriteria
{
    IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; }
    string Name { get; }
    IFilterEvaluationViewModel SelectedEvaluation { get; set; }
    FilterCriteriaType Type { get; }

    void SelectEvaluation(IFilterEvaluationViewModel selectedEvaluation);
}

public abstract class BaseFilterCriteria : IFilterCriteria
{
    public abstract string Name { get; }

    public abstract FilterCriteriaType Type { get; }

    public abstract IReadOnlyList<IFilterEvaluationViewModel> Evaluations { get; }

    public IFilterEvaluationViewModel SelectedEvaluation { get; set; } = default!;

    public void SelectEvaluation(IFilterEvaluationViewModel selectedEvaluation)
        => this.SelectedEvaluation = selectedEvaluation;
}
