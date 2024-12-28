using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Declencheur
    {
        public string Nom { get; set; }
        public List<Event> Events { get; set; }

        public Declencheur(List<Event> Events, string Nom) {
               
            this.Events = Events;
            this.Nom = Nom;

        }
    }

}
