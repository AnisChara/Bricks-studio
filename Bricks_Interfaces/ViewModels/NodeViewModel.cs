using Bricks_Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Bricks_Interfaces.ViewModels
{
    public class NodeViewModel : BaseNotifyPropertyChanded
    {
        public ObservableCollection<Node> PreMadeNode { get; set; }
        private ObservableCollection<Node> _nodes { get; set; }
        public ObservableCollection<Node> Nodes { get => _nodes;
            set
            {
                _nodes = value;
                OnPropertyChanged(nameof(Nodes));
            }
        }


        private FileSystemWatcher _fileWatcher;
        public NodeViewModel() {

            InitializeFileWatcher();
            LoadData();

        }

        private void LoadData()
        {
            string json = File.ReadAllText("A:/Code/bricks-studio/Bricks_Interfaces/Nodes.json");
            Nodes = new ObservableCollection<Node>(JsonSerializer.Deserialize<List<Node>>(json));
        }

        private void InitializeFileWatcher()
        {

            _fileWatcher = new FileSystemWatcher
            {
                Path = "A:\\Code\\bricks-studio\\Bricks_Interfaces\\",
                Filter = "Nodes.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            _fileWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            _fileWatcher.Changed += (sender, e) =>
            {
                // Lorsque le fichier JSON est modifié, rechargez les données
                LoadData();
            };
            _fileWatcher.Renamed += (sender, e) =>
                LoadData();
            _fileWatcher.Created += (sender, e) =>
                LoadData();

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance
        }
    }
}
