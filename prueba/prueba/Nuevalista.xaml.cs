using prueba.Services;
using prueba.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

namespace prueba // Asegúrate de que coincide con tu namespace (el tuyo pone 'prueba')
{
    public partial class NuevaListaWindow : Window
    {
        private FirebaseDatabaseService _dbService = new FirebaseDatabaseService();

        public ObservableCollection<CancionItem> Canciones { get; set; } = new ObservableCollection<CancionItem>();
        public string NombreLista { get; private set; }
        public List<CancionItem> CancionesSeleccionadas { get; private set; } = new List<CancionItem>();

        public NuevaListaWindow()
        {
            InitializeComponent();
            lstCanciones.ItemsSource = Canciones;

            // Carga las canciones de Firebase.
            _ = CargarCancionesDesdeFirebase();
        }

        // Carga las canciones disponibles.
        private async System.Threading.Tasks.Task CargarCancionesDesdeFirebase()
        {
            Canciones.Clear();

            string json = await _dbService.ObtenerCatalogoAsync("canciones");

            if (string.IsNullOrEmpty(json) || json == "null")
            {
                MessageBox.Show("No hay canciones en el catálogo de Firebase.");
                return;
            }

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var diccionario = JsonSerializer.Deserialize<Dictionary<string, Cancion>>(json, opciones);

            foreach (var item in diccionario)
            {
                // Adapta la canción al formato de la ventana.
                Canciones.Add(new CancionItem
                {
                    Id = item.Key, // El ID de Firebase
                    Titulo = item.Value.Titulo,
                    Cantante = "Artista Desconocido", // En Firebase solo guardamos IDAlbum, podríamos buscar el nombre del artista, pero para no liar, ponemos esto (o lo dejáis vacío)
                    Album = item.Value.IdAlbum,
                    Duracion = $"{item.Value.DuracionSeg / 60}:{item.Value.DuracionSeg % 60:D2} min",
                    EstaSeleccionada = false
                });
            }
        }

        private async void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Obtiene el nombre escrito en el formulario.
            string nombre = txtNombreLista.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Escribe un nombre para la lista.");
                return;
            }

            // Obtiene las canciones seleccionadas.
            CancionesSeleccionadas = Canciones.Where(c => c.EstaSeleccionada).ToList();

            if (CancionesSeleccionadas.Count == 0)
            {
                MessageBox.Show("Selecciona al menos una canción.");
                return;
            }

            // Prepara las canciones para Firebase.
            Dictionary<string, bool> dictCanciones = new Dictionary<string, bool>();
            foreach (var cancion in CancionesSeleccionadas)
            {
                dictCanciones.Add(cancion.Id, true);
            }

            // Guarda la lista en Firebase.
            bool exito = await _dbService.CrearPlaylistCompletaAsync(nombre, dictCanciones);

            if (exito)
            {
                NombreLista = nombre; // Guardamos el nombre para que la ventana principal lo sepa
                Close();
            }
            else
            {
                MessageBox.Show(_dbService.LastError ?? "Firebase rechazó la operación.", "Firebase", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}