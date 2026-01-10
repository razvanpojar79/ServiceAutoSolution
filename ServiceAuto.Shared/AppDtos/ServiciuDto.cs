using System.ComponentModel.DataAnnotations;

namespace ServiceAuto.Shared.AppDtos
{
    public class ServiciuDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Denumirea serviciului este obligatorie.")]
        public string Denumire { get; set; }

        [Required(ErrorMessage = "Prețul este obligatoriu.")]
        [Range(0, double.MaxValue, ErrorMessage = "Prețul trebuie să fie pozitiv.")]
        public decimal Pret { get; set; }

        [Required(ErrorMessage = "Durata estimată este obligatorie.")]
        [Range(1, 1000, ErrorMessage = "Durata trebuie să fie validă (în minute).")]
        public int DurataEstimata { get; set; }
    }
}