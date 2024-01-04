using static CardManager.ViewModels.UtilityViewModels.ValueSelectedUtil;

namespace CardManager.ViewModels.UtilityViewModels;

public static class ValueSelectedUtil
{
    public delegate void ValueSelectedHandler();
}

public interface IEnumSelectorViewModel<TScale> : IViewModel where TScale : struct, Enum
{
    event ValueSelectedHandler? ValueSelected;

    TScale SelectedValue { get; set; }

    string ToString();
}

public class EnumSelectorViewModel<TScale> : BaseViewModel, IEnumSelectorViewModel<TScale> where TScale : struct, Enum
{
    public event ValueSelectedHandler? ValueSelected;

    private TScale selectedValue = default;

    public TScale SelectedValue
    {
        get => this.selectedValue;
        set
        {
            this.selectedValue = value;
            this.ValueSelected?.Invoke();
        }
    }

    public override string ToString() => this.SelectedValue.ToString();
}
