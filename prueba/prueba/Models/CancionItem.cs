using prueba.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace prueba.Models
{
    public class CancionItem
    {
        public string Id { get; set; } // Identificador de Firebase.
        public string Titulo { get; set; }
        public string Cantante { get; set; }
        public string Album { get; set; }
        public string Duracion { get; set; }

        // Indica si la canción está seleccionada.
        public bool EstaSeleccionada { get; set; }
    }
}
