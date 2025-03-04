using Bricks_Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bricks_Interfaces.Models
{
    public abstract class Brick : BaseNotifyPropertyChanged
    {
        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
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
                    OnPropertyChanged(nameof(margin));
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
        public string NodeId { get; set; } = string.Empty;

        public Brick(string name, double x, double y, double width, double height)
        {
            Name = name;
            this.id = Guid.NewGuid().ToString();
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.margin = new Thickness(x,y,0,0);
        }

        public (bool isColliding, string direction) CheckCollision(Brick entity2)
        {
            // Vérifie si les deux entités sont marquées comme "collidables"
            // Vérifie les collisions en utilisant les coordonnées et les dimensions
            bool isColliding = this.x < entity2.x + entity2.width &&
                               this.x + this.width > entity2.x &&
                               this.y < entity2.y + entity2.height &&
                               this.y + this.height > entity2.y;

            if (!isColliding)
            {
                return (false, "none");
            }

            // Détermine la direction de la collision
            string direction = "none";

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

        public (string, Brick) CheckAllCollision(IEnumerable<Brick> Entities, ref string debug)
        {
            foreach (Brick entity in Entities)
            {
                if (entity == this) continue;
                if (entity.NodeId == this.NodeId && entity.NodeId != string.Empty) continue;
                //debug = this.y.ToString();
                var (result, direction) = this.CheckCollision(entity);
                if (result) return (direction, entity);
                else continue;
            }
            return ("none", null);
        }
    }
}
