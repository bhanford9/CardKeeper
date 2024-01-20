using CardManager.Models.Cards.PokemonCards;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;

public interface IHolographicFilterPackageBuilder : IFilterPackageBuilder<IPokemonCardViewModel> { }
public class HolographicFilterPackageBuilder : FilterPackageBuilder<IPokemonCardViewModel, IHolographicCriteria>, IHolographicFilterPackageBuilder
{
    protected override string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        IHolographicCriteria criteria,
        IFilterEvaluationViewModel evaluation)
        => $"{criteria.Name} {this.GetPrefix(evaluation)}{evaluation.Name} " +
        $"{this.GetCollectionStringValues<PokemonHolographic>(addFilterViewModel, criteria)}";

    protected override object GetValueToTest(IAddFilterViewModel addFilterViewModel, IHolographicCriteria criteria)
        => addFilterViewModel.SelectedValues.Select(Enum.Parse<PokemonHolographic>).ToList();

    protected override Func<IPokemonCardViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel)
        => (x) => x.Holographic.SelectedValue;
}
