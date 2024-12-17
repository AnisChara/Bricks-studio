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
        public List<string> Events { get; set; } = new List<string>();
    }

}
