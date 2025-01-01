﻿using System;
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
using Bricks_Interfaces.ViewModels;

namespace Bricks_Interfaces.Views.AllOnglets
{
    /// <summary>
    /// Logique d'interaction pour Nodes.xaml
    /// </summary>
    public partial class Nodes : UserControl
    {
        public Nodes()
        {
            InitializeComponent();
            this.DataContext = new NodeViewModel();
        }
    }
}
