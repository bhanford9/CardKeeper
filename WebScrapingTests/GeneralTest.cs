using WebScraping.ScrapeOutputs.PokemonScrapeOutputs;
using WebScraping.WebScrapers.PokemonScrapers;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;

namespace WebScrapingTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var mavinScraper = new MavinScraper();
        var result = await mavinScraper.Scrape(new MavinScrapingParams()
        {
            SearchName = "Vaporeon",
            SearchNumber = "33/144",
        });

        Assert.That(result.Success, Is.True);
        Assert.That(result.Failures, Is.Empty);
        Assert.That(result.ScrapedValue, Is.TypeOf<MavinScrapeOutput>());

        var scrapeOutput = result.ScrapedValue as MavinScrapeOutput;

        Assert.That(scrapeOutput, Is.Not.Null);
        Assert.That(scrapeOutput.Name, Is.EqualTo("Vaporeon"));
        Assert.That(scrapeOutput.Number, Is.EqualTo("33/144"));
        Assert.That(scrapeOutput.AverageWorth, Is.GreaterThan(0));
        Assert.That(scrapeOutput.MinCard, Is.Not.Null);
        Assert.That(scrapeOutput.MinCard.Description, Is.Not.Null.Or.Empty);
        Assert.That(scrapeOutput.MinCard.Url, Is.Not.Null.Or.Empty);
        Assert.That(scrapeOutput.MinCard.SoldFor, Is.GreaterThan(0));
        Assert.That(scrapeOutput.MaxCard, Is.Not.Null);
        Assert.That(scrapeOutput.MaxCard.Description, Is.Not.Null.Or.Empty);
        Assert.That(scrapeOutput.MaxCard.Url, Is.Not.Null.Or.Empty);
        Assert.That(scrapeOutput.MaxCard.SoldFor, Is.GreaterThan(0));
    }
}