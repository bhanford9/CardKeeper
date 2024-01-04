using CardManager.Models.StorageSpecifications.Media;

namespace CardManager.ViewModels.StorageSpecViewModels;

public interface IStorageMediaViewModel
{
    StorageMediaType Type { get; set; }

    IStorageMedia ToModel();

    string ToString();
}

public class StorageMediaViewModel(IStorageMedia media) : IStorageMediaViewModel
{
    public StorageMediaType Type { get; set; } = media.Type;

    public override string ToString() => media.ToString();

    public IStorageMedia ToModel() => media;
}
