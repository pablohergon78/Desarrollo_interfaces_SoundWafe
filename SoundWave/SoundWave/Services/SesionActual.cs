using System;
using System.Collections.Generic;
using System.Text;

namespace SoundWave.Services
{
    public static class SesionActual
    {
        // Aquí guardaremos el Token (el DNI) y el UID (Id del usuario)
        public static string Token { get; set; }
        public static string Uid { get; set; }

        // si el token no está vacío, es que estamos logueados
        public static bool EstaLogeado => !string.IsNullOrEmpty(Token);
    }
}
