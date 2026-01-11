using System.ComponentModel.DataAnnotations;

namespace ServiceAuto.Api.Entities;

public class Masina
{
    public int Id { get; set; }

    [MaxLength(80)]
    public string Marca { get; set; } = string.Empty;

    [MaxLength(80)]
    public string Model { get; set; } = string.Empty;

    [MaxLength(20)]
    public string NrInmatriculare { get; set; } = string.Empty;

    [MaxLength(60)]
    public string? SerieSasiu { get; set; }

    public int AnFabricatie { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public ICollection<Programare> Programari { get; set; } = new List<Programare>();
}
