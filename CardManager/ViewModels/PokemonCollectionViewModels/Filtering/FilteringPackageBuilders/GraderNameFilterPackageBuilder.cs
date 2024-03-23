using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
using CardManager.ViewModels.UtilityViewModels.Filtering;
using CardManager.Models.Grading;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;

public interface IGraderNameFilterPackageBuilder : IFilterPackageBuilder<IPokemonCardViewModel> { }
public class GraderNameFilterPackageBuilder : FilterPackageBuilder<IPokemonCardViewModel, IGraderNameCriteria>, IGraderNameFilterPackageBuilder
{
    protected override string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        IGraderNameCriteria criteria,
        IFilterEvaluationViewModel evaluation)
        => $"{criteria.Name} {this.GetPrefix(evaluation)}{evaluation.Name} " +
        $"{this.GetCollectionStringValues<GradingHost>(addFilterViewModel, criteria)}";

    protected override object GetValueToTest(IAddFilterViewModel addFilterViewModel, IGraderNameCriteria criteria)
        => addFilterViewModel.SelectedValues.Select(Enum.Parse<GradingHost>).ToList();

    protected override Func<IPokemonCardViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel)
        => (x) => x.Grading.SelectedGradingHost;
}
