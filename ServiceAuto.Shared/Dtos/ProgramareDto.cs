using System;
using System.ComponentModel.DataAnnotations;
using ServiceAuto.Shared.Enums;

namespace ServiceAuto.Shared.Dtos
{
    public class ProgramareDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data și ora sunt obligatorii.")]
        public DateTime DataOra { get; set; }

        [Required(ErrorMessage = "Trebuie selectată o mașină.")]
        public int MasinaId { get; set; }

        [Required(ErrorMessage = "Trebuie selectat un serviciu.")]
        public int ServiciuId { get; set; }

        [Required(ErrorMessage = "Vă rugăm descrieți problema pe scurt.")]
        public string DescriereProblema { get; set; }

        public int? MecanicId { get; set; }

        public StatusProgramare Status { get; set; } = StatusProgramare.InAsteptare;

        public string NumeClient { get; set; }

        public string MasinaInfo { get; set; }

        public string DenumireServiciu { get; set; }
    }
}