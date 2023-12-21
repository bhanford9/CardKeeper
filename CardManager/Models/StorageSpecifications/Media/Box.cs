namespace CardManager.Models.StorageSpecifications.Media;

public interface IBox : IStorageMedia
{
    string LocationDescription { get; set; }
}

public class Box : StorageMedia, IBox
{
    public string LocationDescription { get; set; } = string.Empty;
}
