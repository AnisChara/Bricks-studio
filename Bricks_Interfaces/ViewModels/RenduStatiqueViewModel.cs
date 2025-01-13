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
using System.Windows.Controls;
using System.Windows.Input;

namespace Bricks_Interfaces.ViewModels
{
    public class RenduStatiqueViewModel : BaseNotifyPropertyChanged
    {

        private string background;
        public string Background
        {

            get => background;
            set
            {
                background = value;
                OnPropertyChanged(nameof(Background));
            }
        }


        public bool dragging = false;

        private FileSystemWatcher _fileWatcher;

        private Entity selectedEntity;
        public Entity SelectedEntity
        {

            get => selectedEntity;
            set
            {
                selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
            }
        }
        private Button selectedButton;
        public Button SelectedButton
        {

            get => selectedButton;
            set
            {
                selectedButton = value;
                OnPropertyChanged(nameof(SelectedButton));
            }
        }

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


        public RenduStatiqueViewModel()
        {
            Background = "C:\\Users\\user\\Pictures\\Fond.jpg";

            InitializeFileWatcher();
            LoadData();
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
                LoadData();
            };
            _fileWatcher.Renamed += (sender, e) =>
                LoadData();
            _fileWatcher.Created += (sender, e) =>
                LoadData();

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance
        }

        private void LoadData()
        {
            try
            {
                using (FileStream fileStream = new FileStream("../../../Entity.json", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string json = reader.ReadToEnd(); // Lecture synchrone
                        Entities = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));
                        
                    }
                }
            }
            catch { }
        }


        public void StartDrag(object parameter, Button button)
        {
            selectedEntity = parameter as Entity;
            selectedButton = button;
            dragging = true;
        }

        public void ActualiseDrag(Point e)
        {
            bool can_move_x = true;
            bool can_move_y = true;

            if (!dragging) return;

            var (direction,entity_collided) = selectedEntity.CheckAllCollision(Entities);

            if (direction == "right" && e.X > selectedEntity.x)can_move_x = false;
            if (direction == "left" && e.X < entity_collided.x + entity_collided.width)can_move_x = false;
            if (direction == "bottom" && e.Y > selectedEntity.y)can_move_y = false;
            if (direction == "top" && e.Y < entity_collided.y+entity_collided.height)can_move_y = false;

            if (!(e.X < 0 || e.X >= 750-selectedEntity.width) && can_move_x) selectedEntity.x = e.X;
            if (!(e.Y < 0 || e.Y >= 365-selectedEntity.height) && can_move_y) selectedEntity.y = e.Y;// A rendre modulable

            selectedEntity.margin = new Thickness(selectedEntity.x, selectedEntity.y, 0, 0);
        }

        public void StopDrag()
        {
            dragging = false;

            string json = JsonSerializer.Serialize(Entities, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

        }

        public void DeleteEntity(Entity entity)
        {
            if (entity == null) return;
            if (entity.id == "Player") return;
            Entities.RemoveAt(Entities.IndexOf(entity));
            string json = JsonSerializer.Serialize(Entities, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);
        }
    }
}
