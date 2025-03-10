﻿using Bricks_Interfaces.Models;
using Bricks_Interfaces.Views;
using Bricks_Interfaces.Views.AllOnglets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Bricks_Interfaces.ViewModels
{
    internal class AssetsViewModel : BaseNotifyPropertyChanged
    {
        public ICommand AddStructure {  get; set; }
        public ICommand AddObstacle {  get; set; }
        public ICommand AddEnnemy { get; set; }
        public ICommand AddZone { get; set; }
        public ICommand AddLevelCommand { get; set; }
        public ICommand DeleteLevelCommand { get; set; }

        private string levelname {  get; set; }
        public string LevelName
        {
            get => levelname;
            set
            {
                levelname = value;
                OnPropertyChanged(nameof(LevelName));
            }
        }

        public AssetsViewModel() 
        {
            AddStructure = new RelayCommand(Add_structure);
            AddObstacle = new RelayCommand(Add_obstacle);
            AddEnnemy = new RelayCommand(Add_Ennemy);
            AddZone = new RelayCommand(Add_Zone);
            AddLevelCommand = new RelayCommand(AddLevel);
            DeleteLevelCommand = new RelayCommand(DeleteLevel);

        }
        
        private void Add_structure(object parameter) 
        {

            ObservableCollection<Entity> Entités = Entity.GetEntities(Level.CurrentLevel);


            var newEntity = new Entity(
                type: "structure",
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
                image : MainWindowViewModel.AssetsPath+"/téléchargement.jpg",
                Track : false
            );

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);


            Entity.SaveEntities(Entités, Level.CurrentLevel);

        }
        private void Add_obstacle(object parameter) 
        {
            ObservableCollection<Entity> Entités = Entity.GetEntities(Level.CurrentLevel);


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
                damage:100,
                image : MainWindowViewModel.AssetsPath + "/images.png",
                Track: false

            );

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);


            Entity.SaveEntities(Entités, Level.CurrentLevel);


        }

        private void Add_Ennemy(object parameter)
        {
            ObservableCollection<Entity> Entités = Entity.GetEntities(Level.CurrentLevel);

            var newEntity = new Entity(
                type: "offensive_entity",
                id: Guid.NewGuid().ToString(),
                x: 100,
                y: 200,
                width: 100,
                height: 100,
                speed: 5,
                is_collidable: true,
                shape: "square",
                weight: 10,
                render: true,
                has_weapon: false,
                max_health:100,
                damage:100,
                image: MainWindowViewModel.AssetsPath + "/monster.png",
                Track: false

            );

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);
            Entity.SaveEntities(Entités, Level.CurrentLevel);

        }

        private void Add_Zone(object parameter)
        {
            ObservableCollection<Entity> Entités = Entity.GetEntities(Level.CurrentLevel);

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
                weight: 0,
                render: true,
                has_weapon: false,
                image: MainWindowViewModel.AssetsPath + "/flag.png",
                Track: false

            );

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);


            Entity.SaveEntities(Entités, Level.CurrentLevel);

        }

        private void AddLevel(object parameter)
        {
            if (LevelName == null)
            {
                MessageBox.Show("Veuillez entrez un nom pour votre niveau");
                return;
            }
            if (Entity.GetLevel(LevelName) != null)
            {
                MessageBox.Show("Ce niveau existe deja");
                return;
            }
            var levels = Entity.GetAllLevels();
            Entity Player = new Entity(

                type: "Player",
                x: 100,
                y: 100,
                width: 100,
                height: 100,
                id: "Player",
                speed: 10,
                is_collidable: true,
                shape: "square",
                render: true,
                has_weapon: true,
                max_health: 100,
                weight: 10,
                damage: null,
                Track: false,
                image: MainWindowViewModel.AssetsPath + "/mario.png"

            );

            ObservableCollection<Entity> Entitylevel = new ObservableCollection<Entity>();
            Entitylevel.Add(Player);
            var level = new Level(LevelName, Entitylevel, MainWindowViewModel.AssetsPath+"/Fond.jpg",RenduStatiqueViewModel.Width,RenduStatiqueViewModel.Height);

            levels.Add(level);
            Entity.SaveAllLevels(levels);
            Level.CurrentLevel = LevelName;
            LevelName = null;
        }

        private void DeleteLevel(object parameter)
        {
            var levels = Entity.GetAllLevels();
            if (levels.Count == 1) return;
            if (Level.CurrentLevel == Level.FirstLevel) return;
            int index = levels.IndexOf(levels.FirstOrDefault(l => l.Name == Level.CurrentLevel));
            levels.RemoveAt(index);
            Level.CurrentLevel = Level.FirstLevel;
            Entity.SaveAllLevels(levels);

        }
    }
}
