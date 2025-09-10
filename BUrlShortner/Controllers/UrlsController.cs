using Microsoft.AspNetCore.Mvc;
using BUrlShortener.Models;
using BUrlShortener.Services;

namespace BUrlShortener.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlsController : ControllerBase
{
    private readonly UrlStore _store;
    public UrlsController(UrlStore store) => _store = store;

    [HttpPost]
    public ActionResult<UrlRecord> Add([FromBody] UrlInput input)
    {
        if (input == null || string.IsNullOrWhiteSpace(input.Url))
            return BadRequest("url is required");

        var record = _store.Add(input.Url);
        return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
    }

    [HttpGet]
    public ActionResult<IEnumerable<UrlRecord>> GetAll() => Ok(_store.GetAll());

    [HttpGet("{id}")]
    public ActionResult<UrlRecord> GetById(string id)
    {
        var r = _store.GetById(id);
        return r is not null ? Ok(r) : NotFound();
    }
}