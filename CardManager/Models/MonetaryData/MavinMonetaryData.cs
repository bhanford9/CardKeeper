using CardManager.SerializationDtos.MonetaryData;

namespace CardManager.Models.MonetaryData;

public interface IMavinMonetaryData : ISerializableModel<MavinMonetaryDataDto>
{
    double AveragePrice { get; set; }
    double MaxPrice { get; set; }
    double MinPrice { get; set; }
}

public class MavinMonetaryData : IMavinMonetaryData
{
    public double AveragePrice { get; set; }

    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }

    public override string ToString() => $"Min: {this.MinPrice}, Avg: {this.AveragePrice}, Max: {this.MaxPrice}";
    
    public MavinMonetaryDataDto ToDto() => new()
    {
        AveragePrice = this.AveragePrice,
        MaxPrice = this.MaxPrice,
        MinPrice = this.MinPrice,
    };
}
