namespace CardManager.Models.Grading.CgcGrading;

public class CgcGrade : ICardGrade
{
    public CgcScale Centering { get; set; }

    public CgcScale Corners { get; set; }

    public CgcScale Edges { get; set; }

    public CgcScale Surface { get; set; }

    public CgcScale Overall { get; set; }
}
