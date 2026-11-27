using System.Collections.Generic; // Necesario para Dictionary
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks; // Necesario para Task

namespace prueba.Services
{
    public class FirebaseDatabaseService
    {
        private const string BaseUrl = "https://dam2026-d40f5-default-rtdb.europe-west1.firebasedatabase.app";
        private static readonly HttpClient Client = new HttpClient();

        public string? LastError { get; private set; }

        // =========================================================================
        // 1. GET - LEER DATOS PRIVADOS DEL USUARIO (Playlists)
        // =========================================================================
        public async Task<string> ObtenerDatosAsync(string ruta)
        {
            LastError = null;
            if (!SesionActual.EstaLogeado)
            {
                LastError = "No hay una sesión Firebase activa.";
                return null;
            }

            string rutaLimpia = ruta.Trim('/');
            string url = $"{BaseUrl}/usuarios/{Uri.EscapeDataString(SesionActual.Uid!)}/{rutaLimpia}.json?auth={Uri.EscapeDataString(SesionActual.Token!)}";
            try
            {
                using HttpResponseMessage respuesta = await Client.GetAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    return await respuesta.Content.ReadAsStringAsync();
                }
                LastError = await CrearMensajeError(respuesta);
                return null;
            }
            catch (HttpRequestException ex) { LastError = $"No se pudo conectar con Firebase: {ex.Message}"; return null; }
        }

        // =========================================================================
        // 2. GET - LEER EL CATÁLOGO GLOBAL (Canciones)
        // =========================================================================
        public async Task<string> ObtenerCatalogoAsync(string ruta)
        {
            LastError = null;
            if (!SesionActual.EstaLogeado) { LastError = "No hay una sesión Firebase activa."; return null; }
            string url = $"{BaseUrl}/catalogo/{ruta.Trim('/')}.json?auth={Uri.EscapeDataString(SesionActual.Token!)}";
            try
            {
                using HttpResponseMessage respuesta = await Client.GetAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    return await respuesta.Content.ReadAsStringAsync();
                }
                LastError = await CrearMensajeError(respuesta);
                return null;
            }
            catch (HttpRequestException ex) { LastError = $"No se pudo conectar con Firebase: {ex.Message}"; return null; }
        }

        private static async Task<string> CrearMensajeError(HttpResponseMessage respuesta)
        {
            string detalle = await respuesta.Content.ReadAsStringAsync();
            return $"Firebase devolvió {(int)respuesta.StatusCode} ({respuesta.ReasonPhrase}): {detalle}";
        }

        // =========================================================================
        // 3. POST - CREAR UNA PLAYLIST COMPLETA (Con o sin canciones)
        // =========================================================================
        public async Task<bool> CrearPlaylistCompletaAsync(string nombrePlaylist, Dictionary<string, bool> cancionesSeleccionadas)
        {
            LastError = null;
            if (!SesionActual.EstaLogeado) { LastError = "No hay una sesión Firebase activa."; return false; }
            string url = $"{BaseUrl}/usuarios/{Uri.EscapeDataString(SesionActual.Uid!)}/playlists.json?auth={Uri.EscapeDataString(SesionActual.Token!)}";

            var nuevaPlaylist = new
            {
                nombre = nombrePlaylist,
                canciones = cancionesSeleccionadas
            };

            string json = JsonSerializer.Serialize(nuevaPlaylist);
            using var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using HttpResponseMessage respuesta = await Client.PostAsync(url, contenido);
                if (!respuesta.IsSuccessStatusCode) LastError = await CrearMensajeError(respuesta);
                return respuesta.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex) { LastError = $"No se pudo conectar con Firebase: {ex.Message}"; return false; }
        }

        // =========================================================================
        // 4. PATCH - AÑADIR UNA CANCIÓN A UNA PLAYLIST EXISTENTE
        // =========================================================================
        public async Task<bool> AnadirCancionAPlaylistAsync(string idPlaylist, string idCancion)
        {
            // Vamos a la caja de "canciones" de esa playlist específica
            string url = $"{BaseUrl}/usuarios/{SesionActual.Uid}/playlists/{idPlaylist}/canciones.json?auth={SesionActual.Token}";

            using (HttpClient client = new HttpClient())
            {
                // Creamos un mini objeto: { "can_01": true }
                var nuevaCancion = new Dictionary<string, bool> { { idCancion, true } };
                string json = JsonSerializer.Serialize(nuevaCancion);
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    // PATCH fusiona esto sin borrar las canciones que ya hubiera
                    HttpResponseMessage respuesta = await client.PatchAsync(url, contenido);
                    return respuesta.IsSuccessStatusCode;
                }
                catch { return false; }
            }
        }

        // =========================================================================
        // 5. DELETE - BORRAR UNA PLAYLIST ENTERA
        // =========================================================================
        public async Task<bool> EliminarDatoAsync(string ruta, string idRegistro)
        {
            string url = $"{BaseUrl}/usuarios/{SesionActual.Uid}/{ruta}/{idRegistro}.json?auth={SesionActual.Token}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage respuesta = await client.DeleteAsync(url);
                    return respuesta.IsSuccessStatusCode;
                }
                catch { return false; }
            }
        }
    }
}