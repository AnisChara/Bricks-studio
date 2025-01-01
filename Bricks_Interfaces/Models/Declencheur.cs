using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Declencheur
    {
        public string Name { get; set; }
        public List<Event> Events { get; set; }

        public Declencheur(List<Event> events, string Name) {
               
            this.Events = events;
            this.Name = Name;

        }
    }

}
