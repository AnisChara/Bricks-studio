using Bricks_Interfaces.Models;
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
using System.Windows.Input;

namespace Bricks_Interfaces.ViewModels
{
    public class RenduStatiqueViewModel : BaseNotifyPropertyChanged
    {
        public double Width;
        public double Height;
        public ICommand UpdateSizeCommand { get; }

        private FileSystemWatcher _fileWatcher;

        private ObservableCollection<Entity> entities { get; set; }
        public ObservableCollection<Entity> Entities
        {
            get => entities;
            set
            {
                entities = value;
                OnPropertyChanged(nameof(Entities));
            }
        }


        public RenduStatiqueViewModel(double Height,double Width)
        {
            this.Height = Height;
            this.Width = Width;
            MessageBox.Show(Height + " " + Width);

            InitializeFileWatcher();
            LoadDataAsync();
        }

        private void InitializeFileWatcher()
        {

            _fileWatcher = new FileSystemWatcher
            {
                Path = "../../../",
                Filter = "Entity.json",
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

        private async Task LoadDataAsync()
        {
            using (FileStream fileStream = new FileStream("../../../Entity.json", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string json = await reader.ReadToEndAsync();
                    Entities = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

                    //resize
                }
            }
        }

    }
}
