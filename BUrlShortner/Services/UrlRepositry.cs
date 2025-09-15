using BUrlShortner.Models;

namespace BUrlShortner.Services;

public interface IUrlRepository
{
    IEnumerable<UrlRecord> GetAll();   // returns all shortened urls
    UrlRecord? GetById(int id);        // find a specific url
    UrlRecord Add(string originalUrl, string shortUrl); // create a new url record
    UrlRecord? CheckUrl(string url);   // check if url already exists
}

public class UrlRepository : IUrlRepository
{
    private static readonly Dictionary<int, UrlRecord> _urls = new(); // in-memory dictionary
    private static int _nextId = 1;

    public IEnumerable<UrlRecord> GetAll()
    {
        return _urls.Values;
    }

    public UrlRecord? GetById(int id)
    {
        return _urls.GetValueOrDefault(id);
    }

    public UrlRecord? CheckUrl(string originalUrl)
    {
        foreach (var record in _urls.Values)
        {
            if (string.Equals(record.OriginalUrl, originalUrl, StringComparison.OrdinalIgnoreCase))
            {
                return record; // match found
            }
        }
        return null; // no match
    }

    public UrlRecord Add(string originalUrl, string shortUrl)
    {
        var record = new UrlRecord
        {
            Id = _nextId++,
            OriginalUrl = originalUrl,
            ShortUrl = shortUrl
        };

        _urls[record.Id] = record;
        return record;
    }

    
}

public class SqlServerRepository : IUrlRepository
{
    public IEnumerable<UrlRecord> GetAll()
    {
        throw new NotImplementedException();
    }

    public UrlRecord? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public UrlRecord Add(string originalUrl, string shortUrl)
    {
        throw new NotImplementedException();
    }

    public UrlRecord? CheckUrl(string url)
    {
        throw new NotImplementedException();
    }
}