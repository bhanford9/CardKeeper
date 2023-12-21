namespace WebScraping.ScrapeOutputs.PokemonScrapeOutputs;

public interface IMavinScrapeOutput : IScrapeOutput
{
    double AverageWorth { get; init; }
    IMavinCardOutput MaxCard { get; init; }
    IMavinCardOutput MinCard { get; init; }
    string Name { get; init; }
    string Number { get; init; }
}

public class MavinScrapeOutput : ScrapeOutput, IMavinScrapeOutput
{
    public string Name { get; init; } = string.Empty;

    public string Number { get; init; } = string.Empty;

    public double AverageWorth { get; init; }

    public IMavinCardOutput MinCard { get; init; } = default!;

    public IMavinCardOutput MaxCard { get; init; } = default!;
}

public interface IMavinCardOutput : IScrapeOutput
{
    string Description { get; init; }
    double SoldFor { get; init; }
    string Url { get; }
}

public class MavinCardOutput(string href) : ScrapeOutput, IMavinCardOutput
{
    public string Url { get; } = $"https://mavin.io{href}";

    public string Description { get; init; } = string.Empty;

    public double SoldFor { get; init; }
}
