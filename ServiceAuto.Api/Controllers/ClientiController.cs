using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceAuto.Api.Data;
using ServiceAuto.Api.Entities;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Clienti)]
public class ClientiController : ControllerBase
{
    private readonly AppDbContext _db;

    public ClientiController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        var data = await _db.Clienti
            .AsNoTracking()
            .OrderBy(c => c.Nume)
            .Select(c => new ClientDto
            {
                Id = c.Id,
                Nume = c.Nume,
                Telefon = c.Telefon,
                Email = c.Email
            })
            .ToListAsync();

        return data;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var c = await _db.Clienti.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (c == null) return NotFound();

        return new ClientDto
        {
            Id = c.Id,
            Nume = c.Nume,
            Telefon = c.Telefon,
            Email = c.Email
        };
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientDto dto)
    {
        var entity = new Client
        {
            Nume = dto.Nume,
            Telefon = dto.Telefon,
            Email = dto.Email
        };

        _db.Clienti.Add(entity);
        await _db.SaveChangesAsync();

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClientDto dto)
    {
        var entity = await _db.Clienti.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        entity.Nume = dto.Nume;
        entity.Telefon = dto.Telefon;
        entity.Email = dto.Email;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Clienti.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        _db.Clienti.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
