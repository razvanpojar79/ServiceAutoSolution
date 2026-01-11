using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceAuto.Api.Data;
using ServiceAuto.Api.Entities;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Mecanici)]
public class MecaniciController : ControllerBase
{
    private readonly AppDbContext _db;

    public MecaniciController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<MecanicDto>>> GetAll()
    {
        var data = await _db.Mecanici
            .AsNoTracking()
            .OrderBy(m => m.Nume)
            .Select(m => new MecanicDto
            {
                Id = m.Id,
                Nume = m.Nume,
                Specializare = m.Specializare,
                EsteDisponibil = m.EsteDisponibil
            })
            .ToListAsync();

        return data;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MecanicDto>> GetById(int id)
    {
        var m = await _db.Mecanici.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (m == null) return NotFound();

        return new MecanicDto
        {
            Id = m.Id,
            Nume = m.Nume,
            Specializare = m.Specializare,
            EsteDisponibil = m.EsteDisponibil
        };
    }

    [HttpPost]
    public async Task<ActionResult<MecanicDto>> Create([FromBody] MecanicDto dto)
    {
        var entity = new Mecanic
        {
            Nume = dto.Nume,
            Specializare = dto.Specializare,
            EsteDisponibil = dto.EsteDisponibil
        };

        _db.Mecanici.Add(entity);
        await _db.SaveChangesAsync();

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] MecanicDto dto)
    {
        var entity = await _db.Mecanici.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        entity.Nume = dto.Nume;
        entity.Specializare = dto.Specializare;
        entity.EsteDisponibil = dto.EsteDisponibil;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Mecanici.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        _db.Mecanici.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
