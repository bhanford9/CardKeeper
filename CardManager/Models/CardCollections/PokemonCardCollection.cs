using CardManager.Models.Cards.PokemonCards;
using CardManager.SerializationDtos.Cards.PokemonCards;
using CardManager.Services;
using SerializationServices;

namespace CardManager.Models.CardCollections;

public static class PokemonCardCollectionEvents
{
    public delegate void CustomCollectionsChangedHandler();
    public delegate void CustomCollectionAddedHandler(string name);
    public delegate void CustomCollectionUpdatedHandler(string name, IEnumerable<Guid> cardIds);
}

public interface IPokemonCardCollection : ICardCollection<IPokemonCard, PokemonCardDto>
{
}

public class PokemonCardCollection(
    ISerializationExecutive serializationExecutive,
    IWebScrapingService webScrapingService)
    : CardCollection<IPokemonCard, PokemonCardDto>(serializationExecutive), IPokemonCardCollection
{
    private readonly IWebScrapingService webScrapingService = webScrapingService;

    public override string CollectionId => "PokemonCards";

    public override void LoadMasterCardList()
    {
        if (File.Exists(this.StoredPath))
        {
            this.Cards = this.serializer
                .JsonDeserializeFromFile<List<PokemonCardDto>>(this.StoredPath)
                .Select(dto => dto.ToModel(this.webScrapingService))
                .ToList();
        }
    }
}
