using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prueba
{
    // Modelo de datos para cada fila de la tabla de canciones
    public class CancionItem
    {
        public string Numero { get; set; } = "";
        public string Inicial { get; set; } = "";
        public string Titulo { get; set; } = "";
        public string Cantante { get; set; } = "";
        public string Album { get; set; } = "";
        public string Duracion { get; set; } = "";
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Lista maestra con todas las canciones y lista temporal para el botón '+'
        private readonly List<CancionItem> _todasLasCanciones = new List<CancionItem>();
        private readonly List<CancionItem> _miListaCanciones = new List<CancionItem>();

        public MainWindow()
        {
            InitializeComponent();
            CargarCanciones();
        }

        private void MenuInicio_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;
        }

        private void MenuArtistas_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
        }

        private void MenuCanciones_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 2;
        }

        private void MenuListas_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 3;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MainTabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CargarCanciones()//en un futuro ira aqui lo de fire base
        {
            _todasLasCanciones.Clear();

            _todasLasCanciones.AddRange(new[]
            {
                new CancionItem { Titulo = "Lo que queda", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "3:54" },
                new CancionItem { Titulo = "Kilómetro cero", Cantante = "Los Días Largos", Album = "Campo abierto", Duracion = "4:28" },
                new CancionItem { Titulo = "Luz de neón", Cantante = "Norte Magnético", Album = "Orbital", Duracion = "3:12" },
                new CancionItem { Titulo = "Cerca del mar", Cantante = "Silva", Album = "Agua dulce", Duracion = "3:41" },
                new CancionItem { Titulo = "Todo vuelve", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "4:07" },
                new CancionItem { Titulo = "A contraluz", Cantante = "Los Días Largos", Album = "Campo abierto", Duracion = "4:46" },
                new CancionItem { Titulo = "Satélite", Cantante = "Norte Magnético", Album = "Orbital", Duracion = "3:35" },
                new CancionItem { Titulo = "Otra ciudad", Cantante = "Silva", Album = "Agua dulce", Duracion = "3:22" },
                new CancionItem { Titulo = "Domingo gris", Cantante = "Mara Vélez", Album = "Entre líneas", Duracion = "4:18" }
            });

            AplicarFiltroCantante("Todas");
        }

        private void FiltrarCanciones_Click(object sender, RoutedEventArgs e)//funcion organizadora
        {
            if (sender is Button botonPulsado && botonPulsado.Tag is string cantanteSeleccionado)
            {
                AplicarFiltroCantante(cantanteSeleccionado);
                MarcarBotonActivo(botonPulsado);
            }
        }

        private void AplicarFiltroCantante(string cantante)//funcion que sirve para filtrar las canciones por cantante y actualizar la lista de canciones mostrada en la interfaz
        {
            var filtradas = cantante == "Todas"
                ? _todasLasCanciones
                : _todasLasCanciones.Where(c => c.Cantante == cantante).ToList();

            ListaCancionesControl.ItemsSource = filtradas;

            string palabraCancion = filtradas.Count == 1 ? "canción" : "canciones";
            TxtContadorCanciones.Text = $"{filtradas.Count} {palabraCancion} esperando su próxima lista.";
        }

        private void MarcarBotonActivo(Button botonActivo)// sirve para marcar en blanco el boton que se ha pulsado y los demas en gris en el filtro de canciones
        {
            var botonesFiltro = new[] { BtnFiltroTodas, BtnFiltroMara, BtnFiltroDias, BtnFiltroNorte, BtnFiltroSilva };

            foreach (var btn in botonesFiltro)
            {
                if (btn == null) continue;

                bool esActivo = btn == botonActivo;
                btn.Background = esActivo
                    ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECEAE4"))
                    : Brushes.Transparent;

                btn.Foreground = esActivo
                    ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0D0F12"))
                    : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9BA3B0"));

                btn.BorderBrush = esActivo
                    ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECEAE4"))
                    : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#252A32"));

                btn.FontWeight = esActivo ? FontWeights.SemiBold : FontWeights.Normal;
            }
        }

        private void AgregarCancionLista_Click(object sender, RoutedEventArgs e)// ser ala funcion que sirve para añadir la cancion a la lista de reproduccion y mostrar un mensaje de confirmacion
        {
            // Recupera el objeto CancionItem asociado a la fila donde se hizo clic
            if (sender is Button boton && boton.DataContext is CancionItem cancion)
            {
                _miListaCanciones.Add(cancion);

                MessageBox.Show(
                    $"Añadida \"{cancion.Titulo}\" ({cancion.Cantante}) a tu lista.\nTotal en tu lista: {_miListaCanciones.Count}",
                    "SoundWafe",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}