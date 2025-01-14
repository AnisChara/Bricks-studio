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

namespace Bricks_Interfaces
{
    /// <summary>
    /// Logique d'interaction pour DoubleClick.xaml
    /// </summary>
    public partial class DoubleClick : Window
    {
        public DoubleClick()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (double.TryParse(HeightTextBox.Text, out double height) &&
                double.TryParse(WidthTextBox.Text, out double width))
            {
               
                MessageBox.Show($"Dimensions sauvegardées : Hauteur = {height}, Largeur = {width}",
                                "Sauvegarde", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Veuillez entrer des valeurs numériques valides pour la hauteur et la largeur.",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            HeightTextBox.Text = string.Empty;
            WidthTextBox.Text = string.Empty;

            MessageBox.Show("Les champs Hauteur et Largeur ont été réinitialisés.",
                            "Réinitialisation", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

