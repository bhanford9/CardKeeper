using CardManager.Models.StorageSpecifications.Media;

namespace CardManager.SerializationDtos.StorageSpecifications.Media;

public interface IStorageMediaDto : IModelSerialization
{
    StorageMediaType Type { get; set; }
    string Description { get; set; }
}

public class StorageMediaDto : IStorageMediaDto
{
    public StorageMediaType Type { get; set; }

    public string Description { get; set; } = string.Empty;
}

public class NoStorageMediaDto : StorageMediaDto { }


public static class StorageLocationDtoUtils
{
    public static NoStorageMedia ToModel(this NoStorageMediaDto _) => new();

    public static IBinder ToModel(this BinderDto dto) => new Binder()
    {
        Description = dto.Description,
    };

    public static IBox ToModel(this BoxDto dto) => new Box()
    {
        Description = dto.Description,
    };

    public static IStorageMedia ToModel(this IStorageMediaDto dto) => dto switch
    {
        NoStorageMediaDto none => none.ToModel(),
        BinderDto binder => binder.ToModel(),
        BoxDto box => box.ToModel(),
        _ => throw new ArgumentOutOfRangeException(),
    };
}
