using CardManager.Models.StorageSpecifications;
using CardManager.Models.StorageSpecifications.Location;

namespace CardManager.ViewModels.StorageSpecViewModels;

public interface IStorageSpecViewModel
{
    IStorageLocationViewModel StorageLocation { get; }
    IStorageMediaViewModel StorageMedia { get; }

    IStorageSpecification ToModel();
    string ToString();
}

// maybe there shouldn't be a storage spec VM and the card VM should just hold the location and media VMs
public class StorageSpecViewModel : IStorageSpecViewModel
{
    private readonly IStorageSpecification storageSpec;

    public StorageSpecViewModel(IStorageSpecification storageSpec)
    {
        this.storageSpec = storageSpec;

        this.StorageLocation = storageSpec.Location.Type switch
        {
            StorageLocationType.Box => new BoxLocationViewModel((IBoxLocation)storageSpec.Location),
            StorageLocationType.Sleeve => new SleeveLocationViewModel((ISleeveLocation)storageSpec.Location),
            _ => new NoLocationViewModel((NoLocation)storageSpec.Location),
        };

        this.StorageMedia = new StorageMediaViewModel(storageSpec.Media);
    }

    public IStorageLocationViewModel StorageLocation { get; }

    public IStorageMediaViewModel StorageMedia { get; } 

    public override string ToString() =>
        $"{this.StorageMedia.ToString()}{Environment.NewLine}{this.StorageLocation.ToString()}";

    public IStorageSpecification ToModel() => this.storageSpec;
}
