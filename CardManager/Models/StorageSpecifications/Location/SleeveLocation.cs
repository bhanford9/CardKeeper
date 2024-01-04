using CardManager.SerializationDtos.StorageSpecifications.Location;

namespace CardManager.Models.StorageSpecifications.Location;

public interface ISleeveLocation : IStorageLocation, ISerializableModel<SleeveLocationDto>
{
    int Column { get; set; }
    int Page { get; set; }
    int Row { get; set; }
}

public class SleeveLocation : StorageLocation<SleeveLocationDto>, ISleeveLocation
{
    public override string Name => "Sleeve Location";

    public int Page { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public override StorageLocationType Type => StorageLocationType.Sleeve;

    public override SleeveLocationDto ToDto() => new()
    {
        Name = this.Name,
        Page = this.Page,
        Row = this.Row,
        Column = this.Column,
        Type = this.Type,
    };

    public override string ToString() =>
        $"{this.Name}{Environment.NewLine}" +
        $"P: {this.Page}, R: {this.Row}, C: {this.Column}";
}
