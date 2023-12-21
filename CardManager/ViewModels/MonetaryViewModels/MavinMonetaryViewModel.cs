using CardManager.Models.MonetaryData;

namespace CardManager.ViewModels.MonetaryViewModels;

public interface IMavinMonetaryViewModel
{
    double AveragePrice { get; }
    double MaxPrice { get; }
    double MinPrice { get; }

    string ToString();
}

public class MavinMonetaryViewModel(IMavinMonetaryData model) : IMavinMonetaryViewModel
{
    private readonly IMavinMonetaryData model = model;

    public double AveragePrice => model.AveragePrice;

    public double MinPrice => model.MinPrice;

    public double MaxPrice => model.MaxPrice;

    public override string ToString() => $"Min: {this.MinPrice}, Avg: {this.AveragePrice}, Max: {this.MaxPrice}";
}
