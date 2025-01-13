using Bricks_Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private int _weight;
        public int weight
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

        public Entity () { } // pour le json


        public Entity(string type, string id, int x, int y, int width, int height, int speed, bool is_collidable, string shape, int weight, bool render, bool has_weapon, int? max_health = null)
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
        }


    }

}
