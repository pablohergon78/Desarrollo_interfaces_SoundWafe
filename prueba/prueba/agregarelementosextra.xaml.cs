using prueba.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace prueba
{
    public partial class agregarcancionesextra : Window
    {
        public ObservableCollection<CancionItem> Canciones { get; set; } = new ObservableCollection<CancionItem>();
        public List<CancionItem> CancionesSeleccionadas { get; private set; } = new List<CancionItem>();
        public bool Guardado { get; private set; }

        public agregarcancionesextra()
        {
            InitializeComponent();
            lstCanciones.ItemsSource = Canciones;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            CancionesSeleccionadas = Canciones.Where(c => c.EstaSeleccionada).ToList();
            Guardado = true;
            Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Guardado = false;
            Close();
        }
    }
}