﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
            var enregistrerProjet = new EnregistrerProjet();
            enregistrerProjet.Show();
            enregistrerProjet.Width = 1280;
            enregistrerProjet.Height = 720;

            this.Close(); // Fermez la fenêtre actuelle si nécessaire
        }

        private void OnOpenProjectClick(object sender, RoutedEventArgs e)
        {
            // Utilisation de la boîte de dialogue moderne avec Ookii.Dialogs.Wpf
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
            {
                Description = "Ouvrir un projet",
                UseDescriptionForTitle = true
            };

            // Afficher la boîte de dialogue et vérifier si l'utilisateur a sélectionné un dossier
            if (dialog.ShowDialog() == true)
            {
                string folderPath = dialog.SelectedPath;

                if (!Directory.Exists(folderPath + "/Assets")|| !Directory.Exists(folderPath + "/python") || !File.Exists(folderPath + "/Mecaniques.json") || !File.Exists(folderPath + "/Declencheurs.json") || !File.Exists(folderPath + "/Nodes.json") || !File.Exists(folderPath + "/Entity.json"))
                {
                    MessageBox.Show("Projet non-conforme.");
                    return;
                }

                // Créer et configurer la fenêtre principale
                var mainWindow = new MainWindow(folderPath)
                {
                    Width = 1280,
                    Height = 720,
                    WindowState = this.WindowState,
                    Left = this.Left,
                    Top = this.Top
                };

                mainWindow.Show();
                this.Close(); // Fermer la fenêtre d'accueil
            }
        }
    }
}
