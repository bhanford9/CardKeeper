using CardManager.Models.StorageSpecifications.Location;
using CardManager.Models.StorageSpecifications.Media;
using CardManager.SerializationDtos.StorageSpecifications;
using CardManager.SerializationDtos.StorageSpecifications.Location;
using CardManager.SerializationDtos.StorageSpecifications.Media;

namespace CardManager.Models.StorageSpecifications;

public interface IStorageSpecification : ISerializableModel<StorageSpecificationDto>
{
    IStorageLocation Location { get; set; }
    IStorageMedia Media { get; set; }
}

public class StorageSpecification(IStorageMedia storageMedia, IStorageLocation storageLocation) : IStorageSpecification
{
    public IStorageMedia Media { get; set; } = storageMedia;

    public IStorageLocation Location { get; set; } = storageLocation;

    public static StorageSpecification Default => new(StorageMedia<NoStorageMediaDto>.Default, StorageLocation<NoLocationDto>.Default);

    public override string ToString() =>
        $"{this.Media.ToString()}{Environment.NewLine}{this.Location.ToString()}";

    public StorageSpecificationDto ToDto() => new StorageSpecificationDto()
    {
        StorageMedia = this.Media switch
        {
            IBox box => box.ToDto(),
            IBinder binder => binder.ToDto(),
            NoStorageMedia none => none.ToDto(),
            _ => throw new ArgumentOutOfRangeException()
        },
        StorageLocation = this.Location switch
        {
            ISleeveLocation sleeve => sleeve.ToDto(),
            IBoxLocation box => box.ToDto(),
            NoLocation none => none.ToDto(),
            _ => throw new ArgumentOutOfRangeException()
        },
    };
}