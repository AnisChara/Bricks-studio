using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Node
    {
        public string Nom { get; set; }
        public List<Mecanique> Mecaniques { get; set; } = new List<Mecanique>();
        public List<Declencheur> Declencheurs { get; set; } = new List<Declencheur>();
    }

}
