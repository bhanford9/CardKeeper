using CardManager.ViewModels.GradingViewModels;
using CardManager.ViewModels.MonetaryViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilterCriteria;
using CardManager.ViewModels.PokemonCollectionViewModels.Filtering.FilteringPackageBuilders;
using CardManager.ViewModels.StorageSpecViewModels;
using CardManager.ViewModels.UtilityViewModels;
using CardManager.ViewModels.UtilityViewModels.Filtering.FilterPackageBuilding;
using CardManager.ViewModels.UtilityViewModels.Filtering;

namespace CardManager.ViewModels;

public static class ViewModelsContainer
{
    public static IServiceCollection RegisterViewModels(this IServiceCollection builder) =>
        builder
            .AddSingleton<IViewModelsFactory, ViewModelsFactory>()
            .AddGradingData()
            .AddMonetaryData()
            .AddPokemonCollectionData()
            .AddStorageData()
            .AddUtilityData()
            .AddFilteringData();

    private static IServiceCollection AddGradingData(this IServiceCollection builder) =>
        builder
            .AddTransient<IBeckettGradingViewModel, BeckettGradingViewModel>()
            .AddTransient<ICgcGradingViewModel, CgcGradingViewModel>()
            .AddTransient<IPsaGradingViewModel, PsaGradingViewModel>()
            .AddTransient<IGradingAggregateViewModel, GradingAggregateViewModel>()
            .AddTransient(typeof(IGradingInputRowViewModel<>), typeof(GradingInputRowViewModel<>));

    private static IServiceCollection AddMonetaryData(this IServiceCollection builder) =>
        builder
            .AddTransient<IMavinMonetaryViewModel, MavinMonetaryViewModel>()
            .AddTransient<IMonetaryAggregateViewModel, MonetaryAggregateViewModel>();

    private static IServiceCollection AddPokemonCollectionData(this IServiceCollection builder) =>
        builder
            .AddTransient<IPokemonCardViewModel, PokemonCardViewModel>()
            .AddTransient<IPokemonCollectionViewModel, PokemonCollectionViewModel>()
            .AddTransient<IPokemonCustomCollectionViewModel, PokemonCustomCollectionViewModel>()
            .AddTransient<IPokemonCollectionsManagerViewModel, PokemonCollectionsManagerViewModel>()
            .AddTransient<IFullCollectionActionPermissionsViewModel, FullCollectionActionPermissionsViewModel>()
            .AddTransient<ICustomCollectionActionPermissionsViewModel, CustomCollectionActionPermissionsViewModel>();

    private static IServiceCollection AddStorageData(this IServiceCollection builder) =>
        builder
            .AddTransient<ISleeveLocationViewModel, SleeveLocationViewModel>()
            .AddTransient<IBoxLocationViewModel, BoxLocationViewModel>();

    private static IServiceCollection AddUtilityData(this IServiceCollection builder) =>
        builder.AddTransient(typeof(IEnumSelectorViewModel<>), typeof(EnumSelectorViewModel<>));

    private static IServiceCollection AddFilteringData(this IServiceCollection builder) =>
        builder
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
            .AddSingleton(typeof(IFilterPackageBuilderRepository<>), typeof(FilterPackageBuilderRepository<>));
}
