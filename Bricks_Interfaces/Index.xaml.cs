using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bricks_Interfaces
{
    /// <summary>
    /// Logique d'interaction pour Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        public Index()
        {
            InitializeComponent();
        }

        private void OnNewProjectClick(object sender, RoutedEventArgs e)
        {
            // Créez une instance de la fenêtre de création de projet et affichez-la
            var mainWindow = new MainWindow("path");

            // Synchroniser les dimensions et l'état de la fenêtre
            mainWindow.Width = 1280;
            mainWindow.Height = 720;
            mainWindow.WindowState = this.WindowState;

            // Synchroniser la position de la fenêtre
            mainWindow.Left = 0;
            mainWindow.Top = 0;

            mainWindow.Show();
            this.Close(); // Fermez la fenêtre actuelle si nécessaire
        }

        private void OnOpenProjectClick(object sender, RoutedEventArgs e)
        {
            // Ouvrir une boîte de dialogue pour sélectionner un projet (optionnel)
            // Exemple : OpenFileDialog openFileDialog = new OpenFileDialog();

            // Ouvrir la fenêtre principale après sélection (ou directement)
            var mainWindow = new MainWindow("path");

            // Synchroniser les dimensions et l'état de la fenêtre
            mainWindow.Width = 1280;
            mainWindow.Height = 720;
            mainWindow.WindowState = this.WindowState;

            // Synchroniser la position de la fenêtre
            mainWindow.Left = 0;
            mainWindow.Top = 0;

            mainWindow.Show();
            this.Close(); // Fermer la fenêtre d'accueil
        }
    }
}
