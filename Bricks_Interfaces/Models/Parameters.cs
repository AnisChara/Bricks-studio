using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Parameters
    {
        public ObservableCollection<string>? Available_values { get; set; }

        public string Value { get; set; }
        public int Param_count { get; set; }
        public string Show_list { get; set;}
        public string Show_number { get; set; }
        public string Param_type { get; set; }

        public Parameters(ObservableCollection<string> Available_values, int Param_count, string Param_type) { 

            if (Param_type == "string" && Available_values == null)
            {
                throw new ArgumentNullException(nameof(Available_values));
            }
        
            this.Available_values = Available_values;
            this.Param_count = Param_count;
            this.Param_type = Param_type;

            if(Param_count <= 0)
            {
                this.Show_list = "Collapsed";
                this.Show_number = "Collapsed";
                return;
            }

            if (Param_type == "number")
            {
                this.Show_number = "Visible";
                this.Show_list = "Collapsed";
            }
            else if (Param_type == "string") 
            {
                this.Show_list = "Visible";
                this.Show_number = "Collapsed";
            }
        }
    }
}
