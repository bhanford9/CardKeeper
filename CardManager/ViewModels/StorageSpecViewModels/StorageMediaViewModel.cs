using CardManager.Models.StorageSpecifications.Media;

namespace CardManager.ViewModels.StorageSpecViewModels;

public interface IStorageMediaViewModel
{
    string Type { get; set; }

    IStorageMedia ToModel();

    string ToString();
}

public class StorageMediaViewModel(IStorageMedia media) : IStorageMediaViewModel
{
    public string Type { get; set; } = media.Type;

    public override string ToString() => media.ToString();

    public IStorageMedia ToModel() => media;
}
