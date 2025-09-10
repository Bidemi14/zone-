using System.Security.Cryptography;
using System.Text;
using BUrlShortner.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// In-memory dictionary to store mappings: id → originalUrl
var store = new Dictionary<string, string>();

// POST /  -> create short URL
app.MapPost("/", (UrlInput input, HttpRequest req) =>
{
    if (input == null || string.IsNullOrWhiteSpace(input.Url))
        return Results.BadRequest(new { error = "url is required" });

    if (!Uri.IsWellFormedUriString(input.Url, UriKind.Absolute))
        return Results.BadRequest(new { error = "url must be a valid absolute URL" });

    // generate short id
    var id = GenerateId(6);
    store[id] = input.Url;

    var shortUrl = $"{req.Scheme}://{req.Host}/{id}";

    var record = new UrlRecord
    {
        Id = id,
        OriginalUrl = input.Url,
        ShortUrl = shortUrl
    };

    return Results.Created($"/{id}", record);
});

// GET /{id} -> redirect
app.MapGet("/{id}", (string id) =>
{
    if (store.TryGetValue(id, out var longUrl))
        return Results.Redirect(longUrl);

    return Results.NotFound();
});

app.Run();

// --- helper function to generate random IDs ---
static string GenerateId(int length)
{
    const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    var bytes = new byte[length];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(bytes);

    var sb = new StringBuilder(length);
    foreach (var b in bytes)
        sb.Append(alphabet[b % alphabet.Length]);
    return sb.ToString();
}