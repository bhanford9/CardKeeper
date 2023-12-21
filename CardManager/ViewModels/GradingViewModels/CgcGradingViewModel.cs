using CardManager.Models.Grading.CgcGrading;

namespace CardManager.ViewModels.GradingViewModels;

public interface ICgcGradingViewModel : IViewModel
{
    GradingInputRowViewModel<CgcScale> Centering { get; }
    GradingInputRowViewModel<CgcScale> Corners { get; }
    GradingInputRowViewModel<CgcScale> Edges { get; }
    GradingInputRowViewModel<CgcScale> Overall { get; }
    GradingInputRowViewModel<CgcScale> Surface { get; }

    string ToString();
}

public class CgcGradingViewModel : BaseViewModel, ICgcGradingViewModel
{
    public GradingInputRowViewModel<CgcScale> Centering { get; } = new("Centering");

    public GradingInputRowViewModel<CgcScale> Corners { get; } = new("Corners");

    public GradingInputRowViewModel<CgcScale> Edges { get; } = new("Edges");

    public GradingInputRowViewModel<CgcScale> Surface { get; } = new("Surface");

    public GradingInputRowViewModel<CgcScale> Overall { get; } = new("Overall");

    public override string ToString() => this.Overall?.ToString() ?? "Not Graded";
}
