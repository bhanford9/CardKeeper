using CardManager.SerializationDtos.Grading.PsaGrading;

namespace CardManager.Models.Grading.PsaGrading;

public interface IPsaGrade : ICardGrade, ISerializableModel<PsaGradeDto>
{
    PsaScale Score { get; set; }
}

public class PsaGrade : IPsaGrade
{
    public PsaScale Score { get; set; }

    public PsaGradeDto ToDto() => new() { Score = this.Score };
}
