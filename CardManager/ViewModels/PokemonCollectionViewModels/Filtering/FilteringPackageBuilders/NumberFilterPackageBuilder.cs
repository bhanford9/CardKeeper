using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
using CardManager.ViewModels.UtilityViewModels.Filtering;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;

public interface INumberFilterPackageBuilder : IFilterPackageBuilder<IPokemonCardViewModel> { }
public class NumberFilterPackageBuilder : FilterPackageBuilder<IPokemonCardViewModel, INumberFilterCriteria>, INumberFilterPackageBuilder
{
    protected override string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        INumberFilterCriteria criteria,
        IFilterEvaluationViewModel evaluation)
        => $"{criteria.Name} {this.GetPrefix(evaluation)}{evaluation.Name} {this.GetValueToTest(addFilterViewModel, criteria)}";

    protected override object GetValueToTest(IAddFilterViewModel addFilterViewModel, INumberFilterCriteria criteria)
        => criteria.IsString() ? addFilterViewModel.StringComparison : addFilterViewModel.IntegerComparison;

    protected override Func<IPokemonCardViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel)
        => (x) => x.Number;
}
