namespace WebScraping.WebScrapingParameters.PokemonScrapingParameters;

public interface IMavinScrapingParams : IWebScrapingParams
{
    string SearchName { get; set; }
    string SearchNumber { get; set; }
    string SearchUrl { get; }
}

public class MavinScrapingParams : WebScrapingParams, IMavinScrapingParams
{
    public override string BaseUrl { get; } = "https://mavin.io";

    public string SearchUrl { get; } = "https://mavin.io/search?q=";

    public string SearchName { get; set; } = string.Empty;

    public string SearchNumber { get; set; } = string.Empty;
}
