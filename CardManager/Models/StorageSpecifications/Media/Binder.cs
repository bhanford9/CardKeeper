namespace CardManager.Models.StorageSpecifications.Media;

public interface IBinder : IStorageMedia
{
    int Column { get; set; }
    int Page { get; set; }
    int Row { get; set; }
}

public class Binder : StorageMedia, IBinder
{
    public int Page { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }
}
