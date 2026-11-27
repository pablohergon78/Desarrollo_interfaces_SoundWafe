using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace prueba.Models
{
    public class Cancion
    {
        [JsonIgnore] // No se guarda en el JSON, solo la usamos en el código
        public string Id { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("idAlbum")]
        public string IdAlbum { get; set; }

        [JsonPropertyName("duracionSeg")]
        public int DuracionSeg { get; set; }
    }

}
