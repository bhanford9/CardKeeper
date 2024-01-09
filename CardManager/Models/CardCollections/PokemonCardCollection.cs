using CardManager.Models.Cards.PokemonCards;
using CardManager.SerializationDtos.Cards.PokemonCards;
using CardManager.Services;
using SerializationServices;

namespace CardManager.Models.CardCollections;

public interface IPokemonCardCollection : ICardCollection<IPokemonCard, PokemonCardDto>
{
    Dictionary<string, List<Guid>> CustomCollections { get; set; }
}

public class PokemonCardCollection(
    ISerializationExecutive serializationExecutive,
    IWebScrapingService webScrapingService)
    : CardCollection<IPokemonCard, PokemonCardDto>(serializationExecutive), IPokemonCardCollection
{
    private readonly IWebScrapingService webScrapingService = webScrapingService;

    protected string CustomCollectionsPath => Path.Combine(this.StoredDirectoryPath, "CustomCollections.json");

    public override string CollectionId => "PokemonCards";

    public Dictionary<string, List<Guid>> CustomCollections { get; set; } = [];

    public override void Load()
    {
        if (File.Exists(this.StoredPath))
        {
            this.Cards = this.serializer
                .JsonDeserializeFromFile<List<PokemonCardDto>>(this.StoredPath)
                .Select(dto => dto.ToModel(this.webScrapingService))
                .ToList();

            if (File.Exists(this.CustomCollectionsPath))
            {
                this.CustomCollections = 
                    this.serializer.JsonDeserializeFromFile<Dictionary<string, List<Guid>>>(
                        this.CustomCollectionsPath);
            }
        }
    }

    protected override void InternalSave()
    {
        this.serializer.JsonSerializeToFile(this.CustomCollections, this.CustomCollectionsPath);
    }
}
