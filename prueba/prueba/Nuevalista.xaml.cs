using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace prueba
{
    public partial class NuevaListaWindow : Window
    {
        public ObservableCollection<CancionItem> Canciones { get; set; } = new ObservableCollection<CancionItem>();
        public string NombreLista { get; private set; }
        public List<CancionItem> CancionesSeleccionadas { get; private set; } = new List<CancionItem>();

        public NuevaListaWindow()
        {
            InitializeComponent();
            CargarCanciones();
        }

        private void CargarCanciones()
        {
            Canciones.Clear();
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });
            Canciones.Add(new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" });

            lstCanciones.ItemsSource = Canciones;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
           
          
            Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}