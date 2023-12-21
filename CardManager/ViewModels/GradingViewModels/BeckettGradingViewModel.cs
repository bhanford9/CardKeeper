using CardManager.Models.Grading.BeckettGrading;

namespace CardManager.ViewModels.GradingViewModels;

public interface IBeckettGradingViewModel : IViewModel
{
    GradingInputRowViewModel<BeckettScale> Centering { get; }
    GradingInputRowViewModel<BeckettScale> Corners { get; }
    GradingInputRowViewModel<BeckettScale> Edges { get; }
    GradingInputRowViewModel<BeckettScale> Surface { get; }
    GradingInputRowViewModel<BeckettScale> Overall { get; }

    string ToString();
}

public class BeckettGradingViewModel : BaseViewModel, IBeckettGradingViewModel
{
    public GradingInputRowViewModel<BeckettScale> Centering { get; } = new("Centering");

    public GradingInputRowViewModel<BeckettScale> Corners { get; } = new("Corners");

    public GradingInputRowViewModel<BeckettScale> Edges { get; } = new("Edges");

    public GradingInputRowViewModel<BeckettScale> Surface { get; } = new("Surface");

    public GradingInputRowViewModel<BeckettScale> Overall { get; } = new("Overall");

    public override string ToString() => this.Overall?.ToString() ?? "Not Graded";
}
