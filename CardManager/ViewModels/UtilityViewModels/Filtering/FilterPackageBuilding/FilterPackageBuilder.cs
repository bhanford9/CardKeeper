using CardManager.ViewModels.PokemonCollectionViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;

public interface IFilterPackageBuilder<TParentViewModel>
    where TParentViewModel : IViewModel
{
    IFilterPackage<TParentViewModel> Build(IAddFilterViewModel addFilterViewModel);

    bool IsApplicable(IFilterCriteria criteria);
}

public abstract class FilterPackageBuilder<TViewModel, TCriteria> : IFilterPackageBuilder<TViewModel>
    where TViewModel : IViewModel
    where TCriteria : IFilterCriteria
{
    public IFilterPackage<TViewModel> Build(IAddFilterViewModel addFilterViewModel) => new FilterPackage<TViewModel>(
        this.GetParameterRetriever(addFilterViewModel),
        this.GetValueToTest(addFilterViewModel, (TCriteria)addFilterViewModel.SelectedFilterCriteria),
        this.GetStringified(
            addFilterViewModel,
            (TCriteria)addFilterViewModel.SelectedFilterCriteria,
            addFilterViewModel.SelectedFilterCriteria.SelectedEvaluation));

    public virtual bool IsApplicable(IFilterCriteria criteria) => criteria is TCriteria;

    protected abstract string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        TCriteria criteria,
        IFilterEvaluationViewModel evaluation);

    protected abstract object GetValueToTest(IAddFilterViewModel addFilterViewModel, TCriteria criteria);

    protected abstract Func<TViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel);

    protected string GetPrefix(IFilterEvaluationViewModel evaluation)
        => evaluation.Prefix.Length > 0 ? $"{evaluation.Prefix} " : evaluation.Prefix;

    protected string GetCollectionStringValues<TItem>(IAddFilterViewModel addFilterViewModel, TCriteria criteria)
    {
        var values = this.GetValueToTest(addFilterViewModel, criteria) as List<TItem>;
        if (values!.Count == 0) return string.Empty;
        if (values!.Count == 1) return values!.First()!.ToString()!;
        return values.Aggregate(
            Environment.NewLine,
            (accum, curr) => $"{accum}{curr}{Environment.NewLine}",
            (result) => result.TrimEnd());
    }
}
