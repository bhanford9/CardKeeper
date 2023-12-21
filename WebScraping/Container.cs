using Microsoft.Extensions.DependencyInjection;
using WebScraping.WebScrapers;
using WebScraping.WebScrapingParameters;

namespace WebScraping;

public class Container
{
    public static void Register(IServiceCollection builder)
    {
        ScrapingParamsContainer.Register(builder);
        WebScrapersContainer.Register(builder);

        builder
            .AddTransient<IScrapingPackage, ScrapingPackage>()
            .AddSingleton<IWebScraperRepository, WebScraperRepository>()
            .AddSingleton<IWebScrapingExecutive, WebScrapingExecutive>()
            .AddSingleton<IWebScrapingApi, WebScrapingApi>();
    }
}
