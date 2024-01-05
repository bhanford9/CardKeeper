using System.Diagnostics.CodeAnalysis;
using CardManager.Models.CardCollections;
using Microsoft.AspNetCore.Components;

namespace CardManager.ViewModels.PokemonCollectionViewModels;

public interface IPokemonCollectionsManagerViewModel : IViewModel
{
    Dictionary<string, IPokemonCollectionViewModel> CustomCollections { get; set; }
    IPokemonCollectionViewModel FullCollection { get; set; }
}

public class PokemonCollectionsManagerViewModel(IPokemonCollectionViewModel fullCollection)
    : BaseViewModel, IPokemonCollectionsManagerViewModel
{
    public IPokemonCollectionViewModel FullCollection { get; set; } = fullCollection;

    public Dictionary<string, IPokemonCollectionViewModel> CustomCollections { get; set; } = new();
}
