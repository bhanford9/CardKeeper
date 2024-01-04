namespace CardManager.SerializationDtos.StorageSpecifications.Media;

public interface IStorageMediaDto : IModelSerialization
{
    string Type { get; set; }
}

public class StorageMediaDto : IStorageMediaDto
{
    public string Type { get; set; } = string.Empty;
}

public class NoStorageMediaDto : StorageMediaDto { }
