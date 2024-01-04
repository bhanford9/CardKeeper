using CardManager.Models.Grading.CgcGrading;

namespace CardManager.SerializationDtos.Grading.CgcGrading;

public class CgcGradeDto : ICardGradeDto
{
    public CgcScale Centering { get; set; }
    public CgcScale Corners { get; set; }
    public CgcScale Edges { get; set; }
    public CgcScale Surface { get; set; }
    public CgcScale Overall { get; set; }
}
