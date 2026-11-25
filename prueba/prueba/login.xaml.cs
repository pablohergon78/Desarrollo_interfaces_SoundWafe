using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace prueba
{
    /// <summary>
    /// Lógica de interacción para login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();

            // Un toque gracioso en el título de la ventana
            this.Title = "SoundWafe 🎧 - Dale al play";
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            // Un pequeño aviso simpático antes de entrar
            MessageBox.Show("¡Subiendo el volumen! 🎶 Preparando tu música...", "SoundWafe", MessageBoxButton.OK, MessageBoxImage.Information);

            new MainWindow().Show();
            Close();
        }
    }

}