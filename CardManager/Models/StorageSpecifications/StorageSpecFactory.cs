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
    IStorageSpecification NewBinderSpec();
    IStorageSpecification NewBoxSpec();
    IStorageSpecification NewStorageSpec(IStorageMedia media, IStorageLocation location);
}

public class StorageSpecFactory(IServiceProvider serviceProvider) : IStorageSpecFactory
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public IBoxLocation NewBoxLocation() => this.GetService<IBoxLocation>();
    public ISleeveLocation NewSleeveLocation() => this.GetService<ISleeveLocation>();
    public NoLocation NewNoLocation() => new();

    public IBinder NewBinder() => this.GetService<IBinder>();
    public IBox NewBox() => this.GetService<IBox>();
    public NoStorageMedia NewNoStorage() => new();

    public IStorageSpecification NewBinderSpec()
        => new StorageSpecification(this.NewBinder(), this.NewSleeveLocation());
    public IStorageSpecification NewBoxSpec()
        => new StorageSpecification(this.NewBox(), this.NewBoxLocation());
    public IStorageSpecification NewStorageSpec(IStorageMedia media, IStorageLocation location)
        => new StorageSpecification(media, location);

    private T GetService<T>() => this.serviceProvider.GetService<T>()!;
}
