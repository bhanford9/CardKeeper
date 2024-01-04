using CardManager.Models.StorageSpecifications.Location;

namespace CardManager.SerializationDtos.StorageSpecifications.Location;

public interface IStorageLocationDto : IModelSerialization
{
    string Name { get; set; }
    StorageLocationType Type { get; set; }
}

public class StorageLocationDto : IStorageLocationDto
{
    public string Name { get; set; } = string.Empty;

    public StorageLocationType Type { get; set; }
}

public class NoLocationDto : StorageLocationDto { }
