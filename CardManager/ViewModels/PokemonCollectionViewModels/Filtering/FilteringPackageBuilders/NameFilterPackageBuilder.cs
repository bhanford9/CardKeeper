using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;

public interface INameFilterPackageBuilder : IFilterPackageBuilder<IPokemonCardViewModel> { }
public class NameFilterPackageBuilder : FilterPackageBuilder<IPokemonCardViewModel, INameFilterCriteria>, INameFilterPackageBuilder
{
    protected override string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        INameFilterCriteria criteria,
        IFilterEvaluationViewModel evaluation)
        => $"{criteria.Name} {this.GetPrefix(evaluation)}{evaluation.Name} {addFilterViewModel.StringComparison}";

    protected override object GetValueToTest(IAddFilterViewModel addFilterViewModel, INameFilterCriteria criteria)
        => addFilterViewModel.StringComparison;

    protected override Func<IPokemonCardViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel)
        => (x) => x.Name;
}
