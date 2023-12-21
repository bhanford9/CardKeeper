namespace CardManager.Models.MonetaryData;

public interface IEmptyMonetaryData : IMonetaryData { }

public class EmptyMonetaryData : MonetaryData, IEmptyMonetaryData
{
    public static EmptyMonetaryData Default => new();

    public override string ToString() => string.Empty;
}