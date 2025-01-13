using Bricks_Interfaces.Models;
using Bricks_Interfaces.Views.AllOnglets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bricks_Interfaces.ViewModels
{
    internal class AssetsViewModel : BaseNotifyPropertyChanged
    {
        public ICommand AddStructure {  get; set; }
        public ICommand AddObstacle {  get; set; }
        public ICommand AddEnnemy { get; set; }
        public ICommand AddZone { get; set; }

        public AssetsViewModel() 
        {
            AddStructure = new RelayCommand(Add_structure);
            AddObstacle = new RelayCommand(Add_obstacle);
            AddEnnemy = new RelayCommand(Add_Ennemy);
            AddZone = new RelayCommand(Add_Zone);
        }
        
        private void Add_structure(object parameter) 
        {
            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity> Entités = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            var newEntity = new Entity(
                type: "structure",
                id: Guid.NewGuid().ToString(),
                x: 100,
                y: 200,
                width: 50,
                height: 50,
                speed: 5,
                is_collidable: true,
                shape: "square",
                weight: 0,
                render: true,
                has_weapon: false,
                color : "green"
            );
            newEntity.x *= 0.625;
            newEntity.y *= 0.625;
            newEntity.width *= 0.625;
            newEntity.height *= 0.625;

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);


            json = JsonSerializer.Serialize(Entités, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

        }
        private void Add_obstacle(object parameter) 
        {
            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity> Entités = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            var newEntity = new Entity(
                type: "offensive_structure",
                id: Guid.NewGuid().ToString(),
                x: 100,
                y: 200,
                width: 100,
                height: 100,
                speed: 5,
                is_collidable: true,
                shape: "square",
                weight: 0,
                render: true,
                has_weapon: false,
                color : "orange"

            );
            newEntity.x *= 0.625;
            newEntity.y *= 0.625;
            newEntity.width *= 0.625;
            newEntity.height *= 0.625;

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);


            json = JsonSerializer.Serialize(Entités, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

        }

        private void Add_Ennemy(object parameter)
        {
            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity> Entités = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            var newEntity = new Entity(
                type: "offensive_entity",
                id: Guid.NewGuid().ToString(),
                x: 100,
                y: 200,
                width: 50,
                height: 50,
                speed: 5,
                is_collidable: true,
                shape: "square",
                weight: 10,
                render: true,
                has_weapon: false,
                color: "red"

            );
            newEntity.x *= 0.625;
            newEntity.y *= 0.625;
            newEntity.width *= 0.625;
            newEntity.height *= 0.625;

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);


            json = JsonSerializer.Serialize(Entités, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

        }

        private void Add_Zone(object parameter)
        {
            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity> Entités = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            var newEntity = new Entity(
                type: "zone",
                id: Guid.NewGuid().ToString(),
                x: 100,
                y: 200,
                width: 50,
                height: 50,
                speed: 5,
                is_collidable: false,
                shape: "square",
                weight: 10,
                render: true,
                has_weapon: false,
                color: "yellow"

            );
            newEntity.x *= 0.625;
            newEntity.y *= 0.625;
            newEntity.width *= 0.625;
            newEntity.height *= 0.625;

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);


            json = JsonSerializer.Serialize(Entités, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

        }
    }
}
