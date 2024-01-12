namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

public interface IFilterEvaluationViewModel
{
    string Name { get; }

    bool Passes(IComparable value, IComparable test);
}

public abstract class FilterEvaluationViewModel<TValue, TTest> :
    BaseViewModel,
    IFilterEvaluationViewModel
    where TValue : IComparable
    where TTest : IComparable
{
    public abstract string Name { get; }

    public bool Passes(IComparable value, IComparable test)
        => this.Passes((TValue)value, (TTest)test);

    protected abstract bool Passes(TValue value, TTest test);
}
