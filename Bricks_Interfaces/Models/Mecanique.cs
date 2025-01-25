using Bricks_Interfaces.ViewModels;
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
    public class Mecanique : BaseNotifyPropertyChanged
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
            get=> _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private List<Action> _actions;
        public List<Action> Actions
        {
            get => _actions;
            set
            {
                if (_actions != value)
                {
                    _actions = value;
                    OnPropertyChanged(nameof(Actions));
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

        public Mecanique(List<Action> Actions , string name) { 
            
            this.Actions = Actions;
            this.Name = name;
            this.id = Guid.NewGuid().ToString();
            this.x = x;
            this.y = y;
            Image = "C:\\Users\\user\\Documents\\COURS\\C#\\Projet\\bricks-studio\\assets\\lego_rouge.png";
            width = 70;
            height = 35;
            this.margin = new Thickness(x, y, 0, 0);
        }

        public static ObservableCollection<Mecanique> GetMecaniques()
        {
            ObservableCollection<Mecanique> Nodes = new ObservableCollection<Mecanique>();
            string json;
            bool succes = false;

            while (!succes)
            {
                try
                {
                    json = System.IO.File.ReadAllText("../../../Mecaniques.json");
                    Nodes = JsonSerializer.Deserialize<ObservableCollection<Mecanique>>(json);
                    succes = true;

                }
                catch (Exception e) { }
            }

            return Nodes;
        }

        public static void InitializeFileWatcher(ObservableCollection<Mecanique> Nodes)
        {
            FileSystemWatcher _fileWatcher;

            _fileWatcher = new FileSystemWatcher
            {
                Path = "../../../",
                Filter = "Mecaniques.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            _fileWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            _fileWatcher.Changed += (sender, e) =>
            {
                // Lorsque le fichier JSON est modifié, rechargez les données
                Nodes = Mecanique.GetMecaniques();
            };
            _fileWatcher.Renamed += (sender, e) =>
                Nodes = Mecanique.GetMecaniques();
            _fileWatcher.Created += (sender, e) =>
                Nodes = Mecanique.GetMecaniques();

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance

        }
    }

}
