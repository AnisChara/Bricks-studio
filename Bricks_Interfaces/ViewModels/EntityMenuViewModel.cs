using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Bricks_Interfaces.Models;

namespace Bricks_Interfaces.ViewModels
{
    class EntityMenuViewModel : BaseNotifyPropertyChanged
    {
        private FileSystemWatcher _fileWatcher;
        public ICommand DeleteCommand => new RelayCommand(DeleteEntity);
        public ICommand SaveCommand => new RelayCommand(SaveEntity);

        private Projet Data;
        private Visibility _damageVisibility ;
        public Visibility DamageVisibility
        {
            get => _damageVisibility;
            set
            {
                if (_damageVisibility != value)
                {
                    _damageVisibility = value;
                    OnPropertyChanged(nameof(DamageVisibility));
                }
            }
        }

        private Visibility _speedVisibility ;
        public Visibility SpeedVisibility
        {
            get => _speedVisibility;
            set
            {
                if (_speedVisibility != value)
                {
                    _speedVisibility = value;
                    OnPropertyChanged(nameof(SpeedVisibility));
                }
            }
        }

        private Visibility _trackVisibility ;
        public Visibility TrackVisibility
        {
            get => _trackVisibility;
            set
            {
                if (_trackVisibility != value)
                {
                    _trackVisibility = value;
                    OnPropertyChanged(nameof(TrackVisibility));
                }
            }
        }

        private Visibility _healthVisibility;
        public Visibility HealthVisibility
        {
            get => _healthVisibility;
            set
            {
                if (_healthVisibility != value)
                {
                    _healthVisibility = value;
                    OnPropertyChanged(nameof(HealthVisibility));
                }
            }
        }

        private Visibility _collisionVisibility;
        public Visibility CollisionVisibility
        {
            get => _collisionVisibility;
            set
            {
                if (_collisionVisibility != value)
                {
                    _collisionVisibility = value;
                    OnPropertyChanged(nameof(CollisionVisibility));
                }
            }
        }

        private Visibility _deleteVisibility ;
        public Visibility DeleteVisibility
        {
            get => _deleteVisibility;
            set
            {
                if (_deleteVisibility != value)
                {
                    _deleteVisibility = value;
                    OnPropertyChanged(nameof(DeleteVisibility));
                }
            }
        }


        private Entity _entity; 
        public Entity Entity
        {
            get => _entity;
            set
            {
                if (_entity != value)
                {
                    _entity = value;
                    OnPropertyChanged(nameof(Entity));
                }
            }
        }
        public EntityMenuViewModel() 
        {
            InitializeFileWatcher();
            Data = Projet.GetData();
            Entity = Data.SelectedEntity;
            if (Entity.type == "Player")
            {
                HealthVisibility = Visibility.Visible;
                DeleteVisibility = Visibility.Collapsed;
                SpeedVisibility = Visibility.Collapsed;
                TrackVisibility = Visibility.Collapsed;
                CollisionVisibility = Visibility.Collapsed;
                DamageVisibility = Visibility.Collapsed;
            }
            else if (Entity.type == "structure")
            {
                HealthVisibility = Visibility.Collapsed;
                DeleteVisibility = Visibility.Visible;
                CollisionVisibility = Visibility.Visible;
                SpeedVisibility = Visibility.Collapsed;
                TrackVisibility = Visibility.Collapsed;
                DamageVisibility = Visibility.Collapsed;
            }
            else if (Entity.type == "offensive_structure")
            {
                HealthVisibility = Visibility.Collapsed;
                DeleteVisibility = Visibility.Visible;
                CollisionVisibility = Visibility.Collapsed;
                SpeedVisibility = Visibility.Collapsed;
                TrackVisibility = Visibility.Collapsed;
                DamageVisibility = Visibility.Visible;
            }
            else if (Entity.type == "offensive_entity")
            {
                DamageVisibility = Visibility.Visible;
                SpeedVisibility = Visibility.Visible;
                TrackVisibility = Visibility.Visible;
                HealthVisibility = Visibility.Visible;
                DeleteVisibility = Visibility.Visible;
                CollisionVisibility = Visibility.Collapsed;
            }
            else if (Entity.type == "zone")
            {
                HealthVisibility = Visibility.Collapsed;
                DeleteVisibility = Visibility.Visible;
                CollisionVisibility = Visibility.Collapsed;
                SpeedVisibility = Visibility.Collapsed;
                DamageVisibility = Visibility.Collapsed;
                TrackVisibility = Visibility.Collapsed;
            }
        }

