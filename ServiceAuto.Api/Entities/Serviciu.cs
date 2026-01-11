using System.ComponentModel.DataAnnotations;

namespace ServiceAuto.Api.Entities;

public class Serviciu
{
    public int Id { get; set; }

    [MaxLength(120)]
    public string Denumire { get; set; } = string.Empty;

    public decimal Pret { get; set; }

    public int DurataEstimata { get; set; }

    public ICollection<Programare> Programari { get; set; } = new List<Programare>();
}
