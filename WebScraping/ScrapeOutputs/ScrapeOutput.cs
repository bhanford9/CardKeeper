using WebScraping.ScrapingResults;

namespace WebScraping.ScrapeOutputs;

public static class ScrapeOutputUtilities
{
    public static T As<T>(this IScrapingResult result) where T : IScrapeOutput => (T)result.ScrapedValue;
}

public interface IScrapeOutput { }

public class ScrapeOutput : IScrapeOutput 
{
}
