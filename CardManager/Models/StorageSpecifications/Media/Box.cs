using CardManager.SerializationDtos.StorageSpecifications.Media;

namespace CardManager.Models.StorageSpecifications.Media;

public interface IBox : IStorageMedia, ISerializableModel<BoxDto> { }

public class Box : StorageMedia<BoxDto>, IBox
{
    public override StorageMediaType Type { get; } = StorageMediaType.Box;

    public override BoxDto ToDto() => new() { Type = this.Type, };
}
