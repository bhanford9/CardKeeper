using System.Globalization;
using System.Web;
using HtmlAgilityPack;
using WebScraping.ScrapeOutputs.PokemonScrapeOutputs;
using WebScraping.ScrapingResults;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;

namespace WebScraping.WebScrapers.PokemonScrapers;
internal class MavinScraper : WebScraper<MavinScrapingParams>
{
    protected override bool InternalCanScrape(MavinScrapingParams scraperParams)
        => !string.IsNullOrEmpty(scraperParams.SearchName)
        || !string.IsNullOrEmpty(scraperParams.SearchNumber);

    protected override IScrapingResult InternalScrape(MavinScrapingParams scraperParams, int attempt = 0)
    {
        string searchParameters = $"{scraperParams.SearchName} {scraperParams.SearchNumber}";
        string encodedParams = HttpUtility.UrlEncode(searchParameters);
        string url = $"{scraperParams.SearchUrl}{encodedParams}&bt=sold";

        HtmlWeb web = new();
        HtmlDocument doc = web.Load(url);
        HtmlNode worthBlock = doc.GetElementbyId("answer");
        List<HtmlNode> worthItems = [];

        try
        {
            worthItems = worthBlock.ChildNodes
                .Where(x => x.NodeType == HtmlNodeType.Element)
                .ToList();
        }
        catch
        {
            return new ScrapingResult<MavinScrapeOutput>(false, new())
            {
                Failures = ["Problem parsing URL Nodes"]
            };
        }

        if (worthItems == null || worthItems.Count < 4)
        {
            return new ScrapingResult<MavinScrapeOutput>(false, new())
            {
                Failures = ["Failed to find summary text with 3 'a' tags."]
            };
        }

        const NumberStyles monetaryParse = NumberStyles.AllowCurrencySymbol | NumberStyles.Currency;

        HtmlNode searchResult = worthItems[0];
        string searchString = searchResult.InnerHtml;
        int lastIndexOfSpace = searchString.LastIndexOf(' ');

        HtmlNode averageNode = worthItems[1];
        double averageWorth = double.Parse(averageNode.InnerHtml, monetaryParse);

        HtmlNode minItemNode = worthItems[2];
        double minWorth = double.Parse(minItemNode.InnerHtml, monetaryParse);
        string minDescription = minItemNode.GetAttributeValue("alt", "");
        string minhref = minItemNode.GetAttributeValue("href", "");

        HtmlNode maxItemNode = worthItems[3];
        double maxWorth = double.Parse(maxItemNode.InnerHtml, monetaryParse);
        string maxDescription = minItemNode.GetAttributeValue("alt", "");
        string maxhref = minItemNode.GetAttributeValue("href", "");

        return new ScrapingResult<MavinScrapeOutput>(
            success: true,
            scrapedValue: new MavinScrapeOutput()
            {
                Name = searchString[..lastIndexOfSpace],
                Number = searchString[(lastIndexOfSpace + 1)..],
                AverageWorth = averageWorth,
                MinCard = new MavinCardOutput(minhref)
                {
                    Description = minDescription,
                    SoldFor = minWorth,
                },
                MaxCard = new MavinCardOutput(maxhref)
                {
                    Description = maxDescription,
                    SoldFor = maxWorth,
                },
            });
    }
}
