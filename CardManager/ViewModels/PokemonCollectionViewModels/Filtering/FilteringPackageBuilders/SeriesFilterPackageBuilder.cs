using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
using CardManager.ViewModels.UtilityViewModels.Filtering;
using CardManager.Models.Cards.PokemonCards;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;

public interface ISeriesFilterPackageBuilder : IFilterPackageBuilder<IPokemonCardViewModel> { }
public class SeriesFilterPackageBuilder : FilterPackageBuilder<IPokemonCardViewModel, ISeriesCriteria>, ISeriesFilterPackageBuilder
{
    protected override string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        ISeriesCriteria criteria,
        IFilterEvaluationViewModel evaluation)
        => $"{criteria.Name} {this.GetPrefix(evaluation)}{evaluation.Name} " +
        $"{this.GetCollectionStringValues<PokemonSeries>(addFilterViewModel, criteria)}";

    protected override object GetValueToTest(IAddFilterViewModel addFilterViewModel, ISeriesCriteria criteria)
        => addFilterViewModel.SelectedValues.Select(Enum.Parse<PokemonSeries>).ToList();

    protected override Func<IPokemonCardViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel)
        => (x) => x.Series.SelectedValue;
}
