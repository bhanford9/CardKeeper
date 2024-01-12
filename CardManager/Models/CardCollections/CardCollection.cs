using System.Xml.Linq;
using CardManager.Models.Cards;
using CardManager.SerializationDtos;
using SerializationServices;
using static CardManager.Models.CardCollections.PokemonCardCollectionEvents;

namespace CardManager.Models.CardCollections;

public interface ICardCollection<TCard, TCardDto>
    where TCardDto : IModelSerialization
    where TCard : ICard, ISerializableModel<TCardDto>
{
    event CustomCollectionsChangedHandler? CustomCollectionsChanged;
    event CustomCollectionAddedHandler? CustomCollectionAdded;
    event CustomCollectionUpdatedHandler? CustomCollectionUpdated;
    List<TCard> Cards { get; set; }

    List<TCardDto> CardDtos { get; }
    Dictionary<string, List<Guid>> CustomCollections { get; set; }

    void SaveFullCollection();
    void SaveCustomCollections();
    void LoadMasterCardList();
    void LoadCustomCollections();
    void AddCustomCollection(string name);
    void AddToCustomCollection(string collectionName, IEnumerable<Guid> cardIds);
    void RemoveFromCustomCollection(string collectionName, Guid id);
}

public abstract class CardCollection<TCard, TCardDto>
    where TCardDto : IModelSerialization
    where TCard : ICard, ISerializableModel<TCardDto>
{
    protected readonly string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    protected readonly ISerializationExecutive serializer;
    protected Dictionary<string, List<Guid>> customCollections = [];

    public event CustomCollectionsChangedHandler? CustomCollectionsChanged;
    public event CustomCollectionAddedHandler? CustomCollectionAdded;
    public event CustomCollectionUpdatedHandler? CustomCollectionUpdated;

    public CardCollection(ISerializationExecutive serializationExecutive)
    {
        this.serializer = serializationExecutive;
    }

    // TODO --> get this into a file path manager
    protected string StoredDirectoryPath => Path.Combine(this.myDocs, "CardManager");
    protected string StoredPath => Path.Combine(this.StoredDirectoryPath, $"{this.CollectionId}.json");
    protected string CustomCollectionsPath => Path.Combine(this.StoredDirectoryPath, "CustomCollections.json");

    public List<TCard> Cards { get; set; } = [];

    public List<TCardDto> CardDtos => this.Cards.Select(x => x.ToDto()).ToList();

    public abstract string CollectionId { get; }

    public Dictionary<string, List<Guid>> CustomCollections
    {
        get => this.customCollections;
        set
        {
            this.customCollections = value.ToDictionary(x => x.Key, x => x.Value);
            this.CustomCollectionsChanged?.Invoke();
        }
    }

    public abstract void LoadMasterCardList();

    public void SaveFullCollection()
    {
        this.serializer.JsonSerializeToFile(this.CardDtos, this.StoredPath);
    }

    public void SaveCustomCollections()
    {
        this.serializer.JsonSerializeToFile(this.CustomCollections, this.CustomCollectionsPath);
    }

    public void AddCustomCollection(string name)
    {
        this.customCollections.Add(name, []);
        this.CustomCollectionAdded?.Invoke(name);
    }

    public void AddToCustomCollection(string collectionName, IEnumerable<Guid> cardIds)
    {
        this.customCollections[collectionName].AddRange(cardIds);
        this.CustomCollectionUpdated?.Invoke(collectionName, cardIds);
    }

    public void RemoveFromCustomCollection(string collectionName, Guid id)
    {
        if (this.customCollections.TryGetValue(collectionName, out List<Guid>? value))
        {
            value.Remove(id);
            this.CustomCollectionAdded?.Invoke(collectionName); // piggy-back on similar eventS
        }        
    }

    public void LoadCustomCollections()
    {
        if (File.Exists(this.CustomCollectionsPath))
        {
            this.CustomCollections =
                this.serializer.JsonDeserializeFromFile<Dictionary<string, List<Guid>>>(
                    this.CustomCollectionsPath);
        }
    }
}
