namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

public interface IGreaterThanViewModel : IFilterEvaluationViewModel { }
public class GreaterThanViewModel<T> : FilterEvaluationViewModel<T, T>, IGreaterThanViewModel where T : IComparable
{
    public override string Name { get; } = "Greater Than";
    protected override bool Passes(T value, T test) => value.CompareTo(test) > 0;
}
