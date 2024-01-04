namespace CardManager.Models.StorageSpecifications.Media;

public interface IBinder : IStorageMedia { }

public class Binder : StorageMedia, IBinder
{
    public override string Type { get; } = "Binder";
}
