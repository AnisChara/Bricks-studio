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
    public class MecaniqueViewModel : BaseNotifyPropertyChanged
    {

        private ObservableCollection<Models.Action> actions;
        public ObservableCollection<Models.Action> Actions
        {
            get => actions;
            set
            {
                if (actions != value)
                {
                    actions = value;
                    OnPropertyChanged(nameof(Actions));
                }
            }
        }

        public ObservableCollection<Models.Action> SavedActions { get; set; }
        public ICommand AddActionCommand { get; set; }

        public MecaniqueViewModel()
        {
            Actions = Models.Action.GetActions();
            AddActionCommand = new RelayCommand(AddAction);
        }


        private void AddAction(object parameter)
        {
            Models.Action selectedAction = parameter as Models.Action;

            if (selectedAction.Parameter_type == "number" && !int.TryParse(selectedAction.Parameter_value, out _))
            {
                MessageBox.Show("Veuillez fournir un parametre valide");
                return;
            }

            if (selectedAction.Parameter_count > 0 && selectedAction.Parameter_value == null)
            {
                MessageBox.Show("Veuillez fournir une valeur pour tous les parametres");
                return;
            }

            selectedAction.Text = selectedAction.Name + " " + selectedAction.Parameter_value;
            SavedActions = Models.Action.GetSavedActions();
            SavedActions.Add(selectedAction);
            Models.Action.SaveActions(SavedActions);
        }
    }
}
