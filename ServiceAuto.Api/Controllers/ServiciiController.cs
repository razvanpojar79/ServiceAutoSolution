using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceAuto.Api.Data;
using ServiceAuto.Api.Entities;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Servicii)]
public class ServiciiController : ControllerBase
{
    private readonly AppDbContext _db;

    public ServiciiController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<ServiciuDto>>> GetAll()
    {
        var data = await _db.Servicii
            .AsNoTracking()
            .OrderBy(s => s.Denumire)
            .Select(s => new ServiciuDto
            {
                Id = s.Id,
                Denumire = s.Denumire,
                Pret = s.Pret,
                DurataEstimata = s.DurataEstimata
            })
            .ToListAsync();

        return data;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiciuDto>> GetById(int id)
    {
        var s = await _db.Servicii.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (s == null) return NotFound();

        return new ServiciuDto
        {
            Id = s.Id,
            Denumire = s.Denumire,
            Pret = s.Pret,
            DurataEstimata = s.DurataEstimata
        };
    }

    [HttpPost]
    public async Task<ActionResult<ServiciuDto>> Create([FromBody] ServiciuDto dto)
    {
        var entity = new Serviciu
        {
            Denumire = dto.Denumire,
            Pret = dto.Pret,
            DurataEstimata = dto.DurataEstimata
        };

        _db.Servicii.Add(entity);
        await _db.SaveChangesAsync();

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ServiciuDto dto)
    {
        var entity = await _db.Servicii.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        entity.Denumire = dto.Denumire;
        entity.Pret = dto.Pret;
        entity.DurataEstimata = dto.DurataEstimata;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Servicii.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        _db.Servicii.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
