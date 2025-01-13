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
                    weight: 10,
                    render: true,
                    has_weapon: false
    );

            Entités.Add(newEntity);


            json = JsonSerializer.Serialize(Entités, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

            MessageBox.Show("Nouvelle entité ajoutée avec succès !");
        }
        private void Add_obstacle(object parameter) 
        {
            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity> Entités = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            var newEntity = new Entity(
                type: "obstacle",
                id: Guid.NewGuid().ToString(),
                x: 100,
                y: 200,
                width: 50,
                height: 50,
                speed: 5,
                is_collidable: true,
                shape: "square",
                weight: 10,
                render: false,
                has_weapon: false,
                max_health: 100
);

            Entités.Add(newEntity);


            json = JsonSerializer.Serialize(Entités, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

            MessageBox.Show("Nouvelle entité ajoutée avec succès !");
        }

        private void Add_Ennemy(object parameter)
        {
            string json = System.IO.File.ReadAllText("../../../Entity.json");
            ObservableCollection<Entity> Entités = new ObservableCollection<Entity>(JsonSerializer.Deserialize<List<Entity>>(json));

            var newEntity = new Entity(
                type: "ennemy",
                id: Guid.NewGuid().ToString(),
                x: 100,
                y: 200,
                width: 50,
                height: 50,
                speed: 5,
                is_collidable: true,
                shape: "square",
                weight: 10,
                render: false,
                has_weapon: false,
                max_health: 100
);

            Entités.Add(newEntity);


            json = JsonSerializer.Serialize(Entités, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

            MessageBox.Show("Nouvelle entité ajoutée avec succès !");
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
                is_collidable: true,
                shape: "square",
                weight: 10,
                render: false,
                has_weapon: false,
                max_health: 100
);

            Entités.Add(newEntity);


            json = JsonSerializer.Serialize(Entités, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText("../../../Entity.json", json);

            MessageBox.Show("Nouvelle entité ajoutée avec succès !");
        }
    }
}
