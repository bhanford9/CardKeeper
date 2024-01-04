using CardManager.SerializationDtos.StorageSpecifications.Location;

namespace CardManager.Models.StorageSpecifications.Location;

public interface IStorageLocation
{
    string Name { get; }

    StorageLocationType Type { get; }

    string ToString();
}

public abstract class StorageLocation<TDto> : IStorageLocation
    where TDto : IStorageLocationDto
{
    public static IStorageLocation Default => new NoLocation();

    public abstract string Name { get; }

    public abstract StorageLocationType Type { get; }

    public abstract TDto ToDto();

    public abstract override string ToString();
}

public class NoLocation : StorageLocation<NoLocationDto>
{
    public override string Name => "No Location";

    public override StorageLocationType Type => StorageLocationType.None;

    public override NoLocationDto ToDto() => new();

    public override string ToString() => this.Name;
}
