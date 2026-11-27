using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace prueba.Models
{
    public class Album
    {
        [JsonIgnore]
        public string Id { get; set; } = "";

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; } = "";

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = "";

        [JsonPropertyName("idArtista")]
        public string IdArtista { get; set; } = "";

        [JsonIgnore]
        public string Subtitulo { get; set; } = "";


        [JsonPropertyName("imagenUrl")]
        public string ImagenUrl { get; set; } = "";
    }

}
