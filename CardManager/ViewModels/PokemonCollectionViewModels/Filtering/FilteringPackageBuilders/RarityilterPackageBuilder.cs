using CardManager.Models.Cards.PokemonCards;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
using CardManager.ViewModels.UtilityViewModels.Filtering;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;

public interface IRarityFilterPackageBuilder : IFilterPackageBuilder<IPokemonCardViewModel> { }
public class RarityFilterPackageBuilder : FilterPackageBuilder<IPokemonCardViewModel, IRarityCriteria>, IRarityFilterPackageBuilder
{
    protected override string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        IRarityCriteria criteria,
        IFilterEvaluationViewModel evaluation)
        => $"{criteria.Name} {this.GetPrefix(evaluation)}{evaluation.Name} " +
        $"{this.GetCollectionStringValues<PokemonRarity>(addFilterViewModel, criteria)}";

    protected override object GetValueToTest(IAddFilterViewModel addFilterViewModel, IRarityCriteria criteria)
        => addFilterViewModel.SelectedValues.Select(Enum.Parse<PokemonRarity>).ToList();

    protected override Func<IPokemonCardViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel)
        => (x) => x.Rarity.SelectedValue;
}
