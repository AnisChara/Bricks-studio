using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Action
    {
        public string Name { get; set; }
        public string Function { get; set; }
        public string Description { get; set; }

        public Action(string Name, string Function, string Description)
        {
            this.Name = Name;
            this.Function = Function;
            this.Description = Description;
        }
    }
}
