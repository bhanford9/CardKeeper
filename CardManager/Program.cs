using CardManager.Components;
using CardManager.Models;
using CardManager.Services;
using CardManager.ViewModels;
using CardManager.ViewModels.GradingViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;
using CardManager.ViewModels.StorageSpecViewModels;
using CardManager.ViewModels.UtilityViewModels;
using CardManager.ViewModels.UtilityViewModels.Filtering;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
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
    .AddTransient<IMavinMonetaryViewModel, MavinMonetaryViewModel>()
    .AddTransient<IMonetaryAggregateViewModel, MonetaryAggregateViewModel>()
    .AddTransient<IBeckettGradingViewModel, BeckettGradingViewModel>()
    .AddTransient<ICgcGradingViewModel, CgcGradingViewModel>()
    .AddTransient<IPsaGradingViewModel, PsaGradingViewModel>()
    .AddTransient<IGradingAggregateViewModel, GradingAggregateViewModel>()
    .AddTransient(typeof(IGradingInputRowViewModel<>), typeof(GradingInputRowViewModel<>))
    .AddTransient<IPokemonCardViewModel, PokemonCardViewModel>()
    .AddTransient<IPokemonCollectionViewModel, PokemonCollectionViewModel>()
    .AddTransient<IPokemonCustomCollectionViewModel, PokemonCustomCollectionViewModel>()
    .AddTransient(typeof(IEnumSelectorViewModel<>), typeof(EnumSelectorViewModel<>))
    .AddTransient<ISleeveLocationViewModel, SleeveLocationViewModel>()
    .AddTransient<IBoxLocationViewModel, BoxLocationViewModel>()
    .AddTransient<IPokemonCollectionsManagerViewModel, PokemonCollectionsManagerViewModel>()
    .AddTransient<IFullCollectionActionPermissionsViewModel, FullCollectionActionPermissionsViewModel>()
    .AddTransient<ICustomCollectionActionPermissionsViewModel, CustomCollectionActionPermissionsViewModel>()

    // Filtering
    .AddTransient<IAddFilterViewModel, AddFilterViewModel>()
    .AddTransient<IFilterCriteria, GraderNameCriteria>()
    .AddTransient<IFilterCriteria, HolographicCriteria>()
    .AddTransient<IFilterCriteria, MonetaryCriteria>()
    .AddTransient<IFilterCriteria, NameFilterCriteria>()
    .AddTransient<IFilterCriteria, NumberFilterCriteria>()
    .AddTransient<IFilterCriteria, RarityCriteria>()
    .AddTransient<IFilterCriteria, SeriesCriteria>()
    .AddTransient<IFilterCriteria, StorageCriteria>()
    .AddTransient<IFilterCriteria, TypeCriteria>()
    .AddTransient<IFilterCriteria, YearFilterCriteria>()
    .AddTransient<IGraderNameCriteria, GraderNameCriteria>()
    .AddTransient<IHolographicCriteria, HolographicCriteria>()
    .AddTransient<IMonetaryCriteria, MonetaryCriteria>()
    .AddTransient<INameFilterCriteria, NameFilterCriteria>()
    .AddTransient<INumberFilterCriteria, NumberFilterCriteria>()
    .AddTransient<IRarityCriteria, RarityCriteria>()
    .AddTransient<ISeriesCriteria, SeriesCriteria>()
    .AddTransient<IStorageCriteria, StorageCriteria>()
    .AddTransient<ITypeCriteria, TypeCriteria>()
    .AddTransient<IYearFilterCriteria, YearFilterCriteria>()
    .AddTransient<IFilterPackageBuilder<IPokemonCardViewModel>, NameFilterPackageBuilder>()
    .AddTransient<IFilterPackageBuilder<IPokemonCardViewModel>, NumberFilterPackageBuilder>()
    .AddTransient<IFilterPackageBuilder<IPokemonCardViewModel>, YearFilterPackageBuilder>()
    .AddTransient<IFilterPackageBuilder<IPokemonCardViewModel>, SeriesFilterPackageBuilder>()
    .AddTransient<IFilterPackageBuilder<IPokemonCardViewModel>, GraderNameFilterPackageBuilder>()
    .AddTransient<IFilterPackageBuilder<IPokemonCardViewModel>, TypeFilterPackageBuilder>()
    .AddTransient<IFilterPackageBuilder<IPokemonCardViewModel>, HolographicFilterPackageBuilder>()
    .AddTransient<IFilterPackageBuilder<IPokemonCardViewModel>, RarityFilterPackageBuilder>()
    .AddTransient<INameFilterPackageBuilder, NameFilterPackageBuilder>()
    .AddTransient<INumberFilterPackageBuilder, NumberFilterPackageBuilder>()
    .AddTransient<IYearFilterPackageBuilder, YearFilterPackageBuilder>()
    .AddTransient<ISeriesFilterPackageBuilder, SeriesFilterPackageBuilder>()
    .AddTransient<IGraderNameFilterPackageBuilder, GraderNameFilterPackageBuilder>()
    .AddTransient<ITypeFilterPackageBuilder, TypeFilterPackageBuilder>()
    .AddTransient<IHolographicFilterPackageBuilder, HolographicFilterPackageBuilder>()
    .AddTransient<IRarityFilterPackageBuilder, RarityFilterPackageBuilder>()
    .AddSingleton(typeof(IFilterPackageBuilderRepository<>), typeof(FilterPackageBuilderRepository<>))

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
