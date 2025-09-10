using BUrlShortener.Models;

namespace BUrlShortener.Services;

public class UrlStore
{
    private readonly Dictionary<string, UrlRecord> _urls = new();

    public UrlRecord Add(string originalUrl)
    {
        var id = Guid.NewGuid().ToString("N")[..6]; // short random ID
        var record = new UrlRecord
        {
            Id = id,
            OriginalUrl = originalUrl,
            ShortenedUrl = $"/{id}"
        };

        _urls[id] = record;
        return record;
    }

    public IEnumerable<UrlRecord> GetAll() => _urls.Values;

    public UrlRecord? GetById(string id) =>
        _urls.TryGetValue(id, out var record) ? record : null;
}