namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

public interface IContainsViewModel : IFilterEvaluationViewModel { }
public class ContainsViewModel : FilterEvaluationViewModel<string, string>, IContainsViewModel
{
    public override string Prefix { get; } = "";
    public override string Name { get; } = "Contains";
    protected override bool Passes(string value, string test) => value.Contains(test);
}

public interface IStartsWithViewModel : IFilterEvaluationViewModel { }
public class StartsWithViewModel : FilterEvaluationViewModel<string, string>, IStartsWithViewModel
{
    public override string Prefix { get; } = "";
    public override string Name { get; } = "Starts With";
    protected override bool Passes(string value, string test) => value.StartsWith(test);
}

public interface IEndsWithViewModel : IFilterEvaluationViewModel { }
public class EndsWithViewModel : FilterEvaluationViewModel<string, string>, IEndsWithViewModel
{
    public override string Prefix { get; } = "";
    public override string Name { get; } = "Ends With";
    protected override bool Passes(string value, string test) => value.EndsWith(test);
}