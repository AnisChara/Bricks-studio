using Bricks_Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bricks_Interfaces.Models
{
    public class Projet
    {
        public string name { get; set; }
        public bool Gravity { get; set; }
        public Entity SelectedEntity { get; set; }

        public Projet(string name, bool gravity, Entity selectedEntity)
        {
            this.name = name;
            this.Gravity = gravity;
            this.SelectedEntity = selectedEntity;
        }
        public static Projet GetData()
        {
            Projet data = null;
            bool success = false;

            while (!success)
            {
                try
                {
                    if (!File.Exists(MainWindowViewModel.DataPath))
                        throw new FileNotFoundException($"Fichier non trouvé : {MainWindowViewModel.DataPath}");

                    string json = File.ReadAllText(MainWindowViewModel.DataPath);
                    data = JsonSerializer.Deserialize<Projet>(json);
                    success = true;
                }
                catch (Exception)
                {
                    // Tu peux ajouter un délai ou un log ici si besoin
                }
            }

            return data;
        }

        public static void SaveData(Projet data)
        {
            bool success = false;

            while (!success)
            {
                try
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(data, options);
                    File.WriteAllText(MainWindowViewModel.DataPath, json);
                    success = true;
                }
                catch (Exception)
                {
                    // Tu peux ajouter un délai ou un log ici si besoin
                }
            }
        }


    }


}
