using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SoundWave.Services
{
    public class FirebaseAuthService
    {
        // ESTO CREA EL OBJETO CON EL MAIL Y LA CONTRASEÑA. LO CONVERTIMOS A JSON Y LO ENVIAMOS A FIREBASE 
        // PARA QUE NOS DEVUELVA EL TOKEN Y EL UID DEL USUARIO
        private const string ApiKey = "AIzaSyCMazIKm0wtecmkHnf7zXalaVvymuIzBSk";

        public async Task<bool> LoginAsync(string email, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                // 1. Preparamos los datos que enviaremos a Firebase
                var datosLogin = new { email = email, password = password, returnSecureToken = true };
                string json = JsonSerializer.Serialize(datosLogin);
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                // 2. Hacemos la petición POST a la URL de Firebase
                string url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={ApiKey}";

                try
                {
                    HttpResponseMessage respuesta = await client.PostAsync(url, contenido);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        // 3. Si todo va bien, leemos la respuesta de Firebase
                        string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(jsonRespuesta))
                        {
                            // 4. ¡Guardamos el Token y el UID en nuestra SesionActual!
                            SesionActual.Token = doc.RootElement.GetProperty("idToken").GetString();
                            SesionActual.Uid = doc.RootElement.GetProperty("localId").GetString();
                            return true; // Login correcto
                        }
                    }
                    else
                    {
                        return false; // Contraseña mal o usuario no existe
                    }
                }
                catch (Exception)
                {
                    return false; // Error de red (sin internet, etc.)
                }
            }
        }
    }
}
