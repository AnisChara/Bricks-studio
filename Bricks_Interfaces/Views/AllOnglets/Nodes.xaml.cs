using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bricks_Interfaces.Models;
using Bricks_Interfaces.ViewModels;

namespace Bricks_Interfaces.Views.AllOnglets
{
    /// <summary>
    /// Logique d'interaction pour Nodes.xaml
    /// </summary>
    public partial class Nodes : UserControl
    {
        public Nodes()
        {
            InitializeComponent();
            this.DataContext = new NodeViewModel();
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                var grid = button.Content as Grid;
                // Récupérez l'entité liée via le DataContext
                var entity = button.DataContext as Brick; // Remplacez YourEntityType par le type réel de vos entités
                if (entity != null)
                {
                    ((NodeViewModel)this.DataContext).StartDrag(entity,button,grid.Children.OfType<Rectangle>().FirstOrDefault());
                }
            }
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point _startPoint = e.GetPosition(sender as Grid);

            ((NodeViewModel)this.DataContext).ActualiseDrag(_startPoint, this.ActualWidth, this.ActualHeight);
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((NodeViewModel)this.DataContext).StopDrag();
        }
        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                // Récupérez l'entité liée via le DataContext
                var entity = button.DataContext as Brick; // Remplacez YourEntityType par le type réel de vos entités
                if (entity != null)
                {
                    bool FullVisibility = entity is Node;
                    ((NodeViewModel)this.DataContext).OpenBrickMenu(entity);
                }
            }
        }
    }
}
