using CardManager.Models.Grading.PsaGrading;

namespace CardManager.SerializationDtos.Grading.PsaGrading;

public class PsaGradeDto : ICardGradeDto
{
    public PsaScale Score { get; set; }
}
