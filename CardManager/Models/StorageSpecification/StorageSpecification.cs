using CardManager.Models.StorageSpecification.StorageLocation;
using CardManager.Models.StorageSpecification.StorageMedia;

namespace CardManager.Models.StorageSpecification;

public class StorageSpecification(IStorageMedia storageMedia, IStorageLocation storageLocation) : IStorageSpecification
{
    public IStorageMedia StorageMedia { get; set; } = storageMedia;

    public IStorageLocation StorageLocation { get; set; } = storageLocation;
}