﻿using Bricks_Interfaces.Models;
using Bricks_Interfaces.ViewModels;
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

namespace Bricks_Interfaces.Views
{
    /// <summary>
    /// Logique d'interaction pour Bricks.xaml
    /// </summary>
    public partial class Bricks : UserControl
    {
        public Bricks()
        {
            InitializeComponent();
            DataContext = new NodeViewModel();
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point _startPoint = e.GetPosition(Board);

            if (sender is Button button)
            {
                var grid = button.Content as Grid;
                // Récupérez l'entité liée via le DataContext
                var entity = button.DataContext as Brick; // Remplacez YourEntityType par le type réel de vos entités
                if (entity != null)
                {
                    ((NodeViewModel)DataContext).StartDrag(_startPoint,entity, button);
                }
            }
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point _startPoint = e.GetPosition(Board);

            ((NodeViewModel)DataContext).ActualiseDrag(_startPoint, ActualWidth, ActualHeight);
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((NodeViewModel)DataContext).StopDrag();
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
                }
            }
        }
    }
}
