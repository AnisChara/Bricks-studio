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

namespace Bricks_Interfaces
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

        private void AjouterTextBox(Panel parentPanel, double width = 200, double height = 30, string text = "Nouvelle TextBox", Thickness? margin = null)
        {

            TextBox nouvelleTextBox = new TextBox
            {
                Width = width,
                Height = height,
                Margin = margin ?? new Thickness(5),
                Text = text
            };


            parentPanel.Children.Add(nouvelleTextBox);
        }
    }
}