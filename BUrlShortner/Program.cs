using BUrlShortner.Models;
using BUrlShortner.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IUrlRepository, UrlRepository>();
var app = builder.Build();

//POST 
app.MapPost("api/shorten", (UrlInput input, HttpRequest req) =>
{
    //this is used to validate the input 
    var urlRepository = app.Services.GetRequiredService<IUrlRepository>();
    
    if (input == null || string.IsNullOrWhiteSpace(input.Url))
        return Results.BadRequest(new { error = "url is required" });

    if (!Uri.IsWellFormedUriString(input.Url, UriKind.Absolute))
        return Results.BadRequest(new { error = "url must be a valid absolute URL" });

    var existing = urlRepository.CheckUrl(input.Url);
    if (existing != null)
    {
        return Results.Ok(existing); // don't create a duplicate
    }


    var dummyShortUrl = $"{req.Scheme}://{req.Host}"; 
    var record = urlRepository.Add(input.Url, dummyShortUrl);

   
    record.ShortUrl = $"{req.Scheme}://{req.Host}/{record.Id}"; // 5^36

    return Results.Created($"/{record.Id}", record);
});

//GET redirects to the original link  
app.MapGet("/{id:int}", (int id) =>
{
    var urlRepository = app.Services.GetRequiredService<IUrlRepository>();
    
    var record = urlRepository.GetById(id);
    if (record is null)
        return Results.NotFound("Url not found");

    return Results.Redirect(record.OriginalUrl);
});

//GET lists all the urls 
app.MapGet("api/urls", () =>
{
    var urlRepository = app.Services.GetRequiredService<IUrlRepository>();
    return urlRepository.GetAll();
});

app.Run();