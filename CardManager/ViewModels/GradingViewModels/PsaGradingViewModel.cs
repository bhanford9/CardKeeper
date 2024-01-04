using CardManager.Models.Grading.PsaGrading;

namespace CardManager.ViewModels.GradingViewModels;

public interface IPsaGradingViewModel : IViewModel
{
    GradingInputRowViewModel<PsaScale> Overall { get; }

    IPsaGrade ToModel();
    string ToString();
}

public class PsaGradingViewModel : BaseViewModel, IPsaGradingViewModel
{
    public PsaGradingViewModel(IPsaGrade model)
    {
        this.Overall.SelectorViewModel.SelectedValue = model.Score;
    }

    public GradingInputRowViewModel<PsaScale> Overall { get; set; } = new("Overall");

    public IPsaGrade ToModel() => new PsaGrade() { Score = this.Overall.SelectorViewModel.SelectedValue };

    public override string ToString() => this.Overall?.ToString() ?? "Not Graded";
}
