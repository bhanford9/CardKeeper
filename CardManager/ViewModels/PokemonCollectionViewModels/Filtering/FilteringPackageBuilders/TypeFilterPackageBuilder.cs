using CardManager.Models.Cards.PokemonCards;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
using CardManager.ViewModels.UtilityViewModels.Filtering;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;

public interface ITypeFilterPackageBuilder : IFilterPackageBuilder<IPokemonCardViewModel> { }
public class TypeFilterPackageBuilder : FilterPackageBuilder<IPokemonCardViewModel, ITypeCriteria>, ITypeFilterPackageBuilder
{
    protected override string GetStringified(
        IAddFilterViewModel addFilterViewModel,
        ITypeCriteria criteria,
        IFilterEvaluationViewModel evaluation)
        => $"{criteria.Name} {this.GetPrefix(evaluation)}{evaluation.Name} " +
        $"{this.GetCollectionStringValues<ElementType>(addFilterViewModel, criteria)}";

    protected override object GetValueToTest(IAddFilterViewModel addFilterViewModel, ITypeCriteria criteria)
        => addFilterViewModel.SelectedValues.Select(Enum.Parse<ElementType>).ToList();

    protected override Func<IPokemonCardViewModel, object> GetParameterRetriever(IAddFilterViewModel addFilterViewModel)
        => (x) => x.Type.SelectedValue;
}
