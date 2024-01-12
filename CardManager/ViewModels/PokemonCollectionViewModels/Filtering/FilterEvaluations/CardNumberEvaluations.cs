using CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

namespace CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterEvaluations;

public interface INumeratorGreaterThan : IFilterEvaluationViewModel { }
public class NumeratorGreaterThan : FilterEvaluationViewModel<string, int>, INumeratorGreaterThan
{
    public override string Name { get; } = "Top Greater Than";
    protected override bool Passes(string value, int test) =>
        int.Parse(value[..value.IndexOf('/')].TrimStart('0')) > test;
}

public interface INumeratorLessThan : IFilterEvaluationViewModel { }
public class NumeratorLessThan : FilterEvaluationViewModel<string, int>, INumeratorLessThan
{
    public override string Name { get; } = "Top Less Than";
    protected override bool Passes(string value, int test) =>
        int.Parse(value[..value.IndexOf('/')].TrimStart('0')) < test;
}

public interface IDenominatorGreaterThan : IFilterEvaluationViewModel { }
public class DenominatorGreaterThan : FilterEvaluationViewModel<string, int>, IDenominatorGreaterThan
{
    public override string Name { get; } = "Bottom Greater Than";
    protected override bool Passes(string value, int test) =>
        int.Parse(value[(value.IndexOf('/') + 1)..].TrimStart('0')) > test;
}

public interface IDenominatorLessThan : IFilterEvaluationViewModel { }
public class DenominatorLessThan : FilterEvaluationViewModel<string, int>, IDenominatorLessThan
{
    public override string Name { get; } = "Bottom Less Than";
    protected override bool Passes(string value, int test) =>
        int.Parse(value[(value.IndexOf('/') + 1)..].TrimStart('0')) < test;
}
