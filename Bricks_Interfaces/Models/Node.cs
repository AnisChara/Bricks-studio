using Bricks_Interfaces.ViewModels;
using Bricks_Interfaces.Views.AllOnglets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Bricks_Interfaces.Models
{
    public class Node : Brick
    {
       

        private Mecanique _mecanique;
        public Mecanique Mecanique
        { 
            get=> _mecanique;
            set
            {
                if (value != _mecanique)
                {
                    _mecanique = value;
                    OnPropertyChanged(nameof(Mecanique));
                }
            }
        }
        private Declencheur _declencheur;
        public Declencheur Declencheur
        { 
            get=> _declencheur;
            set
            {
                if (_declencheur != value)
                {
                    _declencheur = value;
                    OnPropertyChanged(nameof(Declencheur));
                }
            }
        }

        

        public Node(Mecanique mecanique, Declencheur declencheur, string name, double x = 0, double y = 0, double width = 70, double height = 55) : base(   name,  x,  y,  width,  height) { 
            
            Image = "pack://application:,,,/Assets/lego_jaune.png";
            this.Mecanique = mecanique;
            this.Declencheur = declencheur;
        }






        public static ObservableCollection<Node> GetNodes()
        {
            ObservableCollection<Node> Nodes = new ObservableCollection<Node>();
            string json;
            bool succes = false;

            while (!succes)
            {
                try
                {
                    json = System.IO.File.ReadAllText("../../../Nodes.json");
                    Nodes = JsonSerializer.Deserialize<ObservableCollection<Node>>(json);
                    succes = true;

                }
                catch (Exception e) { }
            }

            return Nodes;
        }

        public static void InitializeFileWatcher(ObservableCollection<Node> Nodes)
        {
            FileSystemWatcher _fileWatcher;
            _fileWatcher = new FileSystemWatcher
            {
                Path = "../../../",
                Filter = "Nodes.json",
                NotifyFilter = NotifyFilters.LastWrite
            };



            _fileWatcher.Changed += (sender, e) =>
            {
                // Lorsque le fichier JSON est modifié, rechargez les données
                Nodes = Node.GetNodes();
            };

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance

        }

        public static void SaveNodes(ObservableCollection<Node> Levels)
        {
            bool succes = false;

            while (!succes)
            {
                try
                {
                    string json = JsonSerializer.Serialize(Levels, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText("../../../Nodes.json", json);
                    succes = true;

                }
                catch (Exception e) { }
            }
        }
    }

}
