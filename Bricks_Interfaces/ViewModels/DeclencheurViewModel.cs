using Bricks_Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Bricks_Interfaces.Views.AllOnglets;
using System.Windows.Input;
using System.Xml.Linq;
using System.Text.Json;

namespace Bricks_Interfaces.ViewModels
{
    public class DeclencheurViewModel : BaseNotifyPropertyChanded
    {

        public ObservableCollection<Event> Events { get; set; } // JSON a recuperer
        public ObservableCollection<Event> Declencheur { get; set; }
        public ICommand AddEventCommand { get; set; }
        public ICommand ClearDeclencheurCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        public Declencheur Final_declencheur { get; set; }

        private string declencheurName;
        public string DeclencheurName
        {
            get => declencheurName;
            set
            {
                if (declencheurName != value)
                {
                    declencheurName = value;
                    OnPropertyChanged(nameof(DeclencheurName));
                }
            }
        }
        public DeclencheurViewModel() {

            string json = System.IO.File.ReadAllText("A:/Code/bricks-studio/Bricks_Interfaces/Event_dispo.json");
            Events = new ObservableCollection<Event>(JsonSerializer.Deserialize<List<Event>>(json));
            Declencheur = new ObservableCollection<Event>();
            AddEventCommand = new RelayCommand(AddEvent);
            ClearDeclencheurCommand = new RelayCommand(ClearDeclencheur);
            ConfirmCommand = new RelayCommand(Confirm);

        }

        private void AddEvent(object parameter)
        {
            if (parameter is Event selectedEvent && !Declencheur.Contains(selectedEvent))
            {
                Declencheur.Add(selectedEvent);
            }
        }

        private void ClearDeclencheur(object parameter)
        {
            Declencheur.Clear();
            DeclencheurName = string.Empty;
        }

        private void Confirm(object parameter)
        {
            string name = parameter as string;

            if (!string.IsNullOrEmpty(name))
            {
                Final_declencheur = new Declencheur(Declencheur.ToList(), name);
                ClearDeclencheur(parameter);
                string json = System.IO.File.ReadAllText("A:/Code/bricks-studio/Bricks_Interfaces/Nodes.json");
                ObservableCollection<Node> Nodes = new ObservableCollection<Node>(JsonSerializer.Deserialize<List<Node>>(json));
                Nodes[0].Declencheur = Final_declencheur;

                json = JsonSerializer.Serialize(Nodes, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText("A:\\Code\\bricks-studio\\Bricks_Interfaces\\Nodes.json", json);
            }
            else
            {
                MessageBox.Show("Veuillez rentrez un nom valide");
            }
        }
    }
}
