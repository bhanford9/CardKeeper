using CardManager.Models.Grading.CgcGrading;

namespace CardManager.ViewModels.GradingViewModels;

public interface ICgcGradingViewModel : IViewModel
{
    GradingInputRowViewModel<CgcScale> Centering { get; }
    GradingInputRowViewModel<CgcScale> Corners { get; }
    GradingInputRowViewModel<CgcScale> Edges { get; }
    GradingInputRowViewModel<CgcScale> Overall { get; }
    GradingInputRowViewModel<CgcScale> Surface { get; }

    ICgcGrade ToModel();
    string ToString();
}

public class CgcGradingViewModel : BaseViewModel, ICgcGradingViewModel
{
    public CgcGradingViewModel(ICgcGrade model)
    {
        this.Centering.SelectorViewModel.SelectedValue = model.Centering;
        this.Corners.SelectorViewModel.SelectedValue = model.Corners;
        this.Edges.SelectorViewModel.SelectedValue = model.Edges;
        this.Surface.SelectorViewModel.SelectedValue = model.Surface;
        this.Overall.SelectorViewModel.SelectedValue = model.Overall;
    }

    public GradingInputRowViewModel<CgcScale> Centering { get; } = new("Centering");

    public GradingInputRowViewModel<CgcScale> Corners { get; } = new("Corners");

    public GradingInputRowViewModel<CgcScale> Edges { get; } = new("Edges");

    public GradingInputRowViewModel<CgcScale> Surface { get; } = new("Surface");

    public GradingInputRowViewModel<CgcScale> Overall { get; } = new("Overall");

    public ICgcGrade ToModel() =>
        new CgcGrade()
        {
            Centering = this.Centering.SelectorViewModel.SelectedValue,
            Corners = this.Corners.SelectorViewModel.SelectedValue,
            Edges = this.Edges.SelectorViewModel.SelectedValue,
            Surface = this.Surface.SelectorViewModel.SelectedValue,
            Overall = this.Overall.SelectorViewModel.SelectedValue,
        };

    public override string ToString() => this.Overall?.ToString() ?? "Not Graded";
}
