using CardManager.Models.StorageSpecification.StorageLocation;
using CardManager.Models.StorageSpecification.StorageMedia;

namespace CardManager.Models.StorageSpecification;
public interface IStorageSpecification
{
    IStorageLocation StorageLocation { get; set; }
    IStorageMedia StorageMedia { get; set; }
}