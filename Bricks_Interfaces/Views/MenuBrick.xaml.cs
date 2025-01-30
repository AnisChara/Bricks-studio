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
using System.Windows.Shapes;
using Bricks_Interfaces.Models;
using Bricks_Interfaces.ViewModels;

namespace Bricks_Interfaces.Views
{
    /// <summary>
    /// Logique d'interaction pour MenuBrick.xaml
    /// </summary>
    public partial class MenuBrick : Window
    {
        public MenuBrick(Brick brick)
        {
            InitializeComponent();
            this.DataContext = new MenuBrickViewModel(brick);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ((MenuBrickViewModel)this.DataContext).DeleteNode();
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ((MenuBrickViewModel)this.DataContext).CancelChange();
            this.Close();
        }
        private void SplitButton_Click(object sender, RoutedEventArgs e)
        {
            ((MenuBrickViewModel)this.DataContext).SplitNode();
            this.Close();
        }
    }
}

