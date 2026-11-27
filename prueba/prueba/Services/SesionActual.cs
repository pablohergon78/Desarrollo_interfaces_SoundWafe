namespace prueba.Services
{
    public static class SesionActual
    {
        public static string? Token { get; set; }
        public static string? Uid { get; set; }
        public static string? Email { get; set; }

        public static bool EstaLogeado => !string.IsNullOrWhiteSpace(Token) && !string.IsNullOrWhiteSpace(Uid);
    }
}