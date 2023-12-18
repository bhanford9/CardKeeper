using CardManager.Models.Grading;
using CardManager.Models.StorageSpecification;

namespace CardManager.Models.Cards.PokemonCards;

public class PokemonCard(Guid id, string name, IStorageSpecification storageSpecification, ICardGrade grade)
    : Card(id, name, storageSpecification, grade)
{
    public int CreationYear { get; set; } = 9999;

    public PokemonSeries Series { get; set; }

    public PokemonRarity Rarity { get; set; }

    public PokemonHolographic Holographic { get; set; }

    public ElementType Type { get; set; }
}
