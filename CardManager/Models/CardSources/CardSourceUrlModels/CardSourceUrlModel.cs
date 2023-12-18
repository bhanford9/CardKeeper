using CardManager.Models.CardSources.CardSourceUrlModels.UrlGetParameters;

namespace CardManager.Models.CardSources.CardSourceUrlModels;

public interface ICardSourceUrlModel
{
    string Url { get; set; }
    ICollection<IUrlGetParameter> UrlGetParameters { get; set; }
}

public class CardSourceUrlModel : ICardSourceUrlModel
{
    public string Url { get; set; } = string.Empty;

    public ICollection<IUrlGetParameter> UrlGetParameters { get; set; } = new List<IUrlGetParameter>();
}
