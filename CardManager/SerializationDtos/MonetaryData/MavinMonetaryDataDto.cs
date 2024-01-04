namespace CardManager.SerializationDtos.MonetaryData;

public class MavinMonetaryDataDto : IModelSerialization
{
    public double AveragePrice { get; set; }
    public double MaxPrice { get; set; }
    public double MinPrice { get; set; }
}
