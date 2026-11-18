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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}