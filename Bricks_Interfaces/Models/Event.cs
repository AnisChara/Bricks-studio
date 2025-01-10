using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Function { get; set; }
        public string Description { get; set; }
        public ObservableCollection<string>? Available_parameter_values { get; set; }
        public string Parameter_value { get; set; }
        public int Parameter_count { get; set; }
        public string Show_list { get; set; }
        public string Show_number { get; set; }
        public string Parameter_type { get; set; }


        public Event(string Name, string Function, string Description, ObservableCollection<string> Available_parameter_values, int Parameter_count, string Parameter_type)
        {
            this.Name = Name;
            this.Function = Function;
            this.Description = Description;

            this.Available_parameter_values = Available_parameter_values;
            this.Parameter_count = Parameter_count;
            this.Parameter_type = Parameter_type;

            if (Parameter_count <= 0)
            {
                this.Show_list = "Collapsed";
                this.Show_number = "Collapsed";
                return;
            }

            if (Parameter_type == "number")
            {
                this.Show_number = "Visible";
                this.Show_list = "Collapsed";
            }
            else if (Parameter_type == "string")
            {
                this.Show_list = "Visible";
                this.Show_number = "Collapsed";
            }
        }

        public string Formate()
        {

            if (this.Parameter_count <= 0)
            {

                return this.Function;
            }

            StringBuilder result = new StringBuilder(this.Function);
            int index = 0;

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '*')
                {
                    result.Remove(i, 1);
                    result.Insert(i, this.Parameter_value);
                    index++;
                }
            }

            return result.ToString();
        }

        public static ObservableCollection<Event> GetEvents()
        {
            ObservableCollection<Event> events = 
                [
                    new Event
                    (
                        "press",
                        "keys[pygame.K_*]",
                        "l'utilisateur reste appuyer sur la touche désignée",
                        ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m","n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "ESCAPE", "SPACE"],
                        1,
                        "string"
                    ),
                    new Event
                    (
                        "Keydown",
                        "Game.keys.* == True",
                        "l'utilisateur appuie sur la touche désignée",
                        ["RETURN","SPACE"],
                        1,
                        "string"
                    )
                ];

            return events;
        }

    }
}
