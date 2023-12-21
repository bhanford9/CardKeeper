namespace WebScraping.ScrapeOutputs;

public static class ScrapeOutputUtilities
{
    public static T As<T>(this IScrapeOutput output) where T : IScrapeOutput => (T)(object)output;
}

public interface IScrapeOutput { }

public class ScrapeOutput : IScrapeOutput 
{
}
