using CardManager.Components.Pages;
using CardManager.ViewModels.GradingViewModels;

namespace CardManager.Components.UtilityViews;

public partial class GradingInputRow<TScale> : BaseInjectView<IGradingInputRowViewModel<TScale>>
    where TScale : struct, Enum
{
}
