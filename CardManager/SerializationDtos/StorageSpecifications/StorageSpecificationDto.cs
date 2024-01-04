using CardManager.SerializationDtos.StorageSpecifications.Location;
using CardManager.SerializationDtos.StorageSpecifications.Media;

namespace CardManager.SerializationDtos.StorageSpecifications;

public class StorageSpecificationDto : IModelSerialization
{
    public IStorageLocationDto StorageLocation { get; set; } = default!;

    public IStorageMediaDto StorageMedia { get; set; } = default!;
}
