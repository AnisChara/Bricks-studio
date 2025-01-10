using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bricks_Interfaces.ViewModels
{
    internal class AssetsViewModel : BaseNotifyPropertyChanged
    {
        public ICommand AddStructure {  get; set; }
        public ICommand AddObstacle {  get; set; }
        public ICommand AddEnnemy { get; set; }
        public ICommand AddZone { get; set; }

        public AssetsViewModel() 
        {
            AddStructure = new RelayCommand(Add_structure);
            AddObstacle = new RelayCommand(Add_obstacle);
            AddEnnemy = new RelayCommand(Add_Ennemy);
            AddZone = new RelayCommand(Add_Zone);
        }
        
        private void Add_structure(object parameter) 
        {
            MessageBox.Show("Structure ajoutée");
        }
        private void Add_obstacle(object parameter) 
        {
            MessageBox.Show("Obstacle ajouté");
        }

        private void Add_Ennemy(object parameter)
        {
            MessageBox.Show("Ennemy ajouté");
        }

        private void Add_Zone(object parameter)
        {
            MessageBox.Show("Zone ajoutée");
        }
    }
}
