using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace prueba.Models
{
    public class Lista
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        // El diccionario que guarda las canciones: { "can_01": true }
        [JsonPropertyName("canciones")]
        public Dictionary<string, bool> Canciones { get; set; }
    }

}
