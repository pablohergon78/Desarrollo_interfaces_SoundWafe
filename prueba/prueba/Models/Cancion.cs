using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace prueba.Models
{
    public class Cancion
    {
        [JsonIgnore] // Solo se usa dentro de la aplicación.
        public string Id { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("idAlbum")]
        public string IdAlbum { get; set; }

        [JsonPropertyName("idArtista")]
        public string IdArtista { get; set; }

        [JsonPropertyName("duracionSeg")]
        public int DuracionSeg { get; set; }
    }

}
