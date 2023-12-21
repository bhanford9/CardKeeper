namespace CardManager.ViewModels.UtilityViewModels;

public interface IEnumSelectorViewModel<TScale> : IViewModel where TScale : struct, Enum
{
    TScale SelectedValue { get; set; }

    string ToString();
}

public class EnumSelectorViewModel<TScale> : BaseViewModel, IEnumSelectorViewModel<TScale> where TScale : struct, Enum
{
    public TScale SelectedValue { get; set; } = default;

    public override string ToString() => this.SelectedValue.ToString();
}
