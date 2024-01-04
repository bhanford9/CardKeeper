using CardManager.Models.Grading.BeckettGrading;

namespace CardManager.SerializationDtos.Grading.BeckettGrading;

public class BeckettGradeDto : ICardGradeDto
{
    public BeckettScale Centering { get; set; }
    public BeckettScale Corners { get; set; }
    public BeckettScale Edges { get; set; }
    public BeckettScale Overall { get; set; }
    public BeckettScale Surface { get; set; }
}
