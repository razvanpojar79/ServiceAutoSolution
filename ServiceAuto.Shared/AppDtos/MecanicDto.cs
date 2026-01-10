namespace ServiceAuto.Shared.AppDtos
{
    public enum SpecializareMecanic
    {
        Mecanica,
        Electrica,
        Tinichigerie,
        Vopsitorie,
        Diagnoza,
        General
    }

    public class MecanicDto
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public SpecializareMecanic Specializare { get; set; }
        public bool EsteDisponibil { get; set; }
    }
}