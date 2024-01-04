using CardManager.Models.StorageSpecifications.Location;
using CardManager.Models.StorageSpecifications.Media;

namespace CardManager.Models.StorageSpecifications;

public interface IStorageSpecFactory
{
    IBinder NewBinder();
    IBox NewBox();
    NoStorageMedia NewNoStorage();
    IBoxLocation NewBoxLocation();
    NoLocation NewNoLocation();
    ISleeveLocation NewSleeveLocation();
}

public class StorageSpecFactory : IStorageSpecFactory
{
    public IBoxLocation NewBoxLocation() => new BoxLocation();

    public ISleeveLocation NewSleeveLocation() => new SleeveLocation();

    public NoLocation NewNoLocation() => new NoLocation();

    public IBinder NewBinder() => new Binder();

    public IBox NewBox() => new Box();

    public NoStorageMedia NewNoStorage() => new NoStorageMedia();
}
