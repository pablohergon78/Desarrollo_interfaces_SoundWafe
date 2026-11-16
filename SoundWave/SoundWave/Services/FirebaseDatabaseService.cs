using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SoundWave.Services
{
    public class FirebaseDatabaseService
    {
        // PEDIR LOS DATOS A FIREBASE. PARA ELLO NECESITAMOS EL TOKEN Y EL UID DEL USUARIO, QUE LOS TENEMOS EN SESIONACTUAL
        private const string BaseUrl = "https://dam2026-d40f5-default-rtdb.europe-west1.firebasedatabase.app/";

        // Método para hacer un GET (Leer datos)
        public async Task<string> ObtenerDatosAsync(string ruta)
        {
            // Ejemplo de ruta: "artistas" o "playlists"
            // Si no hay token, fallará por las reglas de seguridad
            string url = $"{BaseUrl}/usuarios/{SesionActual.Uid}/{ruta}.json?auth={SesionActual.Token}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage respuesta = await client.GetAsync(url);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        // Devolvemos el JSON en crudo (string)
                        return await respuesta.Content.ReadAsStringAsync();
                    }
                    return null; // Fallo
                }
                catch
                {
                    return null; // Error de red
                }
            }
        }
    }
}