        public void SaveEntity(object sender)
        {
            ObservableCollection<Entity> Entities = Entity.GetEntities(Level.CurrentLevel);
            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());
            Entities[index] = Entity;
            Entity.SaveEntities(Entities, Level.CurrentLevel);
            RenduStatiqueViewModel.entityMenu = null;

        }

        public void DeleteEntity(object sender) {

            if (Entity == null) return;
            if (Entity.id == "Player") return;

            ObservableCollection<Entity> Entities = Entity.GetEntities(Level.CurrentLevel);
            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());
            Entities.RemoveAt(index);
            Entity.SaveEntities(Entities, Level.CurrentLevel);
            Entity = null;
            RenduStatiqueViewModel.entityMenu = null;
        }
        public void CancelChange() {

            ObservableCollection<Entity> Entities = Entity.GetEntities(Level.CurrentLevel);
            Entity.SaveEntities(Entities, Level.CurrentLevel);
            RenduStatiqueViewModel.entityMenu = null;

        }

        private void InitializeFileWatcher()
        {

            _fileWatcher = new FileSystemWatcher
            {
                Path = MainWindowViewModel.ProjectPath,
                Filter = "Data.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            _fileWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            _fileWatcher.Changed += (sender, e) =>
            {
                Data = Projet.GetData();
                Entity = Data.SelectedEntity;
                if (Entity.type == "Player")
                {
                    HealthVisibility = Visibility.Visible;
                    DeleteVisibility = Visibility.Collapsed;
                    SpeedVisibility = Visibility.Collapsed;
                    TrackVisibility = Visibility.Collapsed;
                    CollisionVisibility = Visibility.Collapsed;
                    DamageVisibility = Visibility.Collapsed;
                }
                else if (Entity.type == "structure")
                {
                    HealthVisibility = Visibility.Collapsed;
                    DeleteVisibility = Visibility.Visible;
                    CollisionVisibility = Visibility.Visible;
                    SpeedVisibility = Visibility.Collapsed;
                    TrackVisibility = Visibility.Collapsed;
                    DamageVisibility = Visibility.Collapsed;
                }
                else if (Entity.type == "offensive_structure")
                {
                    HealthVisibility = Visibility.Collapsed;
                    DeleteVisibility = Visibility.Visible;
                    CollisionVisibility = Visibility.Collapsed;
                    SpeedVisibility = Visibility.Collapsed;
                    TrackVisibility = Visibility.Collapsed;
                    DamageVisibility = Visibility.Visible;
                }
                else if (Entity.type == "offensive_entity")
                {
                    DamageVisibility = Visibility.Visible;
                    SpeedVisibility = Visibility.Visible;
                    TrackVisibility = Visibility.Visible;
                    HealthVisibility = Visibility.Visible;
                    DeleteVisibility = Visibility.Visible;
                    CollisionVisibility = Visibility.Collapsed;
                }
                else if (Entity.type == "zone")
                {
                    HealthVisibility = Visibility.Collapsed;
                    DeleteVisibility = Visibility.Visible;
                    CollisionVisibility = Visibility.Collapsed;
                    SpeedVisibility = Visibility.Collapsed;
                    DamageVisibility = Visibility.Collapsed;
                    TrackVisibility = Visibility.Collapsed;
                }

            };

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance
        }
    }
}
