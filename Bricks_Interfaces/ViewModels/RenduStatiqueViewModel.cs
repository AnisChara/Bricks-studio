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
using System.Windows.Media.Media3D;

namespace Bricks_Interfaces.ViewModels
{
    public class RenduStatiqueViewModel : BaseNotifyPropertyChanged
    {
        public static DoubleClick entityMenu = null;
        public static bool copy = false;
        public static double Width;
        public static double Height;

        private ObservableCollection<string> listLevel { get; set; }
        public ObservableCollection<string> ListLevel
        {

            get => listLevel;
            set
            {
                var list = new ObservableCollection<string>();
                var levels = Entity.GetAllLevels();
                foreach (var level in levels)
                {
                    list.Add(level.Name);
                }
                listLevel = list;
                OnPropertyChanged(nameof(ListLevel));
            }

        }

        private string _currentLevel_name;
        public string CurrentLevel_name
        {
            get
            {
                return Models.Level.CurrentLevel;
            }
            set
            {
                if (value != null)
                    Models.Level.CurrentLevel = value;
                else Level.CurrentLevel = Level.FirstLevel;
                Entities = CurrentLevel.Entities;
                OnPropertyChanged(nameof(CurrentLevel_name));
            }
        }

        private Level _currentLevel;
        public Level CurrentLevel
        {
            get
            {
                return Entity.GetLevel(CurrentLevel_name);
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


        public RenduStatiqueViewModel(double width, double height)
        {
            InitializeFileWatcher();
            Entities = CurrentLevel.Entities;
            ListLevel = null;
            Width = width;
            Height = height;
        }

        private void InitializeFileWatcher()
        {

            _fileWatcher = new FileSystemWatcher
            {
                Path = MainWindowViewModel.ProjectPath,
                Filter = "Entity.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            _fileWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            _fileWatcher.Changed += (sender, e) =>
            {
                // Lorsque le fichier JSON est modifié, rechargez les données
                CurrentLevel_name = Level.CurrentLevel;
                Entities = CurrentLevel.Entities;
                ListLevel = null;
            };
            
            _fileWatcher.EnableRaisingEvents = true; // Active la surveillance
        }


        public void StartDrag(object parameter, Button button)
        {
            selectedEntity = parameter as Entity;
            selectedButton = button;
            dragging = true;
        }

        public void ActualiseDrag(Point e)
        {
            if (!dragging) return;


            bool can_move_left = true;
            bool can_move_right = true;
            bool can_move_top = true;
            bool can_move_bottom = true;
            bool atBorderRight = false;
            bool atBorderLeft = false;
            bool atBorderTop = false;
            bool atBorderBottom = false;

            var (direction,entity_collided) = selectedEntity.CheckAllCollision(Entities);

            int right_collision_index = direction.IndexOf("right");
            int left_collision_index = direction.IndexOf("left");
            int bottom_collision_index = direction.IndexOf("bottom");
            int top_collision_index = direction.IndexOf("top");

            if (right_collision_index >= 0 && e.X >= selectedEntity.x) { can_move_right = false; /*selectedEntity.x = entity_collided[right_collision_index].x - selectedEntity.width -1;*/ } //TOFIX
            if (left_collision_index >= 0 && e.X < selectedEntity.x) { can_move_left = false; /*selectedEntity.x = entity_collided[left_collision_index].x + entity_collided[left_collision_index].width +1;*/ }
            if (bottom_collision_index >= 0 && e.Y > selectedEntity.y) { can_move_bottom = false; /*selectedEntity.y = entity_collided[bottom_collision_index].y - selectedEntity.height -1;*/ }
            if (top_collision_index >= 0 && e.Y < selectedEntity.y) { can_move_top = false; /*selectedEntity.y = entity_collided[top_collision_index].y + entity_collided[top_collision_index].height +1;*/ }
            if (e.X < 0 && selectedEntity.x <= 0){ atBorderLeft = true; selectedEntity.x = 0; }
            if (e.X >= Width - selectedEntity.width && selectedEntity.x >= Width-selectedEntity.width){ atBorderRight = true; selectedEntity.x = Width-selectedEntity.width; }
            if (e.Y < 0 && selectedEntity.y <= 0){ atBorderTop = true; selectedEntity.y = 0; }
            if (e.Y >= Height - selectedEntity.height && selectedEntity.y >= Height - selectedEntity.height){ atBorderBottom = true; selectedEntity.y= Height-selectedEntity.height; }

            if (e.X > selectedEntity.x && (can_move_right || Keyboard.Modifiers == ModifierKeys.Shift) && !atBorderRight) selectedEntity.x = e.X;
            if (e.X < selectedEntity.x && (can_move_left || Keyboard.Modifiers == ModifierKeys.Shift) && !atBorderLeft) selectedEntity.x = e.X;
            if (e.Y < selectedEntity.y && (can_move_top || Keyboard.Modifiers == ModifierKeys.Shift) && !atBorderTop) selectedEntity.y = e.Y;
            if (e.Y > selectedEntity.y && (can_move_bottom || Keyboard.Modifiers == ModifierKeys.Shift) && !atBorderBottom) selectedEntity.y = e.Y;


            selectedEntity.margin = new Thickness(selectedEntity.x, selectedEntity.y, 0, 0);
        }

        public void StopDrag()
        {
            dragging = false;



            Entity.SaveEntities(Entities, CurrentLevel_name);
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
            entityMenu.Height = 450;

            // Synchroniser la position de la fenêtre
            entityMenu.Left = 700;
            entityMenu.Top = 200;

            entityMenu.Show();
        }

        public void CopyEntity(Entity entity)
        {
            if (entity.id == "Player") return;

            ObservableCollection<Entity> Entités = Entity.GetEntities(Level.CurrentLevel);


            var newEntity = new Entity(
                type: entity.type,
                id: Guid.NewGuid().ToString(),
                x: 100,
                y: 200,
                width: entity.width,
                height: entity.height,
                speed: entity.speed,
                is_collidable: entity.is_collidable,
                shape: entity.shape,
                weight: entity.weight,
                render: entity.render,
                has_weapon: entity.has_weapon,
                image: entity.image,
                Track: entity.Track,
                damage: entity.damage,
                max_health: entity.max_health
            );

            newEntity.margin = new Thickness(newEntity.x, newEntity.y, 0, 0);

            Entités.Add(newEntity);


            Entity.SaveEntities(Entités, Level.CurrentLevel);
        }

        public void DeleteEntity(Entity Entity)
        {

            if (Entity == null) return;
            if (Entity.id == "Player") return;

            ObservableCollection<Entity> Entities = Entity.GetEntities(CurrentLevel_name);
            int index = Entities.IndexOf(Entities.Where(e => e.id == Entity.id).FirstOrDefault());
            Entities.RemoveAt(index);
            Entity.SaveEntities(Entities, CurrentLevel_name);
        }

        public void Resize(Point e, double width, double height)
        {
            if (!dragging) return;

            bool can_move_right = true;
            bool can_move_bottom = true;

            var (direction, entity_collided) = selectedEntity.CheckAllCollision(Entities);

            int right_collision_index = direction.IndexOf("right");
            int bottom_collision_index = direction.IndexOf("bottom");

            if (right_collision_index >= 0 && e.X >= selectedEntity.x) can_move_right = false;
            if (bottom_collision_index >= 0 && e.Y > selectedEntity.y) can_move_bottom = false;
            if (e.X >= width) can_move_right = false;
            if (e.Y >= height) can_move_bottom = false;

            if (e.X > selectedEntity.x+selectedEntity.width && can_move_right) selectedEntity.width += e.X - (selectedEntity.x+selectedEntity.width);
            if (e.X < selectedEntity.x + selectedEntity.width && selectedEntity.width >= 20) selectedEntity.width = e.X - selectedEntity.x;
            if (e.Y < selectedEntity.y + selectedEntity.height && selectedEntity.height >= 20) selectedEntity.height = e.Y - selectedEntity.y;
            if (e.Y > selectedEntity.y + selectedEntity.height && can_move_bottom) selectedEntity.height = e.Y - selectedEntity.y;

            if (selectedEntity.width < 20) selectedEntity.width = 20;
            if (selectedEntity.height < 20) selectedEntity.height = 20;

        }

        public void ResizeScreen(double width, double height)
        {
            if (CurrentLevel.LastWidth == 0 || CurrentLevel.LastHeight == 0)
            {
                CurrentLevel.LastHeight = height;
                CurrentLevel.LastWidth = width;
            }
            if (Width <= 0 || Height <= 0)
            {
                Width = width;
                Height = height;
            }
            if (width != Width || height != Height)
            {
                foreach (var entity in Entities)
                {

                    entity.x *= (width / Width);
                    entity.y *= (height / Height);
                    entity.margin = new Thickness(entity.x, entity.y, 0, 0);
                    entity.width *= (width / Width);
                    entity.height *= (height / Height);
                }
            }
            Width = width; Height = height;
            Entity.SaveEntities(Entities, CurrentLevel_name, Width, Height);
        }
    }
}
