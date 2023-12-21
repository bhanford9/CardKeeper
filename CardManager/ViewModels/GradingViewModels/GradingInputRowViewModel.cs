using CardManager.ViewModels.UtilityViewModels;

namespace CardManager.ViewModels.GradingViewModels;

public interface IGradingInputRowViewModel<TScale> : IViewModel where TScale : struct, Enum
{
    string Identifier { get; set; }
    IEnumSelectorViewModel<TScale> SelectorViewModel { get; set; }

    string ToString();
}

public class GradingInputRowViewModel<TScale>(string identifier = "") :
    BaseViewModel,
    IGradingInputRowViewModel<TScale>
    where TScale : struct, Enum
{
    public string Identifier { get; set; } = identifier;

    public IEnumSelectorViewModel<TScale> SelectorViewModel { get; set; }
        = new EnumSelectorViewModel<TScale>();

    public override string ToString() => this.SelectorViewModel?.ToString() ?? "Not Graded";
}
