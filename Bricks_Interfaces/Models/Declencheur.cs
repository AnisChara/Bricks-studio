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
    public class Declencheur: Brick
    {


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

        public Declencheur(List<Event> events, string name, double x = 0, double y = 0, double width = 70, double height = 35) : base( name, x, y, width, height)
        {
            this.Events = events;
            Image = "pack://application:,,,/Assets/lego_bleu.png";
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



            _fileWatcher.Changed += (sender, e) =>
            {
                Nodes = Declencheur.GetDeclencheurs();
            };
            _fileWatcher.Renamed += (sender, e) =>
                Nodes = Declencheur.GetDeclencheurs();
            _fileWatcher.Created += (sender, e) =>
                Nodes = Declencheur.GetDeclencheurs();

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance

        }

        public static void SaveDeclencheurs(ObservableCollection<Declencheur> Levels)
        {
            bool succes = false;

            while (!succes)
            {
                try
                {
                    string json = JsonSerializer.Serialize(Levels, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText("../../../Declencheurs.json", json);
                    succes = true;

                }
                catch (Exception e) { }
            }
        }
    }

}
