using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Mecanique
    {
        public string Nom { get; set; }
        public List<string> Actions { get; set; } = new List<string>();
    }

}
