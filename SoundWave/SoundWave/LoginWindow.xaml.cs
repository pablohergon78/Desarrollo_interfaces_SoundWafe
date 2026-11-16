using SoundWave.Services;
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

namespace SoundWave
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

        }

        private async void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password; // ¡Ojo, aquí no es .Text, es .Password!

            // Validación básica
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                lblMensaje.Text = "Rellena todos los campos.";
                return;
            }

            // Deshabilitamos el botón para que no le den 2 veces mientras carga
            btnEntrar.IsEnabled = false;
            lblMensaje.Text = "Conectando...";
            lblMensaje.Foreground = System.Windows.Media.Brushes.Gray;

            FirebaseAuthService auth = new FirebaseAuthService();

            // ⚠️ AQUÍ USAMOS AWAIT. La ventana no se congela, el botón se queda deshabilitado esperando.
            bool exito = await auth.LoginAsync(email, password);

            if (exito)
            {
                // Abrimos la ventana principal y cerramos la de login
                MainWindow principal = new MainWindow();
                principal.Show();
                this.Close();
            }
            else
            {
                lblMensaje.Text = "Correo o contraseña incorrectos.";
                lblMensaje.Foreground = System.Windows.Media.Brushes.Red;
                btnEntrar.IsEnabled = true; // Volvemos a activar el botón
            }
        }
    }

}
