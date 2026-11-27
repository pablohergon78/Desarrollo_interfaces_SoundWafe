using prueba.Services;
using System;
using System.Windows;
using System.Windows.Media;

namespace prueba
{
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        private async void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                lblMensaje.Text = "Rellena todos los campos.";
                lblMensaje.Foreground = Brushes.OrangeRed;
                return;
            }

            btnEntrar.IsEnabled = false;
            lblMensaje.Text = "Conectando...";
            lblMensaje.Foreground = Brushes.Gray;

            FirebaseAuthService auth = new FirebaseAuthService();
            bool exito = await auth.LoginAsync(email, password);

            if (exito)
            {
                MainWindow principal = new MainWindow();
                principal.Show();
                this.Close();
            }
            else
            {
                lblMensaje.Text = auth.LastError ?? "Firebase no pudo validar la sesión.";
                lblMensaje.Foreground = Brushes.Red;
                btnEntrar.IsEnabled = true;
            }
        }
    }
}