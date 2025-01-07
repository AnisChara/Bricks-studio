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
using System.Windows.Documents;
using System.Windows.Input;

namespace Bricks_Interfaces.ViewModels
{
    public class NodeViewModel : BaseNotifyPropertyChanged
    {
        public ICommand DeleteNodeCommand { get; set; }
        public ICommand AddNodeCommand { get; set; }

        private string node_name;
        public string Node_name
        {
            get => node_name;
            set
            {
                if (node_name != value)
                {
                    node_name = value;
                    OnPropertyChanged(nameof(Node_name));
                }
            }
        }
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

            DeleteNodeCommand = new RelayCommand(DeleteNode);
            AddNodeCommand = new RelayCommand(AddNode);

            InitializeFileWatcher();
            LoadDataAsync();

        }

        private async Task LoadDataAsync()
        {
            using (FileStream fileStream = new FileStream("../../../Nodes.json", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string json = await reader.ReadToEndAsync();
                    Nodes = new ObservableCollection<Node>(JsonSerializer.Deserialize<List<Node>>(json));
                }
            }
        }


        private void InitializeFileWatcher()
        {

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
                LoadDataAsync();
            };
            _fileWatcher.Renamed += (sender, e) =>
                LoadDataAsync();
            _fileWatcher.Created += (sender, e) =>
                LoadDataAsync();

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance
        }

        private void DeleteNode(object parameter)
        {
            string node_name = parameter as string;
            int index = Nodes.IndexOf(Nodes.FirstOrDefault(i => i.Name == node_name));
            if (index != -1)
            {
                Nodes.RemoveAt(index);
                string json = JsonSerializer.Serialize(Nodes, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText("../../../Nodes.json", json);
            }
        }

        private void AddNode(object parameter)
        {
            if (Nodes.Count() > 0 && Nodes[Nodes.Count - 1].Mecanique == null && Nodes[Nodes.Count - 1].Declencheur == null) 
            {
                MessageBox.Show("Votre dernier noeud n'est pas fini");
                return;
            }
            if(string.IsNullOrEmpty(node_name))
            {
                MessageBox.Show("Veuillez nommer votre noeud");
                return;
            }

            int index = Nodes.IndexOf(Nodes.FirstOrDefault(i => i.Name == node_name));

            if (index != -1)
            {
                MessageBox.Show("Un noeud avec ce nom existe deja.");
                return;
            }

            Nodes.Add(new Node(Node_name, null, null));
            string json = JsonSerializer.Serialize(Nodes, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Nodes.json", json);
            Node_name = string.Empty;

        }
    }
}
