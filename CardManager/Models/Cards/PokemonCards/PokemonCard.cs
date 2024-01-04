using CardManager.Models.Grading.BeckettGrading;
using CardManager.Models.Grading.CgcGrading;
using CardManager.Models.Grading.PsaGrading;
using CardManager.SerializationDtos.Cards.PokemonCards;
using CardManager.Services;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;
using static CardManager.Models.Cards.PokemonCards.PokemonCard;

namespace CardManager.Models.Cards.PokemonCards;

public interface IPokemonCard : ICard<PokemonCardDto>
{
    event AppraisalReceivedHandler? AppraisalReceived;

    string Number { get; set; }
    int CreationYear { get; set; }
    PokemonHolographic Holographic { get; set; }
    PokemonRarity Rarity { get; set; }
    PokemonSeries Series { get; set; }
    ElementType Type { get; set; }

    Task RetrieveAppraisal();
}

public class PokemonCard(IWebScrapingService webScrapingService) : Card<PokemonCardDto>, IPokemonCard
{
    private readonly IWebScrapingService webScrapingService = webScrapingService;

    public delegate void AppraisalReceivedHandler();

    public event AppraisalReceivedHandler? AppraisalReceived;

    public string Number { get; set; } = string.Empty;

    public int CreationYear { get; set; } = 9999;

    public PokemonSeries Series { get; set; }

    public PokemonRarity Rarity { get; set; }

    public PokemonHolographic Holographic { get; set; }

    public ElementType Type { get; set; }

    public async Task RetrieveAppraisal()
    {
        var result = await this.webScrapingService.Scrape(new MavinScrapingParams()
        {
            SearchName = this.Name,
            SearchNumber = this.Number,
        });

        this.Monetary.Mavin.MaxPrice = result.MaxCard.SoldFor;
        this.Monetary.Mavin.MinPrice = result.MinCard.SoldFor;
        this.Monetary.Mavin.AveragePrice = result.AverageWorth;
        this.AppraisalReceived?.Invoke();
    }

    public override PokemonCardDto ToDto() => new()
    {
        Id = this.Id,
        Name = this.Name,
        Number = this.Number,
        CreationYear = this.CreationYear,
        Holographic = this.Holographic,
        Rarity = this.Rarity,
        Series = this.Series,
        Type = this.Type,
        Grade = this.Grade switch
        {
            IBeckettGrade b => b.ToDto(),
            ICgcGrade c => c.ToDto(),
            IPsaGrade p => p.ToDto(),
            _ => throw new ArgumentOutOfRangeException(),
        },
        Monetary = this.Monetary.ToDto(),
        StorageSpec = this.StorageSpec.ToDto(),
    };
}