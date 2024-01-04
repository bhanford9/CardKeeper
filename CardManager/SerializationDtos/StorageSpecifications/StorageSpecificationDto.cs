using CardManager.Models.StorageSpecifications;
using CardManager.SerializationDtos.StorageSpecifications.Location;
using CardManager.SerializationDtos.StorageSpecifications.Media;

namespace CardManager.SerializationDtos.StorageSpecifications;

public class StorageSpecificationDto : IModelSerialization
{
    public IStorageLocationDto StorageLocation { get; set; } = default!;

    public IStorageMediaDto StorageMedia { get; set; } = default!;
}

public static class StorageSpecDtoUtils
{
    public static StorageSpecification ToModel(this StorageSpecificationDto dto) =>
        new(dto.StorageMedia.ToModel(), dto.StorageLocation.ToModel());
}