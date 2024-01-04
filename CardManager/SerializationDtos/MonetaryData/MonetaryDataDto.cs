namespace CardManager.SerializationDtos.MonetaryData;

public class MonetaryDataDto : IModelSerialization
{
    public MavinMonetaryDataDto Mavin { get; set; } = default!;
}

public static class MonetaryDtoUtils
{
    public static Models.MonetaryData.MonetaryData ToModel(this MonetaryDataDto dto) => new(null)
    {
        Mavin = dto.Mavin.ToModel(),
    };

    public static Models.MonetaryData.MavinMonetaryData ToModel(this MavinMonetaryDataDto dto) => new()
    {
        AveragePrice = dto.AveragePrice,
        MaxPrice = dto.MaxPrice,
        MinPrice = dto.MinPrice,
    };
}