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
            if (ProjectName.Text == null || ProjectName.Text == "")
            {
                MessageBox.Show("Veuillez choisir un nom valide.");
                return;
            }

            string ProjectPath = folderPath + '/' + ProjectName.Text;

            CreateProject(ProjectPath);

            var mainWindow = new MainWindow(ProjectPath);

            // Synchroniser les dimensions et l'état et la position de la fenêtre
            mainWindow.Width = 1280;
            mainWindow.Height = 720;
            mainWindow.WindowState = this.WindowState;
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
                File.Copy("../../../Mecaniques.json", ProjectPath + "/Mecaniques.json", true);
                File.Copy("../../../Declencheurs.json", ProjectPath + "/Declencheurs.json", true);
                File.Copy("../../../Nodes.json", ProjectPath + "/Nodes.json", true);

                Entity Player = new Entity(

                    type: "Player",
                    x: 1.533333333333303,
                    y: 0.37333333333332774,
                    width: 62.5,
                    height: 62.5,
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
                var level = new Level("Niveau 1", Entitylevel, ProjectPath + "/Assets/Fond.jpg");
                ObservableCollection<Level> Levels = [level];
                string json = JsonSerializer.Serialize(Levels, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(ProjectPath + "/Entity.json", json);

            }
        }

        static void CopyDirectory(string sourceDir, string destinationDir)
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
    }
}
