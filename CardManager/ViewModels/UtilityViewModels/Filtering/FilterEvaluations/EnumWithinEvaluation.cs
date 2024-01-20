
namespace CardManager.ViewModels.UtilityViewModels.Filtering.FilterEvaluations;

public interface IEnumWithinEvaluation : IWithinEvaluation { }
public class EnumWithinEvaluation<T> : WithinEvaluation<T>, IEnumWithinEvaluation
    where T : Enum
{
    public EnumWithinEvaluation()
    {
        this.Options = Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(o => new CheckBoxViewModel<T>(o, false) as ICheckBoxViewModel)
            .ToList();
    }
}
