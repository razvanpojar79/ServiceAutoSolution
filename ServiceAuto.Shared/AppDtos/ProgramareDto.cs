using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceAuto.Shared.AppDtos
{
    public enum StatusProgramare
    {
        InAsteptare,
        Preluata,
        InLucru,
        Finalizata,
        Anulata
    }

    public class ProgramareDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data și ora sunt obligatorii.")]
        public DateTime DataOra { get; set; }

        public int MasinaId { get; set; }

        public int ServiciuId { get; set; }

        public string DescriereProblema { get; set; }

        public int? MecanicId { get; set; }

        public StatusProgramare Status { get; set; } = StatusProgramare.InAsteptare;

        public string NumeClient { get; set; }

        public string MasinaInfo { get; set; }

        public string DenumireServiciu { get; set; }
    }
}