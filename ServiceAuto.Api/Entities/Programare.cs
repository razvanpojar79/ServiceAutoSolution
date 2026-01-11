using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Api.Entities;

public class Programare
{
    public int Id { get; set; }

    public DateTime DataOra { get; set; }

    public int MasinaId { get; set; }
    public Masina Masina { get; set; } = null!;

    public int ServiciuId { get; set; }
    public Serviciu Serviciu { get; set; } = null!;

    public string? DescriereProblema { get; set; }

    public int? MecanicId { get; set; }
    public Mecanic? Mecanic { get; set; }

    public StatusProgramare Status { get; set; } = StatusProgramare.InAsteptare;
}
