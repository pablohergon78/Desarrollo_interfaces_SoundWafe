using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace prueba.Services
{
    public class FirebaseDatabaseService
    {
        private const string BaseUrl = "https://dam2026-d40f5-default-rtdb.europe-west1.firebasedatabase.app";


        // =========================================================================
        // 2. GET - LEER EL CATÁLOGO GLOBAL (Artistas, Álbumes, Canciones)
        // =========================================================================
        public async Task<string> ObtenerCatalogoAsync(string ruta)
        {
            // Esta URL NO lleva el UID del usuario. Es la carpeta pública que comparten todos.
            string url = $"{BaseUrl}/catalogo/{ruta}.json?auth={SesionActual.Token}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage respuesta = await client.GetAsync(url);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        return await respuesta.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        // ¡AQUÍ ESTÁ EL TRUCO! Vamos a ver qué dice Firebase
                        string errorFirebase = await respuesta.Content.ReadAsStringAsync();
                        System.Windows.MessageBox.Show($"Error {respuesta.StatusCode}: {errorFirebase}");
                        return null;
                    }
                }
                catch { return null; }
            }
        }
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
        // Método POST para crear una Playlist ya con sus canciones
        public async Task<bool> CrearPlaylistCompletaAsync(string nombrePlaylist, Dictionary<string, bool> cancionesSeleccionadas)
        {
            string url = $"{BaseUrl}/usuarios/{SesionActual.Uid}/playlists.json?auth={SesionActual.Token}";

            using (HttpClient client = new HttpClient())
            {
                // Creamos el objeto completo que Firebase espera recibir
                var nuevaPlaylist = new
                {
                    nombre = nombrePlaylist,
                    canciones = cancionesSeleccionadas // Si está vacío, se enviará un objeto vacío, lo cual está bien
                };

                string json = JsonSerializer.Serialize(nuevaPlaylist);
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage respuesta = await client.PostAsync(url, contenido);
                    return respuesta.IsSuccessStatusCode;
                }
                catch { return false; }
            }
        }
    }
}