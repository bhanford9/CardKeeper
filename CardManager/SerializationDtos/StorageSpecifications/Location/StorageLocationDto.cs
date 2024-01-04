using CardManager.Models.StorageSpecifications.Location;

namespace CardManager.SerializationDtos.StorageSpecifications.Location;

public interface IStorageLocationDto : IModelSerialization
{
    string Name { get; set; }
    StorageLocationType Type { get; set; }
}

public class StorageLocationDto : IStorageLocationDto
{
    public string Name { get; set; } = string.Empty;

    public StorageLocationType Type { get; set; }
}

public class NoLocationDto : StorageLocationDto { }

public static class StorageLocationDtoUtils
{
    public static NoLocation ToModel(this NoLocationDto _) => new();

    public static ISleeveLocation ToModel(this SleeveLocationDto dto) => new SleeveLocation()
    {
        Column = dto.Column,
        Page = dto.Page,
        Row = dto.Row,
    };

    public static IBoxLocation ToModel(this BoxLocationDto dto) => new BoxLocation()
    {
        Description = dto.Description,
    };

    public static IStorageLocation ToModel(this IStorageLocationDto dto) => dto switch
    {
        NoLocationDto none => none.ToModel(),
        SleeveLocationDto sleeve => sleeve.ToModel(),
        BoxLocationDto box => box.ToModel(),
        _ => throw new ArgumentOutOfRangeException(),
    };
}
