using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceAuto.Api.Data;
using ServiceAuto.Api.Entities;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Programari)]
public class ProgramariController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProgramariController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<ProgramareDto>>> GetAll()
    {
        var data = await _db.Programari
            .AsNoTracking()
            .Include(p => p.Masina)
            .ThenInclude(m => m.Client)
            .Include(p => p.Serviciu)
            .Include(p => p.Mecanic)
            .OrderByDescending(p => p.DataOra)
            .Select(p => new ProgramareDto
            {
                Id = p.Id,
                DataOra = p.DataOra,
                MasinaId = p.MasinaId,
                ServiciuId = p.ServiciuId,
                MecanicId = p.MecanicId,
                DescriereProblema = p.DescriereProblema,
                Status = p.Status,
                NumeClient = p.Masina.Client.Nume,
                MasinaInfo = p.Masina.Marca + " " + p.Masina.Model + " (" + p.Masina.NrInmatriculare + ")",
                DenumireServiciu = p.Serviciu.Denumire
            })
            .ToListAsync();

        return data;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProgramareDto>> GetById(int id)
    {
        var p = await _db.Programari
            .AsNoTracking()
            .Include(x => x.Masina).ThenInclude(m => m.Client)
            .Include(x => x.Serviciu)
            .Include(x => x.Mecanic)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (p == null) return NotFound();

        return new ProgramareDto
        {
            Id = p.Id,
            DataOra = p.DataOra,
            MasinaId = p.MasinaId,
            ServiciuId = p.ServiciuId,
            MecanicId = p.MecanicId,
            DescriereProblema = p.DescriereProblema,
            Status = p.Status,
            NumeClient = p.Masina.Client.Nume,
            MasinaInfo = p.Masina.Marca + " " + p.Masina.Model + " (" + p.Masina.NrInmatriculare + ")",
            DenumireServiciu = p.Serviciu.Denumire
        };
    }

    [HttpPost]
    public async Task<ActionResult<ProgramareDto>> Create([FromBody] ProgramareDto dto)
    {
       
        if (dto == null) return BadRequest();
        if (dto.DataOra == default) return BadRequest("DataOra invalida.");
        if (dto.MasinaId <= 0) return BadRequest("MasinaId invalid.");
        if (dto.ServiciuId <= 0) return BadRequest("ServiciuId invalid.");

        var masinaExists = await _db.Masini.AnyAsync(m => m.Id == dto.MasinaId);
        if (!masinaExists) return BadRequest("MasinaId invalid.");

        var serviciuExists = await _db.Servicii.AnyAsync(s => s.Id == dto.ServiciuId);
        if (!serviciuExists) return BadRequest("ServiciuId invalid.");

        if (dto.MecanicId.HasValue)
        {
            var mecanicExists = await _db.Mecanici.AnyAsync(m => m.Id == dto.MecanicId.Value);
            if (!mecanicExists) return BadRequest("MecanicId invalid.");
        }

        var entity = new Programare
        {
            DataOra = dto.DataOra,
            MasinaId = dto.MasinaId,
            ServiciuId = dto.ServiciuId,
            MecanicId = dto.MecanicId,
            DescriereProblema = dto.DescriereProblema,
            Status = dto.Status
        };

        _db.Programari.Add(entity);
        await _db.SaveChangesAsync();

        dto.Id = entity.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProgramareDto dto)
    {
        
        if (dto == null) return BadRequest();
        if (dto.DataOra == default) return BadRequest("DataOra invalida.");
        if (dto.MasinaId <= 0) return BadRequest("MasinaId invalid.");
        if (dto.ServiciuId <= 0) return BadRequest("ServiciuId invalid.");

        var entity = await _db.Programari.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        var masinaExists = await _db.Masini.AnyAsync(m => m.Id == dto.MasinaId);
        if (!masinaExists) return BadRequest("MasinaId invalid.");

        var serviciuExists = await _db.Servicii.AnyAsync(s => s.Id == dto.ServiciuId);
        if (!serviciuExists) return BadRequest("ServiciuId invalid.");

        if (dto.MecanicId.HasValue)
        {
            var mecanicExists = await _db.Mecanici.AnyAsync(m => m.Id == dto.MecanicId.Value);
            if (!mecanicExists) return BadRequest("MecanicId invalid.");
        }

        entity.DataOra = dto.DataOra;
        entity.MasinaId = dto.MasinaId;
        entity.ServiciuId = dto.ServiciuId;
        entity.MecanicId = dto.MecanicId;
        entity.DescriereProblema = dto.DescriereProblema;
        entity.Status = dto.Status;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Programari.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();

        _db.Programari.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
