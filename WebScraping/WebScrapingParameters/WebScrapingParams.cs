namespace WebScraping.WebScrapingParameters;

public interface IWebScrapingParams
{
    string BaseUrl { get; }
}

public abstract class WebScrapingParams : IWebScrapingParams
{
    public abstract string BaseUrl { get; }
}
