namespace CardManager.Models.MonetaryData;

public interface IMonetaryData
{
    string ToString();
}

public abstract class MonetaryData : IMonetaryData
{
    public abstract override string ToString();
}