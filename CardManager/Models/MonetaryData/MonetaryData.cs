using CardManager.SerializationDtos.MonetaryData;

namespace CardManager.Models.MonetaryData;

public interface IMonetaryData : ISerializableModel<MonetaryDataDto>
{
    IMavinMonetaryData Mavin { get; set; }

    string ToString();
}

public class MonetaryData(IMavinMonetaryData mavinMonetaryData) : IMonetaryData
{
    public IMavinMonetaryData Mavin { get; set; } = mavinMonetaryData;

    public static MonetaryData Default => new(new MavinMonetaryData());

    public override string ToString()
        => this.Mavin.ToString() ?? string.Empty;
    public MonetaryDataDto ToDto() => new()
    {
        Mavin = this.Mavin.ToDto(),
    };
}