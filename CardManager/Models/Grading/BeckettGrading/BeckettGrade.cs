namespace CardManager.Models.Grading.BeckettGrading;

public class BeckettGrade : ICardGrade
{
    public BeckettScale Centering { get; set; }

    public BeckettScale Corners { get; set; }

    public BeckettScale Edges { get; set; }

    public BeckettScale Surface { get; set; }

    public BeckettScale Overall { get; set; }
}
