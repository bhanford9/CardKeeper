namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

public interface IEqualViewModel : IFilterEvaluationViewModel { }
public class EqualViewModel<T> : FilterEvaluationViewModel<T, T>, IEqualViewModel where T : IComparable
{
    public override string Prefix { get; } = "Is";
    public override string Name { get; } = "Equal To";
    protected override bool Passes(T value, T test) => value == null ? test == null : value.Equals(test);
}

public interface INotEqualViewModel : IFilterEvaluationViewModel { }
public class NotEqualViewModel<T> : EqualViewModel<T>, INotEqualViewModel where T : IComparable
{
    public override string Name { get; } = "Not Equal To";
    protected override bool Passes(T value, T test) => !base.Passes(value, test);
}