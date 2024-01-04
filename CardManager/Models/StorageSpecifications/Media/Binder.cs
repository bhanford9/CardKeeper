using CardManager.SerializationDtos.StorageSpecifications.Media;

namespace CardManager.Models.StorageSpecifications.Media;

public interface IBinder : IStorageMedia, ISerializableModel<BinderDto> { }

public class Binder : StorageMedia<BinderDto>, IBinder
{
    public override StorageMediaType Type { get; } = StorageMediaType.Binder;

    public override BinderDto ToDto() => new() { Type = this.Type, };
}
