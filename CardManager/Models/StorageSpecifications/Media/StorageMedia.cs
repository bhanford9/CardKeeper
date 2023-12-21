namespace CardManager.Models.StorageSpecifications.Media;
public interface IStorageMedia
{
    string Name { get; set; }
}

public class StorageMedia : IStorageMedia
{
    public string Name { get; set; } = string.Empty;

    public static StorageMedia Default => new();
}
