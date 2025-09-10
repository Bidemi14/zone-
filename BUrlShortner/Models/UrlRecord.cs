namespace BUrlShortener.Models;

public class UrlRecord
{
    public string Id{get;set;}= string.Empty;
    public string OriginalUrl{get;set;}= string.Empty;
    public string ShortenedUrl{get;set;}= string.Empty;
}