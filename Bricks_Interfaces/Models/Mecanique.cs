using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Mecanique
    {
        public string Name { get; set; }
        public List<Action> Actions { get; set; }

        public Mecanique(List<Action> Actions , string name) { 
            
            this.Actions = Actions;
            this.Name = name;
        }
    }

}
