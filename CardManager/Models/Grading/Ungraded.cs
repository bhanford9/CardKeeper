using CardManager.SerializationDtos.Grading;

namespace CardManager.Models.Grading;

public interface IUngraded : ICardGrade, ISerializableModel<UngradedDto> { }

public class Ungraded : IUngraded
{
    public static ICardGrade Get => new Ungraded();

    public UngradedDto ToDto() => new();
}
