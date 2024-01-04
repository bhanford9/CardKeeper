using CardManager.Models.Grading;

namespace CardManager.ViewModels.GradingViewModels;

public interface IGradingAggregateViewModel
{
    IBeckettGradingViewModel BeckettGrading { get; set; }
    ICgcGradingViewModel CgcGrading { get; set; }
    IPsaGradingViewModel PsaGrading { get; set; }
    GradingHost SelectedGradingHost { get; set; }

    ICardGrade ToModel();
    string ToString();
}

public class GradingAggregateViewModel(
    IBeckettGradingViewModel beckettGrading,
    ICgcGradingViewModel cgcGrading,
    IPsaGradingViewModel psaGrading)
    : IGradingAggregateViewModel
{
    public GradingHost SelectedGradingHost { get; set; }

    public IBeckettGradingViewModel BeckettGrading { get; set; } = beckettGrading;

    public ICgcGradingViewModel CgcGrading { get; set; } = cgcGrading;

    public IPsaGradingViewModel PsaGrading { get; set; } = psaGrading;

    public ICardGrade ToModel() => this.SelectedGradingHost switch
        {
            GradingHost.Beckett => this.BeckettGrading.ToModel(),
            GradingHost.Cgc => this.CgcGrading.ToModel(),
            GradingHost.Psa => this.PsaGrading.ToModel(),
            _ => throw new ArgumentOutOfRangeException()
        };

    public override string ToString() =>
        this.SelectedGradingHost switch
        {
            GradingHost.Beckett => this.BeckettGrading?.ToString(),
            GradingHost.Cgc => this.CgcGrading?.ToString(),
            GradingHost.Psa => this.PsaGrading?.ToString(),
            _ => "Not Graded"
        } ?? "Not Graded";
}
