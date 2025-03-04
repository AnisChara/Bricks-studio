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
    /// Logique d'interaction pour Events.xaml
    /// </summary>
    public partial class Events : UserControl
    {
        public Events()
        {
            InitializeComponent();
            this.DataContext = new DeclencheurViewModel();
        }
    }
}
