using Bricks_Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.Json;
using System.Windows;
using Bricks_Interfaces.Views.AllOnglets;

namespace Bricks_Interfaces.ViewModels
{
    public class MecaniqueViewModel : BaseNotifyPropertyChanded
    {

        public ObservableCollection<Models.Action> Actions { get; set; }
        public ObservableCollection<Models.Action> Mecanique { get; set; }
        public ICommand AddActionCommand { get; set; }
        public ICommand ClearMecaniqueCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        public Models.Mecanique Final_mecanique { get; set; }

        private string mecaniqueName;
        public string MecaniqueName
        {
            get => mecaniqueName;
            set
            {
                if (mecaniqueName != value)
                {
                    mecaniqueName = value;
                    OnPropertyChanged(nameof(MecaniqueName));
                }
            }
        }

        public MecaniqueViewModel()
        {
            string json = System.IO.File.ReadAllText("A:/Code/bricks-studio/Bricks_Interfaces/Action_dispo.json");
            Actions = new ObservableCollection<Models.Action>(JsonSerializer.Deserialize<List<Models.Action>>(json));

        
            Mecanique = new ObservableCollection<Models.Action>();
            AddActionCommand = new RelayCommand(AddAction);
            ClearMecaniqueCommand = new RelayCommand(ClearMecanique);
            ConfirmCommand = new RelayCommand(Confirm);

        }


        private void AddAction(object parameter)
        {
            if (parameter is Models.Action selectedEvent && !Mecanique.Contains(selectedEvent))
            {
                Mecanique.Add(selectedEvent);
            }
        }

        private void ClearMecanique(object parameter)
        {
            Mecanique.Clear();
            MecaniqueName = string.Empty;
        }

        private void Confirm(object parameter)
        {
            string name = parameter as string;

            if (!string.IsNullOrEmpty(name) && Mecanique.Count() > 0)
            {
                string json = System.IO.File.ReadAllText("A:/Code/bricks-studio/Bricks_Interfaces/Nodes.json");
                ObservableCollection<Node> Nodes = new ObservableCollection<Node>(JsonSerializer.Deserialize<List<Node>>(json));

                if (Nodes.Count <= 0)
                {
                    MessageBox.Show("Veuillez d'abord créer un noeud");
                    return;
                }

                if (Nodes[Nodes.Count - 1].Mecanique != null)
                {
                    MessageBox.Show("Une mecanique est deja dans le dernier noeud");
                    return;
                }

                Final_mecanique = new Models.Mecanique(Mecanique.ToList(), name);
                ClearMecanique(parameter);
                Nodes[Nodes.Count - 1].Mecanique = Final_mecanique;

                json = JsonSerializer.Serialize(Nodes, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText("A:\\Code\\bricks-studio\\Bricks_Interfaces\\Nodes.json", json);

            }
            else
            {
                MessageBox.Show("Veuillez rentrez un nom et/ou une mecanique valide");
            }
        }
    }
}
