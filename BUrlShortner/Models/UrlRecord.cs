namespace BUrlShortner.Models;

public class UrlRecord
{
    public string Id { get; set; } = string.Empty;       // short code
    public string OriginalUrl { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
}