using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering;

public class StoredFilter<TViewModel>(
    Func<object, object, bool> passes,
    IFilterPackage<TViewModel> filterPackage)
    where TViewModel : IViewModel
{
    public IEnumerable<TViewModel> Filter(IEnumerable<TViewModel> cards)
        => cards.Where(c => passes(filterPackage.ValueGetter(c), filterPackage.ValueToTest));

    public override string ToString() => filterPackage.Stringified;
}
