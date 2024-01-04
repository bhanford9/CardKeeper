using CardManager.Models.Cards;
using CardManager.SerializationDtos;
using SerializationServices;

namespace CardManager.Models.CardCollections;

public interface ICardCollection<TCard, TCardDto>
    where TCardDto : IModelSerialization
    where TCard : ICard, ISerializableModel<TCardDto>
{
    List<TCard> Cards { get; set; }

    List<TCardDto> CardDtos { get; }

    public void Save();

    public void Load();
}

public abstract class CardCollection<TCard, TCardDto>
    where TCardDto : IModelSerialization
    where TCard : ICard, ISerializableModel<TCardDto>
{
    protected readonly ISerializationExecutive serializer;

    public CardCollection(ISerializationExecutive serializationExecutive)
    {
        this.serializer = serializationExecutive;
        this.Load();
    }

    public List<TCard> Cards { get; set; } = [];

    public List<TCardDto> CardDtos => this.Cards.Select(x => x.ToDto()).ToList();

    public abstract string CollectionId { get; }

    // TODO --> get this into a file path manager
    protected string StoredPath => @$"C:\Users\bmhanford\Documents\CardManager\{this.CollectionId}.json";

    public void Save()
    {
        this.serializer.JsonSerializeToFile(this.CardDtos, this.StoredPath);
    }

    public abstract void Load();
}
