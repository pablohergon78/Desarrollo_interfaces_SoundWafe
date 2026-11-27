using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace prueba.Services
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyCMazIKm0wtecmkHnf7zXalaVvymuIzBSk";

        public async Task<bool> LoginAsync(string email, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                var datosLogin = new { email = email, password = password, returnSecureToken = true };
                string json = JsonSerializer.Serialize(datosLogin);
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                string url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={ApiKey}";

                try
                {
                    HttpResponseMessage respuesta = await client.PostAsync(url, contenido);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(jsonRespuesta))
                        {
                            SesionActual.Token = doc.RootElement.GetProperty("idToken").GetString();
                            SesionActual.Uid = doc.RootElement.GetProperty("localId").GetString();
                            return true;
                        }
                    }

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}