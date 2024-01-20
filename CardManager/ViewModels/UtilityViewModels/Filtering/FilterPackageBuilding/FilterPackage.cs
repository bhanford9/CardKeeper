namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;

public interface IFilterPackage<TViewModel>
    where TViewModel : IViewModel
{
    string Stringified { get; set; }
    Func<TViewModel, object> ValueGetter { get; set; }
    object ValueToTest { get; set; }
}

public class FilterPackage<TViewModel>(Func<TViewModel, object> valueGetter, object valueToTest, string stringified)
    : IFilterPackage<TViewModel>
    where TViewModel : IViewModel
{
    public Func<TViewModel, object> ValueGetter { get; set; } = valueGetter;

    public object ValueToTest { get; set; } = valueToTest;

    public string Stringified { get; set; } = stringified;
}
