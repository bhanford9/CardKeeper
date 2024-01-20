using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
using CardManager.ViewModels.UtilityViewModels.Filtering;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;

public interface IYearFilterPackageBuilder : IFilterPackageBuilder<IPokemonCardViewModel> { }
public class YearFilterPackageBuilder : FilterPackageBuilder<IPokemonCardViewModel, IYearFilterCriteria>, IYearFilterPackageBuilder
{
    protected override string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        IYearFilterCriteria criteria,
        IFilterEvaluationViewModel evaluation)
        => $"{criteria.Name} {this.GetPrefix(evaluation)}{evaluation.Name} {addFilterViewModel.IntegerComparison}";

    protected override object GetValueToTest(IAddFilterViewModel addFilterViewModel, IYearFilterCriteria criteria)
        => addFilterViewModel.IntegerComparison;

    protected override Func<IPokemonCardViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel)
        => (x) => x.Number;
}
