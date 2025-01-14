﻿using Bricks_Interfaces.Models;
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
        public static DoubleClick entityMenu = null;

        public static int CurrentLevel = 0;
        private int _currentLevel = 0;
        public int currentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                CurrentLevel = value;
                Entities = Entity.GetEntities(CurrentLevel);
                OnPropertyChanged(nameof(currentLevel));
            }
        }

        private int _NbLevel;
        public int NbLevel
        {
            get => _NbLevel;
            set
            {
                _NbLevel = value;
                OnPropertyChanged(nameof(_NbLevel));
            }
        }

        private ObservableCollection<int> listLevel {  get; set; }
        public ObservableCollection<int> ListLevel
        {

            get => listLevel;
            set
            {
                listLevel = value;
                OnPropertyChanged(nameof(ListLevel));
            }
        }
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
            NbLevel = Entity.GetAllEntities().Count;
            ListLevel = new ObservableCollection<int>();
            for (int i = 0; i < NbLevel; i++)
            {
                ListLevel.Add(i);
            }

            InitializeFileWatcher();
            Entities = Entity.GetEntities(CurrentLevel);
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
                Entities = Entity.GetEntities(CurrentLevel);
                NbLevel = Entity.GetAllEntities().Count;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ListLevel.Clear();
                    for (int i = 0; i < NbLevel; i++)
                    {
                        ListLevel.Add(i);
                    }
                });
                
            };
            _fileWatcher.Renamed += (sender, e) =>
                Entities = Entity.GetEntities(CurrentLevel);
                NbLevel = Entity.GetAllEntities().Count;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ListLevel.Clear();
                    for (int i = 0; i < NbLevel; i++)
                    {
                        ListLevel.Add(i);
                    }
                });
            _fileWatcher.Created += (sender, e) =>
                Entities = Entity.GetEntities(CurrentLevel);
                NbLevel = Entity.GetAllEntities().Count;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ListLevel.Clear();
                    for (int i = 0; i < NbLevel; i++)
                    {
                        ListLevel.Add(i);
                    }
                });

            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance
        }


        public void StartDrag(object parameter, Button button)
        {
            selectedEntity = parameter as Entity;
            selectedButton = button;
            dragging = true;
        }

        public void ActualiseDrag(Point e, double width, double height)
        {
            if (!dragging) return;

            bool can_move_left = true;
            bool can_move_right = true;
            bool can_move_top = true;
            bool can_move_bottom = true;

            var (direction,entity_collided) = selectedEntity.CheckAllCollision(Entities);

            if (direction == "right" && e.X >= selectedEntity.x)can_move_right = false;
            if (direction == "left" && e.X < entity_collided.x +entity_collided.width )can_move_left = false;
            if (direction == "bottom" && e.Y > selectedEntity.y)can_move_bottom = false;
            if (direction == "top" && e.Y < entity_collided.y+entity_collided.height)can_move_top = false;
            if (e.X < 0) can_move_left = false;
            if (e.X >= width - selectedEntity.width) can_move_right = false;
            if (e.Y < 0) can_move_top = false;
            if (e.Y >= height - selectedEntity.height) can_move_bottom = false;

            if (e.X > selectedEntity.x && can_move_right) selectedEntity.x = e.X;
            if (e.X < selectedEntity.x && can_move_left) selectedEntity.x = e.X;
            if (e.Y < selectedEntity.y && can_move_top) selectedEntity.y = e.Y;
            if (e.Y > selectedEntity.y && can_move_bottom) selectedEntity.y = e.Y;


            selectedEntity.margin = new Thickness(selectedEntity.x, selectedEntity.y, 0, 0);
        }

        public void StopDrag()
        {
            dragging = false;

            Entity.SaveEntities(Entities, RenduStatiqueViewModel.CurrentLevel);
        }

        public void OpenEntityMenu(Entity entity)
        {
            if (entityMenu != null)
            {
                MessageBox.Show("Un menu d'entité est deja ouvert veuillez d'abord sauvegarder ou annuler vos changements.");
                return;
            }

            entityMenu = new DoubleClick(entity);

            // Synchroniser les dimensions et l'état de la fenêtre
            entityMenu.Width = 200;
            entityMenu.Height = 300;

            // Synchroniser la position de la fenêtre
            entityMenu.Left = 700;
            entityMenu.Top = 200;

            entityMenu.Show();
        }

    }
}
