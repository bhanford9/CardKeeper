using CardManager.Models.MonetaryData;

namespace CardManager.ViewModels.MonetaryViewModels;

public interface IMonetaryAggregateViewModel
{
    IMavinMonetaryViewModel MavinViewModel { get; set; }

    string ToString();
}

public class MonetaryAggregateViewModel(
    IViewModelsFactory viewModelsFactory,
    IMavinMonetaryData mavinModel) : IMonetaryAggregateViewModel
{
    public IMavinMonetaryViewModel MavinViewModel { get; set; }
        = viewModelsFactory.NewMavinMonetary(mavinModel);

    public override string ToString()
    {
        return MavinViewModel.ToString();
    }
}
