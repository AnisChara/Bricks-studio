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
    public class DeclencheurViewModel : BaseNotifyPropertyChanged
    {

        public ICommand AddEventCommand { get; set; }
        public ICommand ClearDeclencheurCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public Declencheur Final_declencheur { get; set; }

        public ObservableCollection<Event> Declencheur { get; set; }

        private ObservableCollection<Event> events { get; set; }
        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                if (events != value)
                {
                    events = value;
                    OnPropertyChanged(nameof(Events));
                }
            }
        }
        

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

            Events = Models.Event.GetEvents();
            Declencheur = new ObservableCollection<Event>();
            AddEventCommand = new RelayCommand(AddEvent);
            ClearDeclencheurCommand = new RelayCommand(ClearDeclencheur);
            ConfirmCommand = new RelayCommand(Confirm);

        }

        private void AddEvent(object parameter)
        {
            Event selectedEvent = parameter as Event;    

            if (Declencheur.Contains(selectedEvent))
            {
                MessageBox.Show("Evenement deja dans le declencheur");
                return;
            }
            if (selectedEvent.Parameter_type == "number" && !int.TryParse(selectedEvent.Parameter_value, out _)) 
            {
                MessageBox.Show("Veuillez fournir un parametre valide");
                return ;
            }

            if (selectedEvent.Parameter_count > 0 && selectedEvent.Parameter_value == null)
            {
                MessageBox.Show("Veuillez fournir une valeur pour tous les parametres");
                return;
            }

            Event copy = new Event(selectedEvent.Name,selectedEvent.Function,selectedEvent.Description,null,selectedEvent.Parameter_count,selectedEvent.Parameter_type);
            copy.Parameter_value = selectedEvent.Parameter_value; 

            Declencheur.Add(copy);
        }

        private void ClearDeclencheur(object parameter)
        {
            Declencheur.Clear();
            DeclencheurName = string.Empty;
        }

        private void Confirm(object parameter)
        {
            string name = parameter as string;

            if (!string.IsNullOrEmpty(name) && Declencheur.Count() > 0)
            {

                ObservableCollection<Models.Declencheur> Declencheurs = Models.Declencheur.GetDeclencheurs();

                Final_declencheur = new Models.Declencheur(Declencheur.ToList(), name);
                ClearDeclencheur(parameter);
                Declencheurs.Add(Final_declencheur);

                Models.Declencheur.SaveDeclencheurs(Declencheurs);

            }
            else
            {
                MessageBox.Show("Veuillez rentrez un nom et/ou une mecanique valide");
            }
        }
    }
}
