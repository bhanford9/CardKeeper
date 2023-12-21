using Microsoft.Extensions.DependencyInjection;
using WebScraping.WebScrapers.PokemonScrapers;

namespace WebScraping.WebScrapers;
internal static class WebScrapersContainer
{
    public static IServiceCollection RegisterScrapers(this IServiceCollection builder)
        => builder
            .AddTransient<IWebScraper, MavinScraper>();
}
