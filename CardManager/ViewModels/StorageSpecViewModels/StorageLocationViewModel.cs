using CardManager.Models.StorageSpecifications.Location;

namespace CardManager.ViewModels.StorageSpecViewModels;

public interface IStorageLocationViewModel : IViewModel
{
    void UpdateModel(IStorageLocation location);
    IStorageLocation ToModel();
    string ToString();
}

public abstract class StorageLocationViewModel(IStorageLocation storageLocation)
    : BaseViewModel, IStorageLocationViewModel
{
    protected IStorageLocation storageLocation = storageLocation;

    public void UpdateModel(IStorageLocation location)
    {
        this.storageLocation = location;
    }

    public IStorageLocation ToModel()
    {
        this.UpdateModel();
        return this.storageLocation;
    }

    public override abstract string ToString();

    protected abstract void UpdateModel();
}

public interface ISleeveLocationViewModel : IStorageLocationViewModel
{
    int Column { get; set; }
    int Page { get; set; }
    int Row { get; set; }
}

public class SleeveLocationViewModel(ISleeveLocation storageLocation)
    : StorageLocationViewModel(storageLocation), ISleeveLocationViewModel
{
    private ISleeveLocation sleeveLocation => (ISleeveLocation)this.storageLocation;

    public int Page { get; set; } = storageLocation.Page;

    public int Row { get; set; } = storageLocation.Row;

    public int Column { get; set; } = storageLocation.Column;

    protected override void UpdateModel()
    {
        this.sleeveLocation.Page = this.Page;
        this.sleeveLocation.Row = this.Row;
        this.sleeveLocation.Column = this.Column;
    }

    public override string ToString() => $"P: {this.Page}, R: {this.Row}, C: {this.Column}";
}

public interface IBoxLocationViewModel : IStorageLocationViewModel
{
    string Description { get; set; }
}

public class BoxLocationViewModel(IBoxLocation storageLocation)
    : StorageLocationViewModel(storageLocation), IBoxLocationViewModel
{
    private IBoxLocation boxLocation => (IBoxLocation)this.storageLocation;

    public string Description { get; set; } = storageLocation.Description;

    protected override void UpdateModel()
    {
        this.boxLocation.Description = this.Description;
    }
    public override string ToString() => $"{this.Description}";
}

public class NoLocationViewModel(NoLocation storageLocation) : StorageLocationViewModel(storageLocation)
{
    public override string ToString() => this.storageLocation.ToString();

    protected override void UpdateModel() { }
}
