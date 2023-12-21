using CardManager.Models.StorageSpecifications.Location;
using CardManager.Models.StorageSpecifications.Media;

namespace CardManager.Models.StorageSpecifications;
public interface IStorageSpecification
{
    IStorageLocation Location { get; set; }
    IStorageMedia Media { get; set; }
}

public class StorageSpecification(IStorageMedia storageMedia, IStorageLocation storageLocation) : IStorageSpecification
{
    public IStorageMedia Media { get; set; } = storageMedia;

    public IStorageLocation Location { get; set; } = storageLocation;

    public static StorageSpecification Default => new(StorageMedia.Default, StorageLocation.Default);
}