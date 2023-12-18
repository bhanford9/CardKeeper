namespace CardManager.Models.Grading;

public class Ungraded : ICardGrade
{
    public static ICardGrade Get => new Ungraded();
}
