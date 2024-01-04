namespace CardManager.Models.StorageSpecifications.Location;

public interface ISleeveLocation : IStorageLocation
{
    int Column { get; set; }
    int Page { get; set; }
    int Row { get; set; }
}

public class SleeveLocation : StorageLocation, ISleeveLocation
{
    public override string Name => "Sleeve Location";

    public int Page { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public override StorageLocationType Type => StorageLocationType.Sleeve;

    public override string ToString() =>
        $"{this.Name}{Environment.NewLine}" +
        $"P: {this.Page}, R: {this.Row}, C: {this.Column}";
}
