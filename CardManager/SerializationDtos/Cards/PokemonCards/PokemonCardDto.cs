using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.MonetaryData;
using CardManager.Models.StorageSpecifications;
using CardManager.SerializationDtos.Grading;
using CardManager.SerializationDtos.MonetaryData;
using CardManager.SerializationDtos.StorageSpecifications;

namespace CardManager.SerializationDtos.Cards.PokemonCards;

public class PokemonCardDto : IModelSerialization
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public int CreationYear { get; set; }
    public PokemonHolographic Holographic { get; set; }
    public PokemonRarity Rarity { get; set; }
    public PokemonSeries Series { get; set; }
    public ElementType Type { get; set; }
    public ICardGradeDto Grade { get; set; } = default!;
    public MonetaryDataDto Monetary { get; set; } = default!;
    public StorageSpecificationDto StorageSpec { get; set; } = default!;
}
