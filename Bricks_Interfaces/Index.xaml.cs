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
using System.Windows.Navigation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Microsoft.Win32;

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
            // Créer une boîte de dialogue pour enregistrer un fichier
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Créer un nouveau projet",
                // Filter limite les types de fichiers affichés dans le gestionnaire
                Filter = "Fichiers de projet (*.proj)|*.proj|Tous les fichiers (*.*)|*.*"
            };

            // Afficher la boîte de dialogue et vérifier si l'utilisateur a choisi un emplacement
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                // Logique pour initialiser un nouveau projet à 'filePath'
                // MessageBox.Show($"Nouveau projet créé à : {filePath}");

                // Créez une instance de la fenêtre de création de projet et affichez-la
                var mainWindow = new MainWindow(filePath);

                // Synchroniser les dimensions et l'état de la fenêtre
                mainWindow.Width = this.Width;
                mainWindow.Height = this.Height;
                mainWindow.WindowState = this.WindowState;

                // Synchroniser la position de la fenêtre
                mainWindow.Left = this.Left;
                mainWindow.Top = this.Top;

                mainWindow.Show();
                this.Close(); // Fermez la fenêtre actuelle si nécessaire
            }
        }

        private void OnOpenProjectClick(object sender, RoutedEventArgs e)
        {
            // Créer une boîte de dialogue pour sélectionner un fichier
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Ouvrir un projet",
                // Filter limite les types de fichiers affichés dans le gestionnaire
                Filter = "Fichiers de projet (*.proj)|*.proj|Tous les fichiers (*.*)|*.*"
            };

            // Afficher la boîte de dialogue et vérifier si l'utilisateur a sélectionné un fichier
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                // Logique pour charger le projet depuis 'filePath'
                // MessageBox.Show($"Projet ouvert : {filePath}");

                // Ouvrir la fenêtre principale après sélection (ou directement)
                var mainWindow = new MainWindow(filePath);

                // Synchroniser les dimensions et l'état de la fenêtre
                mainWindow.Width = this.Width;
                mainWindow.Height = this.Height;
                mainWindow.WindowState = this.WindowState;

                // Synchroniser la position de la fenêtre
                mainWindow.Left = this.Left;
                mainWindow.Top = this.Top;

                mainWindow.Show();
                this.Close(); // Fermer la fenêtre d'accueil
            }
        }
    }
}
