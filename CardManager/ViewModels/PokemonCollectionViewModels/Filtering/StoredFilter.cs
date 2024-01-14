namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering;

public class StoredFilter(
    Func<IComparable, IComparable, bool> passes,
    Func<IPokemonCardViewModel, IComparable> getValue,
    IComparable testValue,
    string stringified)
{
    public IEnumerable<IPokemonCardViewModel> Filter(IEnumerable<IPokemonCardViewModel> cards)
        => cards.Where(c => passes(getValue(c), testValue));

    public override string ToString() => stringified;
}
