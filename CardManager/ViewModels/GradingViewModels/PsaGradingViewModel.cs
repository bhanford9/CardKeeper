using CardManager.Models.Grading.PsaGrading;

namespace CardManager.ViewModels.GradingViewModels;

public interface IPsaGradingViewModel : IViewModel
{
    GradingInputRowViewModel<PsaScale> Overall { get; }

    string ToString();
}

public class PsaGradingViewModel : BaseViewModel, IPsaGradingViewModel
{
    public GradingInputRowViewModel<PsaScale> Overall { get; set; } = new("Overall");

    public override string ToString() => this.Overall?.ToString() ?? "Not Graded";
}
