namespace CardManager.Models.MonetaryData;

public interface IMonetaryData
{
    IMavinMonetaryData Mavin { get; set; }

    string ToString();
}

public class MonetaryData(IMavinMonetaryData mavinMonetaryData) : IMonetaryData
{
    public IMavinMonetaryData Mavin { get; set; } = mavinMonetaryData;

    public static MonetaryData Default => new(new MavinMonetaryData());

    public override string ToString()
        => Mavin.ToString() ?? string.Empty;
}