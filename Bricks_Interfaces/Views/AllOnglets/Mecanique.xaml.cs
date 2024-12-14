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

namespace Bricks_Interfaces.Views.AllOnglets
{
    /// <summary>
    /// Logique d'interaction pour Mecanique.xaml
    /// </summary>
    public partial class Mecanique : UserControl
    {
        public Mecanique()
        {
            InitializeComponent();

            ListMecanique.Children.Clear();

            string[] blocks = { "Mecanique 1", "Mecanique 2", "Mecanique 3" };

            foreach (var block in blocks)
            {
                // Créer un TextBlock dynamiquement
                TextBlock textBlock = new TextBlock
                {
                    Text = block,
                    Margin = new Thickness(5),
                    FontSize = 16
                };

                // Ajouter le TextBlock au WrapPanel
                ListMecanique.Children.Add(textBlock);
            }
        }
    }
}
