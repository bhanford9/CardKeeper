using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using static CardManager.ViewModels.UtilityViewModels.Filtering.FilterViewModelEvents;

namespace CardManager.ViewModels.UtilityViewModels.Filtering;

public class FilterViewModelEvents
{
    public delegate void FilterAppliedHandler();
}

public interface IAddFilterViewModel : IViewModel
{
    event FilterAppliedHandler? FilterApplied;
    List<IFilterCriteria> FilterCriteria { get; }
    IFilterCriteria SelectedFilterCriteria { get; set; }
    string StringComparison { get; set; }
    int IntegerComparison { get; set; }
    bool IsHidden { get; set; }

    void ApplyFilter();
    void SelectFilterCriteria(IFilterCriteria filterCriteria);
}

public class AddFilterViewModel(IEnumerable<IFilterCriteria> filterCriteria) : BaseViewModel, IAddFilterViewModel
{
    public event FilterAppliedHandler? FilterApplied;

    public List<IFilterCriteria> FilterCriteria { get; } = filterCriteria.ToList();

    public IFilterCriteria SelectedFilterCriteria { get; set; } = default!;

    public string StringComparison { get; set; } = string.Empty;

    public int IntegerComparison { get; set; }

    public bool IsHidden { get; set; } = true;

    public void SelectFilterCriteria(IFilterCriteria filterCriteria)
    {
        SelectedFilterCriteria = filterCriteria;
    }

    public void ApplyFilter()
    {
        this.FilterApplied?.Invoke();
        this.IsHidden = true;
    }
}
