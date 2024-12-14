using System.Configuration;
using System.Data;
using System.Windows;

namespace Bricks_Interfaces
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Ouvrir la fenêtre d'accueil
            var indexWindow = new Index();
            indexWindow.Show();
        }
    }

}
