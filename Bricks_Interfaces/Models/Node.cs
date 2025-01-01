using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Node
    {
        public string Name { get; set; }
        public Mecanique Mecanique { get; set; } 
        public Declencheur Declencheur { get; set; } 

        public Node(string name, Mecanique mecanique, Declencheur declencheur) { 
            
            Name = name;
            Mecanique = mecanique;
            Declencheur = declencheur;
        }
    }

}
