namespace CardManager.Models.StorageSpecifications.Location;

public interface IBoxLocation : IStorageLocation
{
    string Description { get; set; }
}

public class BoxLocation : StorageLocation, IBoxLocation
{
    public override string Name { get; } = "Box Location";

    public string Description { get; set; } = string.Empty;

    public override StorageLocationType Type => StorageLocationType.Box;

    public override string ToString() => $"{this.Description}";
}
