using CardManager.Models.Grading;
using CardManager.Models.Grading.BeckettGrading;
using CardManager.Models.Grading.CgcGrading;
using CardManager.Models.Grading.PsaGrading;
using CardManager.SerializationDtos.Grading.BeckettGrading;
using CardManager.SerializationDtos.Grading.CgcGrading;
using CardManager.SerializationDtos.Grading.PsaGrading;

namespace CardManager.SerializationDtos.Grading;

public interface ICardGradeDto : IModelSerialization { }

public static class CardGradeDtoUtils
{
    public static BeckettGrade ToModel(this BeckettGradeDto dto) => new()
    {
        Centering = dto.Centering,
        Corners = dto.Corners,
        Edges = dto.Edges,
        Overall = dto.Overall,
        Surface = dto.Surface,
    };

    public static CgcGrade ToModel(this CgcGradeDto dto) => new()
    {
        Centering = dto.Centering,
        Corners = dto.Corners,
        Edges = dto.Edges,
        Overall = dto.Overall,
        Surface = dto.Surface,
    };

    public static PsaGrade ToModel(this PsaGradeDto dto) => new() { Score = dto.Score };

    public static ICardGrade ToModel(this ICardGradeDto dto) => dto switch
    {
        BeckettGradeDto b => b.ToModel(),
        CgcGradeDto cgc => cgc.ToModel(),
        PsaGradeDto psa => psa.ToModel(),
        _ => throw new ArgumentOutOfRangeException(),
    };
}