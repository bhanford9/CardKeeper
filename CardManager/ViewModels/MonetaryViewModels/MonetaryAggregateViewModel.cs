using CardManager.Models.MonetaryData;

namespace CardManager.ViewModels.MonetaryViewModels;

public interface IMonetaryAggregateViewModel
{
    IMavinMonetaryViewModel MonetaryViewModel { get; }
}

public class MonetaryAggregateViewModel(
    IViewModelsFactory viewModelsFactory,
    IMavinMonetaryData mavinModel) : IMonetaryAggregateViewModel
{
    public IMavinMonetaryViewModel MonetaryViewModel { get; }
        = viewModelsFactory.NewMavinMonetaryViewModel(mavinModel);
}
