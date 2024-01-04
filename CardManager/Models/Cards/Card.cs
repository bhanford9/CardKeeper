using CardManager.Models.Grading;
using CardManager.Models.MonetaryData;
using CardManager.Models.StorageSpecifications;
using CardManager.SerializationDtos;

namespace CardManager.Models.Cards;

public interface ICard
{
    ICardGrade Grade { get; set; }
    Guid Id { get; set; }
    IMonetaryData Monetary { get; set; }
    string Name { get; set; }
    IStorageSpecification StorageSpec { get; set; }
}

public abstract class Card<T> :
    ICard,
    ISerializableModel<T> where T : IModelSerialization
{
    public Guid Id { get; set; } = default;

    public string Name { get; set; } = string.Empty;

    public IStorageSpecification StorageSpec { get; set; } = StorageSpecification.Default;

    public ICardGrade Grade { get; set; } = Ungraded.Get;

    public IMonetaryData Monetary { get; set; } = MonetaryData.MonetaryData.Default;

    public abstract T ToDto();
}
