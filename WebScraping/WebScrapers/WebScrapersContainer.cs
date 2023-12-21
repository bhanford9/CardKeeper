using Microsoft.Extensions.DependencyInjection;
using WebScraping.WebScrapers.PokemonScrapers;

namespace WebScraping.WebScrapers;
internal class WebScrapersContainer
{
    public static void Register(IServiceCollection builder)
    {
        builder
            .AddTransient<IWebScraper, MavinScraper>();
    }
}
