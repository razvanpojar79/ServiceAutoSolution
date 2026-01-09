namespace ServiceAuto.Shared
{
    public static class ApiRoutes
    {
        public const string Base = "api";

        // Auth
        public const string Login = $"{Base}/auth/login";
        public const string Register = $"{Base}/auth/register";

        // Entitati
        public const string Clienti = $"{Base}/clienti";
        public const string Masini = $"{Base}/masini";
        public const string Mecanici = $"{Base}/mecanici";
        public const string Servicii = $"{Base}/servicii";
        public const string Programari = $"{Base}/programari";
    }
}