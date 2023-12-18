using CardManager.Components.Pages;
using CardManager.ViewModels.UtilityViewModels;
using Microsoft.AspNetCore.Components;

namespace CardManager.Components.UtilityViews;

public partial class EnumSelectorView<TEnum> : BaseInjectView<IEnumSelectorViewModel<TEnum>>
    where TEnum : struct, Enum
{
    [Parameter]
    public string CssClasses { get; set; } = string.Empty;
}
