namespace CardManager.Models.StorageSpecifications.Media;

public interface IBox : IStorageMedia
{
}

public class Box : StorageMedia, IBox
{
    public override string Type { get; } = "Box";
}
