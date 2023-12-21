namespace CardManager.Models.StorageSpecifications.Location;

public interface IStorageLocation
{
}

public class StorageLocation : IStorageLocation
{
    public static StorageLocation Default => new();
}
