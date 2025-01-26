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
                var node = button.DataContext as Node; 
                if (node != null)
                {
                    ((NodeViewModel)this.DataContext).StartDrag(node);
                    return;
                }
                var meca = button.DataContext as Mecanique;
                if (meca != null)
                {
                    ((NodeViewModel)this.DataContext).StartDrag(meca);
                    return;
                }
                var decl = button.DataContext as Declencheur;
                if (decl != null)
                {
                    ((NodeViewModel)this.DataContext).StartDrag(decl);
                    return;
                }
            }
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Grid sen = (Grid)sender;
            Point _startPoint = e.GetPosition(sender as Grid);

            ((NodeViewModel)this.DataContext).ActualiseDrag(_startPoint, sen.ActualWidth, sen.ActualHeight);
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((NodeViewModel)this.DataContext).StopDrag();
        }
    }
}
