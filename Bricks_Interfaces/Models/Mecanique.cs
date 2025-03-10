﻿using Bricks_Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Bricks_Interfaces.Models
{
    public class Mecanique : Brick
    {

        private List<Action> _actions;
        public List<Action> Actions
        {
            get => _actions;
            set
            {
                if (_actions != value)
                {
                    _actions = value;
                    OnPropertyChanged(nameof(Actions));
                }
            }
        }

        public Mecanique(List<Action> actions, string name, double x = 0, double y = 0, double width = 70, double height= 35) : base(name, x, y, width, height)
        {
            this.Actions = actions;
            Image = MainWindowViewModel.formattedPath+"/../../../Assets/lego_rouge.png";
            this.x = x;
            this.y = y;
            width = 70;
            height = 35;
            this.margin = new Thickness(x, y, 0, 0);
        }

        public static ObservableCollection<Mecanique> GetMecaniques()
        {
            ObservableCollection<Mecanique> Nodes = new ObservableCollection<Mecanique>();
            string json;
            bool succes = false;

            while (!succes)
            {
                try
                {
                    json = System.IO.File.ReadAllText(MainWindowViewModel.MecaniquesPath);
                    Nodes = JsonSerializer.Deserialize<ObservableCollection<Mecanique>>(json);
                    succes = true;

                }
                catch (Exception e) { }
            }

            return Nodes;
        }


        public static void SaveMecaniques(ObservableCollection<Models.Mecanique> Levels)
        {
            bool succes = false;

            while (!succes)
            {
                try
                {
                    string json = JsonSerializer.Serialize(Levels, new JsonSerializerOptions { WriteIndented = true });
                    System.IO.File.WriteAllText(MainWindowViewModel.MecaniquesPath, json);
                    succes = true;

                }
                catch (Exception e) { }
            }
        }
    }

}
