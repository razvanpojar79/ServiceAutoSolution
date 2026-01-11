using System.ComponentModel.DataAnnotations;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Api.Entities;

public class Mecanic
{
    public int Id { get; set; }

    [MaxLength(120)]
    public string Nume { get; set; } = string.Empty;

    public SpecializareMecanic Specializare { get; set; }

    public bool EsteDisponibil { get; set; } = true;

    public ICollection<Programare> Programari { get; set; } = new List<Programare>();
}
