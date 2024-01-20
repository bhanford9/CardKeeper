namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

public interface IFilterEvaluationViewModel
{
    string Name { get; }
    string Prefix { get; }

    bool Passes(object value, object test);
}

public abstract class FilterEvaluationViewModel<TValue, TTest> :
    BaseViewModel,
    IFilterEvaluationViewModel
{
    public abstract string Prefix { get; }
    public abstract string Name { get; }

    public bool Passes(object value, object test)
        => this.Passes((TValue)value, (TTest)test);

    protected abstract bool Passes(TValue value, TTest test);
}

public abstract class CollectionFilterEvaluationViewModel<TValue, TTest> :
    BaseViewModel,
    IFilterEvaluationViewModel
{
    public abstract string Prefix { get; }
    public abstract string Name { get; }

    public bool Passes(object value, object test)
        => this.Passes((TValue)value, ((System.Collections.IEnumerable)test).Cast<TTest>());

    protected abstract bool Passes(TValue value, IEnumerable<TTest> test);
}
