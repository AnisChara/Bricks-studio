using Bricks_Interfaces.Models;
using Bricks_Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Bricks_Interfaces.Views.AllOnglets
{
    /// <summary>
    /// Logique d'interaction pour Declencheurs.xaml
    /// </summary>
    public partial class Declencheurs : UserControl
    {
        public Declencheurs()
        {
            InitializeComponent();
            this.DataContext = new DeclencheurViewModel();
        }
    }
}