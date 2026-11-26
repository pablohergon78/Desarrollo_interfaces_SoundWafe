using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace prueba.Services
{
    public class FirebaseDatabaseService
    {
        private const string BaseUrl = "https://dam2026-d40f5-default-rtdb.europe-west1.firebasedatabase.app";

        public async Task<string> ObtenerDatosAsync(string ruta)
        {
            string rutaLimpia = ruta.Trim('/');
            string url = $"{BaseUrl}/usuarios/{SesionActual.Uid}/{rutaLimpia}.json?auth={SesionActual.Token}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage respuesta = await client.GetAsync(url);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return await respuesta.Content.ReadAsStringAsync();
                    }

                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}