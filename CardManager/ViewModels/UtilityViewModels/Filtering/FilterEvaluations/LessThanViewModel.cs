namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

public interface ILessThanViewModel : IFilterEvaluationViewModel { }
public class LessThanViewModel<T> : FilterEvaluationViewModel<T, T>, ILessThanViewModel where T : IComparable
{
    public override string Prefix { get; } = "Is";
    public override string Name { get; } = "Less Than";
    protected override bool Passes(T value, T test) => value.CompareTo(test) < 0;
}
