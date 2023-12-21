namespace CardManager.Models.Cards.PokemonCards;

public interface IPokemonCard
{
    int CreationYear { get; set; }
    PokemonHolographic Holographic { get; set; }
    PokemonRarity Rarity { get; set; }
    PokemonSeries Series { get; set; }
    ElementType Type { get; set; }
}

public class PokemonCard : Card, IPokemonCard
{
    public int CreationYear { get; set; } = 9999;

    public PokemonSeries Series { get; set; }

    public PokemonRarity Rarity { get; set; }

    public PokemonHolographic Holographic { get; set; }

    public ElementType Type { get; set; }
}
