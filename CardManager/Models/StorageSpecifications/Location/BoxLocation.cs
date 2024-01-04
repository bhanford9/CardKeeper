using CardManager.SerializationDtos.StorageSpecifications.Location;

namespace CardManager.Models.StorageSpecifications.Location;

public interface IBoxLocation : IStorageLocation, ISerializableModel<BoxLocationDto>
{
    string Description { get; set; }
}

public class BoxLocation : StorageLocation<BoxLocationDto>, IBoxLocation
{
    public override string Name { get; } = "Box Location";

    public string Description { get; set; } = string.Empty;

    public override StorageLocationType Type => StorageLocationType.Box;

    public override BoxLocationDto ToDto() => new()
    {
        Name = this.Name,
        Description = this.Description,
        Type = this.Type,
    };

    public override string ToString() => $"{this.Description}";
}
