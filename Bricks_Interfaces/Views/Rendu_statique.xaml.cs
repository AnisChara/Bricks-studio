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

namespace Bricks_Interfaces.Views
{
    /// <summary>
    /// Logique d'interaction pour Rendu_statique.xaml
    /// </summary>
    public partial class Rendu_statique : UserControl
    {
        public Rendu_statique()
        {
            InitializeComponent();
            this.DataContext = new RenduStatiqueViewModel();
        }


        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                // Récupérez l'entité liée via le DataContext
                var entity = button.DataContext as Entity; // Remplacez YourEntityType par le type réel de vos entités
                if (entity != null)
                {
                    ((RenduStatiqueViewModel)this.DataContext).StartDrag(entity, button);
                }
            }
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point _startPoint = e.GetPosition(sender as Grid);


            ((RenduStatiqueViewModel)this.DataContext).ActualiseDrag(_startPoint);
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((RenduStatiqueViewModel)this.DataContext).StopDrag();
        }
    }
}
