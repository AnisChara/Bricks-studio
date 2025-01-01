using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Function { get; set; }
        public string Description { get; set; }

        public Event(string name, string function, string description)
        {
            this.Name = name;
            this.Function = function;
            this.Description = description;
        }
    }
}
