namespace CardManager.SerializationDtos.StorageSpecifications.Location;

public class SleeveLocationDto : StorageLocationDto
{
    public int Column { get; set; }
    public int Page { get; set; }
    public int Row { get; set; }
}
