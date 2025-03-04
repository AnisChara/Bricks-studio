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

        public ObservableCollection<Event> SavedEvents { get; set; }
        

        public DeclencheurViewModel() {

            Events = Models.Event.GetEvents();
            AddEventCommand = new RelayCommand(AddEvent);
        }

        private void AddEvent(object parameter)
        {
            Event selectedEvent = parameter as Event;    

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

            selectedEvent.Text = selectedEvent.Name + " " + selectedEvent.Parameter_value;
            SavedEvents = Event.GetSavedEvents();
            SavedEvents.Add(selectedEvent);
            Event.SaveEvents(SavedEvents);

        }
    }
}
