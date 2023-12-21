namespace CardManager.Models.Grading;

public interface IUngraded : ICardGrade { }

public class Ungraded : IUngraded
{
    public static ICardGrade Get => new Ungraded();
}
