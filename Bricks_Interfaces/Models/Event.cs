using Bricks_Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Event : Brick
    {
        public string Function { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public ObservableCollection<string>? Available_parameter_values { get; set; }
        public string Parameter_value { get; set; }
        public int Parameter_count { get; set; }
        public string Show_list { get; set; }
        public string Show_number { get; set; }
        public string Parameter_type { get; set; }


        public Event(string Name, string Function, string Description, ObservableCollection<string> Available_parameter_values, int Parameter_count, string Parameter_type, double x = 0, double y = 0, double width = 100, double height = 35) : base(Name, x, y, width, height)
        {
            this.Name = Name;
            this.Function = Function;
            this.Description = Description;
            this.Image = MainWindowViewModel.formattedPath + "/../../../Assets/lego_bleu.png";
            Text = this.Name + " " + this.Parameter_value;

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

        public static ObservableCollection<Event> GetSavedEvents()
        {
            ObservableCollection<Event> Nodes = new ObservableCollection<Event>();
            string json;
            bool succes = false;

            while (!succes)
            {
                try
                {
                    json = System.IO.File.ReadAllText(MainWindowViewModel.EventsPath);
                    Nodes = JsonSerializer.Deserialize<ObservableCollection<Event>>(json);
                    succes = true;

                }
                catch (Exception e) { }
            }

            return Nodes;
        }


        public static void SaveEvents(ObservableCollection<Models.Event> Levels)
        {
            bool succes = false;

            while (!succes)
            {
                try
                {
                    string json = JsonSerializer.Serialize(Levels, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText(MainWindowViewModel.EventsPath, json);
                    succes = true;

                }
                catch (Exception e) { }
            }
        }

        public static ObservableCollection<Event> GetEvents()
        {
            ObservableCollection<Event> events = 
                [
                    new Event
                    (
                        "Presser ",
                        "keys[pygame.K_*]",
                        "l'utilisateur reste appuyé sur la touche désignée",
                        ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m","n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "ESCAPE", "SPACE"],
                        1,
                        "string"
                    ),
                    new Event
                    (
                        "Appuyer sur",
                        "Game.keys.* == True",
                        "l'utilisateur appuie sur la touche désignée",
                        ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "ECHAPE", "ESPACE", "ENTRE"],
                        1,
                        "string"
                    )
                ];

            return events;
        }

    }
}
