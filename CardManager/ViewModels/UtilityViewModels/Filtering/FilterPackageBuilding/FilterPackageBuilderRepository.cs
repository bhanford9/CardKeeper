namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;

public interface IFilterPackageBuilderRepository<TViewModel> where TViewModel : IViewModel
{
    IFilterPackage<TViewModel> GetFilterPackage(IAddFilterViewModel addFilterViewModel);
}

public class FilterPackageBuilderRepository<TViewModel>(IEnumerable<IFilterPackageBuilder<TViewModel>> builders) : IFilterPackageBuilderRepository<TViewModel> where TViewModel : IViewModel
{
    private List<IFilterPackageBuilder<TViewModel>> builders = builders.ToList();

    public IFilterPackage<TViewModel> GetFilterPackage(IAddFilterViewModel addFilterViewModel)
        => this.builders.First(x => x.IsApplicable(addFilterViewModel.SelectedFilterCriteria)).Build(addFilterViewModel);
}
