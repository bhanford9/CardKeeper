namespace CardManager.SerializationDtos.MonetaryData;

public class MonetaryDataDto : IModelSerialization
{
    public MavinMonetaryDataDto Mavin { get; set; } = default!;
}
