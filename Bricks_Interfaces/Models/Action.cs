using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Action
    {
        public string Name { get; set; }
        public string Function { get; set; }
        public string Description { get; set; }
        public int Param_count { get; set; }
        public string? Parameters { get; set; }
        public string Param_box { get; set; }

        public Action(string Name, string Function, string Description, int param_count, string? parameters)
        {
            this.Name = Name;
            this.Function = Function;
            this.Description = Description;
            this.Param_count = param_count;
            this.Parameters = parameters;

            if (Param_count > 0 ) { this.Param_box = "Visible"; }
            else this.Param_box = "Collapsed";
        }

        public string Formate()
        {
            /*if (Parameters.Count <= 0)
            {
                return this.Function;
            }*/

            StringBuilder result = new StringBuilder(this.Function);
            int index = 0;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '*')
                {
                    result.Remove(i, 1);
                    result.Insert(i, Parameters);
                    index++;
                }
            }

            return result.ToString();
        }
    }
}
