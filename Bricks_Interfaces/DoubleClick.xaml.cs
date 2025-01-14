using Bricks_Interfaces.Models;
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
using Bricks_Interfaces.ViewModels;

namespace Bricks_Interfaces
{
    /// <summary>
    /// Logique d'interaction pour DoubleClick.xaml
    /// </summary>
    public partial class DoubleClick : Window
    {
        public DoubleClick(Entity entity)
        {
            InitializeComponent();
            this.DataContext = new EntityMenuViewModel(entity);
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ((EntityMenuViewModel)this.DataContext).SaveEntity();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ((EntityMenuViewModel)this.DataContext).DeleteEntity();
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ((EntityMenuViewModel)this.DataContext).CancelChange();
            this.Close();
        }


    }

}

