using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAuto.Shared.AppDtos
{
    public class ClientDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Numele este prea lung.")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Telefonul este obligatoriu.")]
        [Phone(ErrorMessage = "Numărul de telefon nu este valid.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Emailul este obligatoriu.")]
        [EmailAddress(ErrorMessage = "Formatul emailului nu este valid.")]
        public string Email { get; set; }
    }
}