using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Parameters
    {
        public string Value { get; set; }
        public int Param_count { get; set; }
        public string Show_box { get; set;}

        public Parameters(string Value, int Param_count) { 
        
            this.Value = Value;
            this.Param_count = Param_count;

            if (Param_count > 0) { this.Show_box = "Visible"; }
            else this.Show_box = "Collapsed";
        
        }


    }
}
