using prueba.Models;
using prueba.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace prueba
{
    public partial class agregarcancionesextra : Window
    {
        private FirebaseDatabaseService _dbService = new FirebaseDatabaseService();
        private string _idPlaylistSeleccionada;

        public ObservableCollection<CancionItem> Catalogo { get; set; } = new ObservableCollection<CancionItem>();

        public agregarcancionesextra(string idPlaylist, IEnumerable<string>? cancionesYaIncluidas = null)
        {
            InitializeComponent();
            _idPlaylistSeleccionada = idPlaylist;

            // Enlaza las canciones con la lista visual.
            lstCanciones.ItemsSource = Catalogo;

            _ = CargarCatalogoAsync(cancionesYaIncluidas ?? Enumerable.Empty<string>());
        }

        private async System.Threading.Tasks.Task CargarCatalogoAsync(IEnumerable<string> cancionesYaIncluidas)
        {
            var idsIncluidos = cancionesYaIncluidas.ToHashSet();
            string json = await _dbService.ObtenerCatalogoAsync("canciones");
            if (string.IsNullOrEmpty(json) || json == "null") return;

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var diccionario = JsonSerializer.Deserialize<Dictionary<string, Cancion>>(json, opciones);
            var albumes = await CargarCatalogoAsync<Album>(opciones, "albumes", "albums", "album");
            var artistas = await CargarCatalogoAsync<Artista>(opciones, "artista", "artistas");

            if (diccionario == null) return;

            foreach (var item in diccionario)
            {
                Album? album = item.Value.IdAlbum != null && albumes.TryGetValue(item.Value.IdAlbum, out var albumEncontrado)
                    ? albumEncontrado
                    : null;
                string nombreAlbum = album?.Nombre ?? album?.Titulo ?? item.Value.IdAlbum ?? "Álbum desconocido";
                string idArtista = album?.IdArtista ?? item.Value.IdArtista;
                string nombreArtista = !string.IsNullOrWhiteSpace(idArtista) && artistas.TryGetValue(idArtista, out var artista)
                    ? artista.Nombre
                    : "Artista desconocido";

                Catalogo.Add(new CancionItem
                {
                    Id = item.Key,
                    Titulo = item.Value.Titulo,
                    Cantante = nombreArtista,
                    Album = nombreAlbum,
                    Duracion = $"{item.Value.DuracionSeg / 60}:{item.Value.DuracionSeg % 60:D2}",
                    EstaSeleccionada = idsIncluidos.Contains(item.Key)
                });
            }
        }

        private async System.Threading.Tasks.Task<Dictionary<string, T>> CargarCatalogoAsync<T>(JsonSerializerOptions opciones, params string[] rutas)
        {
            foreach (string ruta in rutas)
            {
                string json = await _dbService.ObtenerCatalogoAsync(ruta);
                if (!string.IsNullOrWhiteSpace(json) && json != "null")
                {
                    return JsonSerializer.Deserialize<Dictionary<string, T>>(json, opciones)
                        ?? new Dictionary<string, T>();
                }
            }

            return new Dictionary<string, T>();
        }

        private async void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var seleccionadas = Catalogo.Where(c => c.EstaSeleccionada).ToList();

            if (seleccionadas.Count == 0)
            {
                MessageBox.Show("Selecciona al menos una canción.");
                return;
            }

            bool todoExito = true;
            foreach (var cancion in seleccionadas)
            {
                bool exito = await _dbService.AnadirCancionAPlaylistAsync(_idPlaylistSeleccionada, cancion.Id);
                if (!exito) todoExito = false;
            }

            if (todoExito)
            {
                MessageBox.Show("Canciones añadidas correctamente.");
                Close();
            }
            else
            {
                MessageBox.Show("Hubo un error al añadir algunas canciones.");
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}