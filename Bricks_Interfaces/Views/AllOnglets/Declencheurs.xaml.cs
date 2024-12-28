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

            ListDeclencheur.Children.Clear();
            Event[] events = [ new Event("nam","fun","description")];

            Create_events_list(events);

        }

        private void Add_event_box_on_board(Panel parentPanel, double width = 200, double height = 30, string text = "Nouvelle TextBox", Thickness? margin = null)
        {

            TextBlock nouvelleTextBox = new TextBlock
            {
                Width = width,
                Height = height,
                Margin = margin ?? new Thickness(5),
                Text = text,
                Background = new SolidColorBrush(Colors.Red)
            };


            parentPanel.Children.Add(nouvelleTextBox);
        }

        public void Create_events_list(Event[] events)
        {
            foreach (Event evenement in events)
            {
                // Créer un TextBlock dynamiquement
                Button textBlock = new Button
                {
                    Content = evenement.name,
                    Margin = new Thickness(5),
                    FontSize = 16,
                    Command = new RelayCommand((o) =>
                    {
                        Add_event_box_on_board(board , 50, 20, evenement.name);
                    })

                };

                // Ajouter le TextBlock au WrapPanel
                ListDeclencheur.Children.Add(textBlock);
            }
        }
    }
}
