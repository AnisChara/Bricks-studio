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
    public class Node : BaseNotifyPropertyChanged
    {
        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

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

        private string _id;
        public string id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(id));
                }
            }
        }

        private Thickness _margin;
        public Thickness margin
        {
            get => _margin;
            set
            {
                if (_margin != value)
                {
                    _margin = value;
                    OnPropertyChanged(nameof(margin)); // Notification de changement
                }
            }
        }

        private double _x;
        public double x
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged(nameof(x));
                }
            }
        }

        private double _y;
        public double y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged(nameof(y));
                }
            }
        }

        private double _width;
        public double width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged(nameof(width));
                }
            }
        }

        private double _height;
        public double height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(nameof(height));
                }
            }
        }

        public Node(string name, Mecanique mecanique, Declencheur declencheur, double x = 100, double y= 100) { 
            
            Name = name;
            Mecanique = mecanique;
            Declencheur = declencheur;
            Image = "C:\\Users\\user\\Documents\\COURS\\C#\\Projet\\bricks-studio\\assets\\lego_jaune.png";
            this.id = Guid.NewGuid().ToString();
            this.x = x;
            this.y = y;
            width = 70;
            height = 35;
            this.margin = new Thickness(x, y, 0, 0);
        }

        public (bool isColliding, string direction) CheckCollision(Node entity2)
        {
            // Vérifie si les deux entités sont marquées comme "collidables"
            // Vérifie les collisions en utilisant les coordonnées et les dimensions
            bool isColliding = this.x < entity2.x + entity2.width &&
                               this.x + this.width > entity2.x &&
                               this.y < entity2.y + entity2.height &&
                               this.y + this.height > entity2.y;

            if (!isColliding)
            {
                return (false, "none");
            }

            // Détermine la direction de la collision
            string direction = "none";

            double overlapLeft = this.x + this.width - entity2.x;
            double overlapRight = entity2.x + entity2.width - this.x;
            double overlapTop = this.y + this.height - entity2.y;
            double overlapBottom = entity2.y + entity2.height - this.y;

            double minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight), Math.Min(overlapTop, overlapBottom));

            if (minOverlap == overlapLeft)
            {
                direction = "right";
            }
            else if (minOverlap == overlapRight)
            {
                direction = "left";
            }
            else if (minOverlap == overlapTop)
            {
                direction = "bottom";
            }
            else if (minOverlap == overlapBottom)
            {
                direction = "top";
            }

            return (true, direction);
        }


        public (string, Node) CheckAllCollision(ObservableCollection<Node> Entities)
        {
            foreach (Node entity in Entities)
            {
                if (entity == this) continue;
                var (result, direction) = this.CheckCollision(entity);
                if (result) return (direction, entity);
                else continue;
            }
            return ("none", null);
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


            _fileWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            _fileWatcher.Changed += (sender, e) =>
            {
                // Lorsque le fichier JSON est modifié, rechargez les données
                Nodes = Node.GetNodes();
            };
            _fileWatcher.Renamed += (sender, e) =>
                Nodes = Node.GetNodes();
            _fileWatcher.Created += (sender, e) =>
                Nodes = Node.GetNodes();

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance

        }
    }

}
