using System.ComponentModel.DataAnnotations;

namespace ServiceAuto.Api.Entities;

public class Client
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Nume { get; set; } = string.Empty;

    [MaxLength(30)]
    public string Telefon { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public ICollection<Masina> Masini { get; set; } = new List<Masina>();
}
