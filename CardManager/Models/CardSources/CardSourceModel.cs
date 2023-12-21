using CardManager.Models.CardSources.CardSourceScrapingModels;
using CardManager.Models.CardSources.CardSourceUrlModels;

namespace CardManager.Models.CardSources;

public interface ICardSourceModel
{
    ICardSourceScrapingModel CardSourceScraping { get; }
    ICardSourceUrlModel CardSourceUrl { get; }
}

public abstract class CardSourceModel : ICardSourceModel
{
    public abstract ICardSourceScrapingModel CardSourceScraping { get; }

    public abstract ICardSourceUrlModel CardSourceUrl { get; }
}

public interface IEmptyCardSourceModel : ICardSourceModel { }

public class EmptyCardSourceModel : CardSourceModel, IEmptyCardSourceModel
{
    public override ICardSourceScrapingModel CardSourceScraping => CardSourceScrapingModel.Default;

    public override ICardSourceUrlModel CardSourceUrl => CardSourceUrlModel.Default;
}
