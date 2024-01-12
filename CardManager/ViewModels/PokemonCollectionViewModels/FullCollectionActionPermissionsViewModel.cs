namespace CardManager.ViewModels.PokemonCollectionViewModels;

public interface ICollectionActionPermissionsViewModel
{
    bool CanAddNewCard { get; }
    bool CanAddToCollection { get; }
    bool CanDelete { get; }
    bool CanDuplicate { get; }
    bool CanRetrieveAppraisals { get; }
    bool CanSaveCustomCollection { get; }
    bool CanSaveFullCollection { get; }
    bool CanRemoveFromCollection { get; }
}

public interface IFullCollectionActionPermissionsViewModel : ICollectionActionPermissionsViewModel { }
public class FullCollectionActionPermissionsViewModel : IFullCollectionActionPermissionsViewModel
{
    public bool CanDuplicate { get; } = true;

    public bool CanDelete { get; } = true;

    public bool CanRemoveFromCollection { get; } = false;

    public bool CanRetrieveAppraisals { get; } = true;

    public bool CanAddToCollection { get; } = true;

    public bool CanSaveFullCollection { get; } = true;

    public bool CanSaveCustomCollection { get; } = false;

    public bool CanAddNewCard { get; } = true;
}

public interface ICustomCollectionActionPermissionsViewModel : ICollectionActionPermissionsViewModel { }
public class CustomCollectionActionPermissionsViewModel : ICustomCollectionActionPermissionsViewModel
{
    public bool CanDuplicate { get; } = false;

    public bool CanDelete { get; } = false;

    public bool CanRemoveFromCollection { get; } = true;

    public bool CanRetrieveAppraisals { get; } = false;

    public bool CanAddToCollection { get; } = true;

    public bool CanSaveFullCollection { get; } = false;

    public bool CanSaveCustomCollection { get; } = true;

    public bool CanAddNewCard { get; } = false;
}
