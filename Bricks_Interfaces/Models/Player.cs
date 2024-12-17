using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public string level { get; set; }
        public string capacity { get; set; }
    }
}
