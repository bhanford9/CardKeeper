using Microsoft.Extensions.DependencyInjection;
using WebScraping.ScrapeOutputs.PokemonScrapeOutputs;

namespace WebScraping.ScrapeOutputs;
internal class ScrapeOutputsContainer
{
    public static void Register(IServiceCollection builder)
    {
        builder
            .AddTransient<IScrapeOutput, MavinScrapeOutput>()
            .AddTransient<IMavinScrapeOutput, MavinScrapeOutput>()
            .AddTransient<IScrapeOutput, MavinCardOutput>()
            .AddTransient<IMavinCardOutput, MavinCardOutput>();
    }
}
