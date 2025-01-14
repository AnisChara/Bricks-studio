using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bricks_Interfaces.Models;

namespace Bricks_Interfaces.ViewModels
{
    class EntityMenuViewModel : BaseNotifyPropertyChanged
    {

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
        }

        public void SaveEntity()
        {
            ObservableCollection<Entity> Entities = Entity.GetEntities(RenduStatiqueViewModel.CurrentLevel);


            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());

            Entities[index] = Entity;

            Entity.SaveEntities(Entities, RenduStatiqueViewModel.CurrentLevel);

            RenduStatiqueViewModel.entityMenu = null;

        }

        public void DeleteEntity() {

            if (Entity == null) return;
            if (Entity.id == "Player") return;

            ObservableCollection<Entity> Entities = Entity.GetEntities(RenduStatiqueViewModel.CurrentLevel);


            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());

            Entities[index] = Entity;

            Entities.RemoveAt(index);

            Entity.SaveEntities(Entities, RenduStatiqueViewModel.CurrentLevel);


            RenduStatiqueViewModel.entityMenu = null;
        }
        public void CancelChange() {

            ObservableCollection<Entity> Entities = Entity.GetEntities(RenduStatiqueViewModel.CurrentLevel);


            Entity.SaveEntities(Entities, RenduStatiqueViewModel.CurrentLevel);

            RenduStatiqueViewModel.entityMenu = null;

        }
    }
}
