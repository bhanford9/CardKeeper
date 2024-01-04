using CardManager.SerializationDtos.Grading.BeckettGrading;

namespace CardManager.Models.Grading.BeckettGrading;

public interface IBeckettGrade : ICardGrade, ISerializableModel<BeckettGradeDto>
{
    BeckettScale Centering { get; set; }
    BeckettScale Corners { get; set; }
    BeckettScale Edges { get; set; }
    BeckettScale Overall { get; set; }
    BeckettScale Surface { get; set; }
}

public class BeckettGrade : IBeckettGrade
{
    public BeckettScale Centering { get; set; }

    public BeckettScale Corners { get; set; }

    public BeckettScale Edges { get; set; }

    public BeckettScale Surface { get; set; }

    public BeckettScale Overall { get; set; }

    public BeckettGradeDto ToDto() => new()
    {
        Centering = this.Centering,
        Corners = this.Corners,
        Edges = this.Edges,
        Overall = this.Overall,
        Surface = this.Surface,
    };
}
