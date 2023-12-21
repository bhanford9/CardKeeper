using WebScraping.ScrapeOutputs;

namespace WebScraping.ScrapingResults;

public interface IScrapingResult : IScrapeOutput
{
    bool Success { get; }
    object ScrapedValue { get; }
    IReadOnlyCollection<string> Failures { get; }
}

public class ScrapingResult<T>(bool success, T scrapedValue) : IScrapingResult where T : IScrapeOutput
{
    public bool Success { get; } = success;
    public T ScrapedValue { get; } = scrapedValue;
    object IScrapingResult.ScrapedValue => ScrapedValue;
    public IReadOnlyCollection<string> Failures { get; init; } = new List<string>();
}
