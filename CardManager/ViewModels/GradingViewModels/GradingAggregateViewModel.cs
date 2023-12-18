using CardManager.Models.Grading;

namespace CardManager.ViewModels.GradingViewModels;

public interface IGradingAggregateViewModel
{
    IBeckettGradingViewModel BeckettGrading { get; set; }
    ICgcGradingViewModel CgcGrading { get; set; }
    IPsaGradingViewModel PsaGrading { get; set; }
    GradingHost SelectedGradingHost { get; set; }

    string ToString();
}

public class GradingAggregateViewModel : IGradingAggregateViewModel
{
    public GradingHost SelectedGradingHost { get; set; }

    public IBeckettGradingViewModel BeckettGrading { get; set; } = new BeckettGradingViewModel();

    public ICgcGradingViewModel CgcGrading { get; set; } = new CgcGradingViewModel();

    public IPsaGradingViewModel PsaGrading { get; set; } = new PsaGradingViewModel();

    public override string ToString() =>
        this.SelectedGradingHost switch
        {
            GradingHost.Beckett => this.BeckettGrading?.ToString(),
            GradingHost.Cgc => this.CgcGrading?.ToString(),
            GradingHost.Psa => this.PsaGrading?.ToString(),
            _ => "Not Graded"
        } ?? "Not Graded";
}
