using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;

namespace CardManager.ViewModels.UtilityViewModels.Filtering;

public interface IAddFilterViewModel : IViewModel
{
    List<IFilterCriteria> FilterCriteria { get; }
    IFilterCriteria SelectedFilterCriteria { get; set; }
    string StringComparison { get; set; }
    int IntegerComparison { get; set; }

    void ApplyFilter();
    void SelectFilterCriteria(IFilterCriteria filterCriteria);
}

public class AddFilterViewModel(IEnumerable<IFilterCriteria> filterCriteria) : BaseViewModel, IAddFilterViewModel
{
    public List<IFilterCriteria> FilterCriteria { get; } = filterCriteria.ToList();

    public IFilterCriteria SelectedFilterCriteria { get; set; } = default!;

    public string StringComparison { get; set; } = string.Empty;

    public int IntegerComparison { get; set; }

    public void SelectFilterCriteria(IFilterCriteria filterCriteria)
    {
        SelectedFilterCriteria = filterCriteria;
    }

    public void ApplyFilter()
    {

    }
}
