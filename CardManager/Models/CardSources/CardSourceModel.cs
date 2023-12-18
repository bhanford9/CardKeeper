using CardManager.Models.CardSources.CardSourceScrapingModels;
using CardManager.Models.CardSources.CardSourceUrlModels;

namespace CardManager.Models.CardSources;

public abstract class CardSourceModel
{
    public abstract ICardSourceScrapingModel CardSourceScraping { get; }

    public abstract ICardSourceUrlModel CardSourceUrl { get; }
}
