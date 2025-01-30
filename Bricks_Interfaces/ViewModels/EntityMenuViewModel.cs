using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Bricks_Interfaces.Models;

namespace Bricks_Interfaces.ViewModels
{
    class EntityMenuViewModel : BaseNotifyPropertyChanged
    {
        public Visibility DamageVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SpeedVisibility { get; set; } = Visibility.Collapsed;
        public Visibility TrackVisibility { get; set; } = Visibility.Collapsed;
        public Visibility HealthVisibility { get; set; } = Visibility.Collapsed;

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
        public EntityMenuViewModel(Entity entity) 
        {
            Entity = entity;
            if (Entity.type == "Player")
            {
                DamageVisibility = Visibility.Collapsed;
                HealthVisibility = Visibility.Visible;
            }
            else if (Entity.type == "structure")
            {
                DamageVisibility = Visibility.Collapsed;
            }
            else if (Entity.type == "offensive_structure")
            {
                DamageVisibility = Visibility.Visible;
            }
            else if (Entity.type == "offensive_entity")
            {
                DamageVisibility = Visibility.Visible;
                SpeedVisibility = Visibility.Visible;
                TrackVisibility = Visibility.Visible;
                HealthVisibility = Visibility.Visible;
            }
            else if (Entity.type == "zone")
            {
                DamageVisibility = Visibility.Collapsed;
            }
        }

        public void SaveEntity()
        {
            ObservableCollection<Entity> Entities = Entity.GetEntities(Level.CurrentLevel);
            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());
            Entities[index] = Entity;
            Entity.SaveEntities(Entities, Level.CurrentLevel);
            RenduStatiqueViewModel.entityMenu = null;

        }

        public void DeleteEntity() {

            if (Entity == null) return;
            if (Entity.id == "Player") return;

            ObservableCollection<Entity> Entities = Entity.GetEntities(Level.CurrentLevel);
            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());
            Entities.RemoveAt(index);
            Entity.SaveEntities(Entities, Level.CurrentLevel);
            RenduStatiqueViewModel.entityMenu = null;
        }
        public void CancelChange() {

            ObservableCollection<Entity> Entities = Entity.GetEntities(Level.CurrentLevel);
            Entity.SaveEntities(Entities, Level.CurrentLevel);
            RenduStatiqueViewModel.entityMenu = null;

        }
    }
}
