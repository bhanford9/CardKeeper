using CardManager.Models.Cards;
using CardManager.SerializationDtos;
using SerializationServices;

namespace CardManager.Models.CardCollections;

public interface ICardCollection<TCard, TCardDto>
    where TCardDto : IModelSerialization
    where TCard : ICard<TCardDto>
{
    List<TCard> Cards { get; set; }

    List<TCardDto> CardDtos { get; }
}

public class CardCollection<TCard, TCardDto>(ISerializationExecutive serializationExecutive)
    where TCardDto : IModelSerialization
    where TCard : ICard<TCardDto>
{
    private readonly ISerializationExecutive serializationExecutive = serializationExecutive;

    public List<TCard> Cards { get; set; } = [];

    public List<TCardDto> CardDtos => this.Cards.Select(x => x.ToDto()).ToList();

    public void Save()
    {
        // TODO 
    }

    public void Load()
    {
        // TODO
    }
}
