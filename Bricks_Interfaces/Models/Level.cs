using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bricks_Interfaces.ViewModels;

namespace Bricks_Interfaces.Models
{
    public class Level
    {
        public string Name { get; set; }
        public ObservableCollection<Entity> Entities { get; set; }
        public string Background { get; set; } = MainWindowViewModel.formattedPath + "/../../../Assets/lego_rouge.png\"Assets/Fond.jpg";

        public static string FirstLevel = "Niveau 0";

        public static string CurrentLevel = FirstLevel;

        public Level() { }
        public Level(string Name,  ObservableCollection<Entity> Entitie) {
        
            this.Name = Name;
            this.Entities = Entitie;
        }

        public override bool Equals(object obj)
        {
            if (obj is Level other)
            {
                return Name.Equals(other.Name, StringComparison.Ordinal);
            }
            return false;
        }
    }
}
