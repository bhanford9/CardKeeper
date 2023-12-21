using Microsoft.Extensions.DependencyInjection;
using WebScraping.WebScrapers;
using WebScraping.WebScrapingParameters;

namespace WebScraping;

public static class Container
{
    public static IServiceCollection RegisterWebScraping(this IServiceCollection builder)
        => builder
            .RegisterScrapers()
            .RegisterScrapingParams()
            .AddTransient<IScrapingPackage, ScrapingPackage>()
            .AddSingleton<IWebScraperRepository, WebScraperRepository>()
            .AddSingleton<IWebScrapingExecutive, WebScrapingExecutive>()
            .AddSingleton<IWebScrapingApi, WebScrapingApi>();
    
}
