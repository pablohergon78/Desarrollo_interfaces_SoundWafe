using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace prueba.Models
{
    public class Artista
    {
        [JsonIgnore]
        public string Id { get; set; } = "";

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";

        [JsonPropertyName("genero")]
        public string Genero { get; set; } = "";

        [JsonPropertyName("imagenUrl")]
        public string ImagenUrl { get; set; } = "";
    }
}
