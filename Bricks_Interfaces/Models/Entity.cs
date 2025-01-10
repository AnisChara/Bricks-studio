using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Entity
    {
    public string type { get; set; }

    public string Id { get; set; }

    public int x { get; set; }

     public int y { get; set; }

    public int width { get; set; }

    public int height { get; set; }

    public int speed { get; set; }

    public bool is_collidable { get; set; }

     public string shape {  get; set; }

    public int  weight {  get; set; } 

    public bool render {  get; set; }

    public bool has_weapon {  get; set; }

    public int? max_health { get; set; }

        public Entity (string type, string Id, int x, int y, int width,  int height, int speed, bool is_collidable,  string shape, int weight, bool render, bool has_weapon, int? max_health)
        {
            this.type = type;
            this.Id = Id;
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
