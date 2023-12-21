namespace CardManager.Models.MonetaryData;

public interface IMavinMonetaryData : IMonetaryData
{
    double AveragePrice { get; set; }
    double MaxPrice { get; set; }
    double MinPrice { get; set; }
}

public class MavinMonetaryData : MonetaryData, IMavinMonetaryData
{
    public double AveragePrice { get; set; }

    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }

    public override string ToString() => $"Min: {this.MinPrice}, Avg: {this.AveragePrice}, Max: {this.MaxPrice}";
}
