using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;
using Bricks_Interfaces.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Bricks_Interfaces.Models
{
    public class Declencheur: BaseNotifyPropertyChanged
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

        private List<Event> _events;
        public List<Event> Events
        { 
            get=> _events;
            set
            {
                if (_events != value)
                {
                    _events = value;
                    OnPropertyChanged(nameof(Events));
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

        public Declencheur(List<Event> events, string Name) {
               
            this.Events = events;
            this.Name = Name;
            this.id = Guid.NewGuid().ToString();
            Image = "C:\\Users\\user\\Documents\\COURS\\C#\\Projet\\bricks-studio\\assets\\lego_bleu.png";
            this.x = x;
            this.y = y;
            width = 70;
            height = 35;
            this.margin = new Thickness(x, y, 0, 0);

        }
        public static ObservableCollection<Declencheur> GetDeclencheurs()
        {
            ObservableCollection<Declencheur> Nodes = new ObservableCollection<Declencheur>();
            string json;
            bool succes = false;

            while (!succes)
            {
                try
                {
                    json = System.IO.File.ReadAllText("../../../Declencheurs.json");
                    Nodes = JsonSerializer.Deserialize<ObservableCollection<Declencheur>>(json);
                    succes = true;

                }
                catch (Exception e) { }
            }

            return Nodes;
        }

        public static void InitializeFileWatcher(ObservableCollection<Declencheur> Nodes)
        {
            FileSystemWatcher _fileWatcher;

            _fileWatcher = new FileSystemWatcher
            {
                Path = "../../../",
                Filter = "Declencheurs.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            _fileWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            _fileWatcher.Changed += (sender, e) =>
            {
                // Lorsque le fichier JSON est modifié, rechargez les données
                Nodes = Declencheur.GetDeclencheurs();
            };
            _fileWatcher.Renamed += (sender, e) =>
                Nodes = Declencheur.GetDeclencheurs();
            _fileWatcher.Created += (sender, e) =>
                Nodes = Declencheur.GetDeclencheurs();

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance

        }
    }

}
