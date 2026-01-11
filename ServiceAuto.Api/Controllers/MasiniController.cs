using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceAuto.Api.Data;
using ServiceAuto.Api.Entities;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Masini)]
public class MasiniController : ControllerBase
{
    private readonly AppDbContext _db;

    public MasiniController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<MasinaDto>>> GetAll()
    {
        var data = await _db.Masini
            .AsNoTracking()
            .OrderBy(m => m.Marca)
            .ThenBy(m => m.Model)
            .Select(m => new MasinaDto
            {
                Id = m.Id,
                Marca = m.Marca,
                Model = m.Model,
                NrInmatriculare = m.NrInmatriculare,
                SerieSasiu = m.SerieSasiu,
                AnFabricatie = m.AnFabricatie,
                ClientId = m.ClientId
            })
            .ToListAsync();

        return data;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MasinaDto>> GetById(int id)
    {
        var m = await _db.Masini.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (m == null) return NotFound();

        return new MasinaDto
        {
            Id = m.Id,
            Marca = m.Marca,
            Model = m.Model,
            NrInmatriculare = m.NrInmatriculare,
            SerieSasiu = m.SerieSasiu,
            AnFabricatie = m.AnFabricatie,
            ClientId = m.ClientId
        };
    }

    [HttpPost]
    public async Task<ActionResult<MasinaDto>> Create([FromBody] MasinaDto dto)
    {
        var clientExists = await _db.Clienti.AnyAsync(c => c.Id == dto.ClientId);
        if (!clientExists) return BadRequest("ClientId invalid.");

        var duplicate = await _db.Masini.AnyAsync(m => m.NrInmatriculare == dto.NrInmatriculare);
        if (duplicate) return Conflict("NrInmatriculare exista deja.");

        var entity = new Masina
        {
            Marca = dto.Marca,
            Model = dto.Model,
            NrInmatriculare = dto.NrInmatriculare,
            SerieSasiu = dto.SerieSasiu,
            AnFabricatie = dto.AnFabricatie,
            ClientId = dto.ClientId
        };

        _db.Masini.Add(entity);
        await _db.SaveChangesAsync();

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] MasinaDto dto)
    {
        var entity = await _db.Masini.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        var clientExists = await _db.Clienti.AnyAsync(c => c.Id == dto.ClientId);
        if (!clientExists) return BadRequest("ClientId invalid.");

        var duplicate = await _db.Masini.AnyAsync(m => m.Id != id && m.NrInmatriculare == dto.NrInmatriculare);
        if (duplicate) return Conflict("NrInmatriculare exista deja.");

        entity.Marca = dto.Marca;
        entity.Model = dto.Model;
        entity.NrInmatriculare = dto.NrInmatriculare;
        entity.SerieSasiu = dto.SerieSasiu;
        entity.AnFabricatie = dto.AnFabricatie;
        entity.ClientId = dto.ClientId;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Masini.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        _db.Masini.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
