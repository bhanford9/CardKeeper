namespace CardManager.Models.StorageSpecifications.Location;

public interface IStorageLocation
{
    string Name { get; }

    StorageLocationType Type { get; }

    string ToString();
}

public abstract class StorageLocation : IStorageLocation
{
    public static IStorageLocation Default => new NoLocation();

    public abstract string Name { get; }

    public abstract StorageLocationType Type { get; }

    public abstract override string ToString();
}

public class NoLocation : StorageLocation
{
    public override string Name => "No Location";

    public override StorageLocationType Type => StorageLocationType.None;

    public override string ToString() => this.Name;
}
