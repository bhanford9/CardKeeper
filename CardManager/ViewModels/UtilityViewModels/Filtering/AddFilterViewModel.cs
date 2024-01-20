using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using static CardManager.ViewModels.UtilityViewModels.Filtering.FilterViewModelEvents;

namespace CardManager.ViewModels.UtilityViewModels.Filtering;

public class FilterViewModelEvents
{
    public delegate Task FilterAppliedHandler();
}

public interface IAddFilterViewModel : IViewModel
{
    event FilterAppliedHandler? FilterApplied;
    List<IFilterCriteria> FilterCriteria { get; }
    IFilterCriteria SelectedFilterCriteria { get; set; }
    string StringComparison { get; set; }
    int IntegerComparison { get; set; }
    List<string> SelectedValues { get; set; }
    bool IsHidden { get; set; }

    void ApplyFilter();
    void SelectFilterCriteria(IFilterCriteria filterCriteria);
    void UpdateSelectedValues();
}

public class AddFilterViewModel(IEnumerable<IFilterCriteria> filterCriteria) : BaseViewModel, IAddFilterViewModel
{
    public event FilterAppliedHandler? FilterApplied;

    public List<IFilterCriteria> FilterCriteria { get; } = filterCriteria.ToList();

    public IFilterCriteria SelectedFilterCriteria { get; set; } = default!;

    public string StringComparison { get; set; } = string.Empty;

    public int IntegerComparison { get; set; }

    public List<string> SelectedValues { get; set; } = [];

    public bool IsHidden { get; set; } = true;

    public void SelectFilterCriteria(IFilterCriteria filterCriteria)
    {
        this.SelectedFilterCriteria = filterCriteria;
    }

    public void ApplyFilter()
    {
        this.FilterApplied?.Invoke();
        this.IsHidden = true;
    }

    public void UpdateSelectedValues()
    {
        if (this.SelectedFilterCriteria.SelectedEvaluation is IWithinEvaluation withinEval)
        {
            this.SelectedValues = withinEval.Options
                .Where(o => o.IsChecked)
                .Select(o => o.ToString())
                .ToList();
        }
    }
}
