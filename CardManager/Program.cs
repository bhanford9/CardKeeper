using CardManager.Components;
using CardManager.Models;
using CardManager.Services;
using CardManager.ViewModels;
using CardManager.ViewModels.GradingViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;
using CardManager.ViewModels.UtilityViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SerializationServices;
using WebScraping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazorBootstrap();

// Dependency Injection
builder.Services
    .AddSingleton<IViewModelsFactory, ViewModelsFactory>()
    .AddSingleton<IWebScrapingService, WebScrapingService>()
    .AddTransient<IPokemonCollectionViewModel, PokemonCollectionViewModel>()
    .AddTransient<IMavinMonetaryViewModel, MavinMonetaryViewModel>()
    .AddTransient<IMonetaryAggregateViewModel, MonetaryAggregateViewModel>()
    .AddTransient<IBeckettGradingViewModel, BeckettGradingViewModel>()
    .AddTransient<ICgcGradingViewModel, CgcGradingViewModel>()
    .AddTransient<IGradingAggregateViewModel, GradingAggregateViewModel>()
    .AddTransient(typeof(IGradingInputRowViewModel<>), typeof(GradingInputRowViewModel<>))
    .AddTransient<IPsaGradingViewModel, PsaGradingViewModel>()
    //.AddTransient<IEditCardModalViewModel, EditCardModalViewModel>()
    .AddTransient<IPokemonCardViewModel, PokemonCardViewModel>()
    .AddTransient<IPokemonCollectionViewModel, PokemonCollectionViewModel>()
    .AddTransient(typeof(IEnumSelectorViewModel<>), typeof(EnumSelectorViewModel<>))
    //.AddTransient<IStorageSpecViewModel, StorageSpecViewModel>()
    //.AddTransient<IStorageLocationViewModel, StorageLocationViewModel>()
    //.AddTransient<IStorageMediaViewModel, StorageMediaViewModel>()
    .RegisterModels()
    .RegisterWebScraping()
    .RegisterSerializationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
