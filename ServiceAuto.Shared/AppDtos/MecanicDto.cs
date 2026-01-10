using System;
using System.ComponentModel.DataAnnotations;
using ServiceAuto.Shared.Enums;

namespace ServiceAuto.Shared.AppDtos
{
    public class MecanicDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele mecanicului este obligatoriu.")]
        [StringLength(100, ErrorMessage = "Numele este prea lung.")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Specializarea este obligatorie.")]
        public SpecializareMecanic Specializare { get; set; }

        public bool EsteActiv { get; set; } = true;
    }
}