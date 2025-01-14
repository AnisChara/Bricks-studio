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
            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity>  Entities = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());

            Entities[index] = Entity;

            json = JsonSerializer.Serialize(Entities, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);
            RenduStatiqueViewModel.entityMenu = null;

        }

        public void DeleteEntity() {

            if (Entity == null) return;
            if (Entity.id == "Player") return;

            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity> Entities = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());

            Entities[index] = Entity;

            Entities.RemoveAt(index);

            json = JsonSerializer.Serialize(Entities, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

            RenduStatiqueViewModel.entityMenu = null;
        }
        public void CancelChange() {

            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity> Entities = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            json = JsonSerializer.Serialize(Entities, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);
            RenduStatiqueViewModel.entityMenu = null;

        }
    }
}
