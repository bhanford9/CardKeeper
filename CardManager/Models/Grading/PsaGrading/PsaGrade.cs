namespace CardManager.Models.Grading.PsaGrading;

public interface IPsaGrade : ICardGrade
{
    PsaScale Score { get; set; }
}

public class PsaGrade : IPsaGrade
{
    public PsaScale Score { get; set; }
}
