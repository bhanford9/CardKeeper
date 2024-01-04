using CardManager.SerializationDtos.StorageSpecifications.Media;

namespace CardManager.Models.StorageSpecifications.Media;

public interface IBinder : IStorageMedia, ISerializableModel<BinderDto> { }

public class Binder : StorageMedia<BinderDto>, IBinder
{
    public override string Type { get; } = "Binder";

    public override BinderDto ToDto() => new() { Type = this.Type, };
}
