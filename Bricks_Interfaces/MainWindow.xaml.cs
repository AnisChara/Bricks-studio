using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bricks_Interfaces.ViewModels;
using Bricks_Interfaces.Models;

namespace Bricks_Interfaces
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string project_path)
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(project_path);
        }



        
    }
}