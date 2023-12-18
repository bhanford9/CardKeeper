using CardManager.Models.Grading.PsaGrading;

namespace CardManager.ViewModels.GradingViewModels;

public interface IPsaGradingViewModel : IViewModel
{
    GradingInputRowViewModel<PsaScale> Overall { get; }
}

public class PsaGradingViewModel : BaseViewModel, IPsaGradingViewModel
{
    public GradingInputRowViewModel<PsaScale> Overall { get; set; } = new("Overall");
}
