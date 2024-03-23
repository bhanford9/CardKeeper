namespace CardManager.Services;

public static class ServicesContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection builder) =>
        builder.AddSingleton<IWebScrapingService, WebScrapingService>();
}
