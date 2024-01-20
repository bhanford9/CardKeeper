namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

public interface ICheckBoxViewModel
{
    bool IsChecked { get; set; }
    object Value { get; set; }

    bool Equals(object? obj);
    string ToString();
}

public class CheckBoxViewModel<T> : ICheckBoxViewModel
{
    public CheckBoxViewModel() { }

    public CheckBoxViewModel(T value, bool isChecked)
    {
        this.Value = value;
        this.IsChecked = isChecked;
    }

    object ICheckBoxViewModel.Value
    {
        get => this.Value!;
        set => this.Value = (T)value;
    }

    public T Value { get; set; } = default!;

    public bool IsChecked { get; set; }

    public override bool Equals(object? obj)
        => (obj is CheckBoxViewModel<T> model            
            && this.Value!.Equals(model.Value)
            && this.IsChecked == model.IsChecked)
        || (obj is CheckBoxViewModel<object> o
            && o.Value is T v && v.Equals(this.Value)
            && this.IsChecked == o.IsChecked);

    public override string ToString() => this.Value?.ToString() ?? string.Empty;
}

public interface IWithinEvaluation : IFilterEvaluationViewModel
{
    List<ICheckBoxViewModel> Options { get; }
}
public abstract class WithinEvaluation<T> : CollectionFilterEvaluationViewModel<T, T>, IWithinEvaluation
{
    public override string Prefix { get; } = "is";

    public override string Name { get; } = "Within";

    public List<ICheckBoxViewModel> Options { get; protected init; } = [];

    protected override bool Passes(T value, IEnumerable<T> test) => test.Contains(value);
}
