using Bricks_Interfaces.Models;
using Bricks_Interfaces.Views.AllOnglets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bricks_Interfaces.ViewModels
{
    public class MenuBrickViewModel : BaseNotifyPropertyChanged
    {
        public Visibility SplitVisibility { get; set; }
        public Visibility ActionVisibility { get; set; }
        public Visibility EventVisibility { get; set; }

        private Brick _brick;
        public Brick Brick
        {
            get => _brick;
            set
            {
                _brick = value;
                OnPropertyChanged(nameof(Brick));
            }
        }

        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<Models.Action> Actions { get; set; }

        public MenuBrickViewModel(Brick brick)
        { 
            Brick = brick;
            if (Brick is Node)
            {
                SplitVisibility = Visibility.Visible;
                ActionVisibility = Visibility.Collapsed;
                EventVisibility = Visibility.Collapsed;
            }
            else if (Brick is Declencheur Decl)
            {
                Events = new ObservableCollection<Event>(Decl.Events);
                SplitVisibility = Visibility.Collapsed;
                ActionVisibility = Visibility.Collapsed;
                EventVisibility = Visibility.Visible;
            }
            else if (Brick is Models.Mecanique Meca)
            {
                Actions = new ObservableCollection<Models.Action>(Meca.Actions);
                SplitVisibility = Visibility.Collapsed;
                ActionVisibility = Visibility.Visible;
                EventVisibility = Visibility.Collapsed;
            }
        }

        public void DeleteNode()
        {
            if (Brick == null) return;

            if (Brick is Node)
            {
                var Nodes = Node.GetNodes();
                Nodes.RemoveAt(Nodes.IndexOf(Nodes.Where(e => e.id == Brick.id).FirstOrDefault()));
                Node.SaveNodes(Nodes);
            }
            if (Brick is Models.Mecanique)
            {
                var Mecaniques = Models.Mecanique.GetMecaniques();
                Mecaniques.RemoveAt(Mecaniques.IndexOf(Mecaniques.Where(e => e.id == Brick.id).FirstOrDefault()));
                Models.Mecanique.SaveMecaniques(Mecaniques);
            }
            if (Brick is Declencheur)
            {
                var Declencheurs = Declencheur.GetDeclencheurs();
                Declencheurs.RemoveAt(Declencheurs.IndexOf(Declencheurs.Where(e => e.id == Brick.id).FirstOrDefault()));
                Declencheur.SaveDeclencheurs(Declencheurs);
            }

            NodeViewModel.BrickMenu = null;
        }
        public void CancelChange() {
            NodeViewModel.BrickMenu = null;

        }

        public void SplitNode()
        {
            if (Brick is Node node)
            {
                var Declencheurs = Declencheur.GetDeclencheurs();
                var Mecaniques = Models.Mecanique.GetMecaniques();
                var Nodes = Node.GetNodes();
                Declencheurs.Add(node.Declencheur);
                Mecaniques.Add(node.Mecanique);
                Nodes.RemoveAt(Nodes.IndexOf(Nodes.Where(e => e.id == Brick.id).FirstOrDefault()));
                Declencheur.SaveDeclencheurs(Declencheurs);
                Models.Mecanique.SaveMecaniques(Mecaniques);
                Node.SaveNodes(Nodes);
                NodeViewModel.BrickMenu = null;
            }

        }


    }
}
