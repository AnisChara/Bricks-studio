using Bricks_Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Bricks_Interfaces.Models
{
    public class Entity : BaseNotifyPropertyChanged
    {
        private string _type;
        public string type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(type));
                }
            }
        }

        private double _x;
        public double x
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged(nameof(x));
                }
            }
        }

        private double _y;
        public double y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged(nameof(y));
                }
            }
        }

        private double _width;
        public double width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged(nameof(width));
                }
            }
        }

        private double _height;
        public double height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(nameof(height));
                }
            }
        }

        private string _color;
        public string color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged(nameof(color));
                }
            }
        }
        private string _id;
        public string id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(id));
                }
            }
        }


        private Thickness _margin;
        public Thickness margin
        {
            get => _margin;
            set
            {
                if (_margin != value)
                {
                    _margin = value;
                    OnPropertyChanged(nameof(margin)); // Notification de changement
                }
            }
        }

        private int _speed;
        public int speed
        {
            get => _speed;
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    OnPropertyChanged(nameof(speed));
                }
            }
        }

        private bool _isCollidable;
        public bool is_collidable
        {
            get => _isCollidable;
            set
            {
                if (_isCollidable != value)
                {
                    _isCollidable = value;
                    OnPropertyChanged(nameof(is_collidable));
                }
            }
        }

        private string _shape;
        public string shape
        {
            get => _shape;
            set
            {
                if (_shape != value)
                {
                    _shape = value;
                    OnPropertyChanged(nameof(shape));
                }
            }
        }

        private bool _render;
        public bool render
        {
            get => _render;
            set
            {
                if (_render != value)
                {
                    _render = value;
                    OnPropertyChanged(nameof(render));
                }
            }
        }

        private bool _hasWeapon;
        public bool has_weapon
        {
            get => _hasWeapon;
            set
            {
                if (_hasWeapon != value)
                {
                    _hasWeapon = value;
                    OnPropertyChanged(nameof(has_weapon));
                }
            }
        }

        private int? _maxHealth;
        public int? max_health
        {
            get => _maxHealth;
            set
            {
                if (_maxHealth != value)
                {
                    _maxHealth = value;
                    OnPropertyChanged(nameof(max_health));
                }
            }
        }

        private double _weight;
        public double weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged(nameof(weight));
                }
            }
        }
        private int? _damage;
        public int? damage
        {
            get => _damage;
            set
            {
                if (_damage != value)
                {
                    _damage = value;
                    OnPropertyChanged(nameof(damage));
                }
            }
        }

        private string? _image;
        public string? image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(image));
                }
            }
        }

        private bool _track;
        public bool Track
        {
            get => _track;
            set
            {
                if (_track != value)
                {
                    _track = value;
                    OnPropertyChanged(nameof(Track));
                }
            }
        }
        public Entity () { } // pour le json


        public Entity(string type, string id, double x, double y, double width, double height, int speed, bool is_collidable, string shape, double weight, bool render, bool has_weapon, int? max_health = null, int? damage = null, string image = null, bool Track = false)
        {
            this.type = type;
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.speed = speed;
            this.is_collidable = is_collidable;
            this.shape = shape;
            this.weight = weight;
            this.render = render;
            this.has_weapon = has_weapon;
            this.max_health = max_health;
            this.damage = damage;
            this.color = color;
            this.image = image;
            this.Track = Track;
            this.margin = new Thickness(x, y, 0, 0);
        }

        public enum CollisionDirection
        {
            None,
            Top,
            Bottom,
            Left,
            Right
        }
        public (bool isColliding, string direction) CheckCollision( Entity entity2)
        {

            // V�rifie les collisions en utilisant les coordonn�es et les dimensions
            bool isColliding = this.x < entity2.x + entity2.width &&
                               this.x + this.width > entity2.x &&
                               this.y < entity2.y + entity2.height &&
                               this.y + this.height > entity2.y;

            if (!isColliding)
            {
                return (false, "none");
            }

            // D�termine la direction de la collision
            string direction ="none";

            double overlapLeft = this.x + this.width - entity2.x;
            double overlapRight = entity2.x + entity2.width - this.x;
            double overlapTop = this.y + this.height - entity2.y;
            double overlapBottom = entity2.y + entity2.height - this.y;

            double minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight), Math.Min(overlapTop, overlapBottom));

            if (minOverlap == overlapLeft)
            {
                direction = "right";
            }
            else if (minOverlap == overlapRight)
            {
                direction = "left";
            }
            else if (minOverlap == overlapTop)
            {
                direction = "bottom";
            }
            else if (minOverlap == overlapBottom)
            {
                direction = "top";
            }

            return (true, direction);
        }


        public (List<string>, List<Entity>) CheckAllCollision(ObservableCollection<Entity> Entities)
        {
            List<string> directions = [];
            List<Entity> Collisions = [];

            foreach (Entity entity in Entities)
            {
                if (entity == this) continue;
                var (result,direction) = this.CheckCollision(entity);
                if (result)
                {
                    directions.Add(direction);
                    Collisions.Add(entity);
                }
                else continue;
            }
            return (directions, Collisions);
        }

        public static Level GetLevel(string level_name)
        {
            ObservableCollection<Level> Levels = GetAllLevels();
            return Levels.FirstOrDefault(l => l.Name.Trim().Equals(level_name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public static ObservableCollection<Entity> GetEntities(string level)
        {
            ObservableCollection<Entity> Entities = new ObservableCollection<Entity>();
            string json;
            ObservableCollection<Level> Levels;
            bool succes = false;

            while (!succes)
            {
                try
                {
                    json = System.IO.File.ReadAllText(MainWindowViewModel.EntityPath);
                    Levels = JsonSerializer.Deserialize<ObservableCollection<Level>>(json);
                    if (Levels != null && Levels.Count > 0)
                    {
                        Entities = new ObservableCollection<Entity>(
                            Levels.FirstOrDefault(l => l.Name == level).Entities
                        );
                    }
                    succes = true;

                }
                catch (Exception e) { }
            }
            
            return Entities;
        }

        public static ObservableCollection<Level> GetAllLevels() {
            string json;
            ObservableCollection<Level> listofLevels = new ObservableCollection<Level>();
            bool succes = false;

            while (!succes)
            {
                try
                {
                    json = System.IO.File.ReadAllText(MainWindowViewModel.EntityPath);
                    listofLevels = JsonSerializer.Deserialize<ObservableCollection<Level>>(json);
                    succes = true;
                }
                catch (Exception e) { }
            }

            return listofLevels;
        }

        public static void SaveLevel(Level Level, string level_name)
        {
            var Levels = GetAllLevels();
            int index = Levels.IndexOf(Levels.FirstOrDefault(l => l.Name == level_name));
            Levels[index] = Level;

            bool succes = false;

            while (!succes)
            {
                try
                {
                    string json = JsonSerializer.Serialize(Levels, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText(MainWindowViewModel.EntityPath, json);
                    succes = true;

                }
                catch (Exception e) { }
            }
        }

        public static void SaveEntities(ObservableCollection<Entity> Entities , string level_name, double? width = null, double? height = null)
        {
            var Levels = GetAllLevels();
            int index = Levels.IndexOf(GetLevel(level_name));
            Levels[index].Entities = Entities;
            if (width!=null && height!=null)
            {
                Levels[index].LastHeight = (double)height;
                Levels[index].LastWidth = (double)width;
            }

            SaveAllLevels(Levels);
        }

        public static void SaveAllLevels(ObservableCollection<Level> Levels)
        {
            bool succes = false;

            while (!succes)
            {
                try
                {
                    string json = JsonSerializer.Serialize(Levels, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText(MainWindowViewModel.EntityPath, json);
                    succes = true;

                }
                catch (Exception e) {  }
            }
        }
    }
}
