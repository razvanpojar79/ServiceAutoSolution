using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceAuto.Shared.AppDtos
{
    public class MasinaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Marca este obligatorie.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Modelul este obligatoriu.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Numărul de înmatriculare este obligatoriu.")]
        public string NrInmatriculare { get; set; }

        [Range(1950, 2100, ErrorMessage = "Anul fabricației trebuie să fie valid.")]
        public int AnFabricatie { get; set; }

        [Required(ErrorMessage = "Trebuie selectat un client (proprietar).")]
        public int ClientId { get; set; }
    }
}