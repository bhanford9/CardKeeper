namespace CardManager.Models.StorageSpecifications.Media;

public interface IStorageMedia
{
    string Type { get; }

    string ToString();
}

public abstract class StorageMedia() : IStorageMedia
{
    public static IStorageMedia Default => new NoStorageMedia();
    
    public abstract string Type { get; }

    public string Description { get; set; } = string.Empty;

    public override string ToString() => $"{this.Type}{Environment.NewLine}{this.Description}";
}

public class NoStorageMedia() : StorageMedia()
{
    public override string Type { get; } = "No Storage";
}
