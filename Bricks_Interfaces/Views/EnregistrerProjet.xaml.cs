using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Bricks_Interfaces.Models;
using Bricks_Interfaces.ViewModels;
using Bricks_Interfaces.Views;
using Microsoft.Win32;


namespace Bricks_Interfaces
{
    /// <summary>
    /// Logique d'interaction pour EnregistrerProjet.xaml
    /// </summary>
    public partial class EnregistrerProjet : Window
    {
        public EnregistrerProjet()
        {
            InitializeComponent();
        }

        public string? folderPath;

        private void OnRetourClick(object sender, RoutedEventArgs e)
        {
            var index = new Index();
            index.Show();

            this.Close();
        }

        private void OnFileClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
            {
                Description = "Sélectionner un dossier",
                UseDescriptionForTitle = true
            };

            if (dialog.ShowDialog() == true)
            {
                folderPath = dialog.SelectedPath;
                folderPathTextBox.Text = folderPath;
            }
        }

        private void OnEnregistrerClick(object sender, RoutedEventArgs e)
        {
            if (folderPath == null)
            {
                MessageBox.Show("Veuillez choisir un emplacement valide.");
                return;
            }
            if (!IsValidFolderName(ProjectName.Text))
            {
                MessageBox.Show("Nom invalide");
                return;
            }

            string ProjectPath = folderPath + '/' + ProjectName.Text;

            CreateProject(ProjectPath);

            var mainWindow = new MainWindow(ProjectPath);

            // Synchroniser les dimensions et l'état et la position de la fenêtre
            mainWindow.WindowState = WindowState.Maximized;
            mainWindow.Left = this.Left;
            mainWindow.Top = this.Top;

            mainWindow.Show();
            this.Close();
        }

        private void CreateProject(string ProjectPath)
        {


            if (!Directory.Exists(ProjectPath))
            {
                Directory.CreateDirectory(ProjectPath);
                Directory.CreateDirectory(ProjectPath+"/Assets");
                Directory.CreateDirectory(ProjectPath+"/python");
                CopyDirectory("../../../Assets", ProjectPath+"/Assets");
                CopyDirectory("../../../../code/python", ProjectPath+ "/python");
                File.Copy("../../../Actions.json", ProjectPath + "/Actions.json", true);
                File.Copy("../../../Events.json", ProjectPath + "/Events.json", true);
                File.Copy("../../../Nodes.json", ProjectPath + "/Nodes.json", true);

                Entity Player = new Entity(

                    type: "Player",
                    x: 100,
                    y: 100,
                    width: 100,
                    height: 100,
                    id: "Player",
                    speed: 10,
                    is_collidable: true,
                    shape: "square",
                    render: true,
                    has_weapon: true,
                    max_health: 100,
                    weight: 10,
                    damage: null,
                    image: ProjectPath + "/Assets/mario.png",
                    Track: false

                );
                ObservableCollection<Entity> Entitylevel = [Player];
                var level = new Level("Niveau 1", Entitylevel, ProjectPath + "/Assets/Fond.jpg", 0, 0);
                ObservableCollection<Level> Levels = [level];
                string json = JsonSerializer.Serialize(Levels, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(ProjectPath + "/Entity.json", json);

            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            // Crée le dossier de destination s'il n'existe pas
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            // Copier tous les fichiers
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true); // true pour écraser les fichiers existants
            }

            // Copier tous les sous-dossiers (récursivement)
            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string newDestDir = Path.Combine(destinationDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, newDestDir);
            }
        }

        private bool IsValidFolderName(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return false;

            // Vérifie si le nom contient des caractères interdits sous Windows
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in folderName)
            {
                if (Array.IndexOf(invalidChars, c) >= 0)
                    return false;
            }

            // Vérifie si le nom est un nom réservé sous Windows
            string[] reservedNames = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                                   "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
            if (Array.Exists(reservedNames, name => string.Equals(folderName, name, StringComparison.OrdinalIgnoreCase)))
                return false;

            // Vérifie si le nom termine par un espace ou un point (problème sous Windows)
            if (folderName.EndsWith(" ") || folderName.EndsWith("."))
                return false;

            return true;
        }
    }
}
