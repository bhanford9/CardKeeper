using CardManager.Models.CardCollections;
using CardManager.Models.Cards;
using CardManager.Models.Cards.PokemonCards;
using CardManager.Models.CardSources;
using CardManager.Models.CardSources.CardSourceScrapingModels;
using CardManager.Models.CardSources.CardSourceUrlModels;
using CardManager.Models.Grading;
using CardManager.Models.Grading.BeckettGrading;
using CardManager.Models.Grading.CgcGrading;
using CardManager.Models.Grading.PsaGrading;
using CardManager.Models.MonetaryData;
using CardManager.Models.StorageSpecifications;
using CardManager.Models.StorageSpecifications.Location;
using CardManager.Models.StorageSpecifications.Media;
using CardManager.SerializationDtos.Cards.PokemonCards;

namespace CardManager.Models;

public static class ModelsContainer
{
    public static IServiceCollection RegisterModels(this IServiceCollection builder)
        => builder
        .AddCardsData()
        .AddCardSourcesData()
        .AddGradingData()
        .AddMonetaryData()
        .AddStorageSpecifications();

    private static IServiceCollection AddCardsData(this IServiceCollection builder)
        => builder
        .AddTransient<ICard, PokemonCard>()
        .AddTransient<IPokemonCard, PokemonCard>()
        .AddTransient<ICardCollection<IPokemonCard, PokemonCardDto>, PokemonCardCollection>()
        .AddSingleton<IPokemonCardCollection, PokemonCardCollection>();

    private static IServiceCollection AddCardSourcesData(this IServiceCollection builder)
        => builder
        .AddTransient<ICardSourceScrapingModel, CardSourceScrapingModel>()
        .AddTransient<ICardSourceUrlModel, CardSourceUrlModel>()
        .AddTransient<ICardSourceModel, EmptyCardSourceModel>()
        .AddTransient<IEmptyCardSourceModel, EmptyCardSourceModel>();

    private static IServiceCollection AddGradingData(this IServiceCollection builder)
        => builder
        .AddTransient<ICardGrade, Ungraded>()
        .AddTransient<ICardGrade, PsaGrade>()
        .AddTransient<ICardGrade, CgcGrade>()
        .AddTransient<ICardGrade, BeckettGrade>()
        .AddTransient<IPsaGrade, PsaGrade>()
        .AddTransient<ICgcGrade, CgcGrade>()
        .AddTransient<IBeckettGrade, BeckettGrade>();

    private static IServiceCollection AddMonetaryData(this IServiceCollection builder)
        => builder
        .AddTransient<IMonetaryData, MonetaryData.MonetaryData>()
        .AddTransient<IMavinMonetaryData, MavinMonetaryData>();

    private static IServiceCollection AddStorageSpecifications(this IServiceCollection builder)
        => builder
        .AddTransient<IStorageSpecFactory, StorageSpecFactory>()
        .AddTransient<IStorageLocation, SleeveLocation>()
        .AddTransient<IStorageLocation, BoxLocation>()
        .AddTransient<ISleeveLocation, SleeveLocation>()
        .AddTransient<IBoxLocation, BoxLocation>()
        .AddTransient<IStorageMedia, Box>()
        .AddTransient<IStorageMedia, Binder>()
        .AddTransient<IBox, Box>()
        .AddTransient<IBinder, Binder>();
}