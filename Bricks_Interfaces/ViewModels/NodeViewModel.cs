using Bricks_Interfaces.Models;
using Bricks_Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Bricks_Interfaces.ViewModels
{
    public class NodeViewModel : BaseNotifyPropertyChanged
    {
        public static MenuBrick BrickMenu = null;

        public Brick selectedBrick;
        public bool dragging = false;
        bool can_fuse = false;
        public Mecanique MecaniqueToFuse; 
        public Declencheur DeclencheurToFuse;
        private FileSystemWatcher MecaWatcher;
        private FileSystemWatcher DeclWatcher;
        private FileSystemWatcher NodeWatcher;


        private Button selectedButton;
        public Button SelectedButton
        {
            get => selectedButton;
            set
            {
                selectedButton = value;
                OnPropertyChanged(nameof(SelectedButton));
            }
        }

        private System.Windows.Shapes.Rectangle _visibility;
        public System.Windows.Shapes.Rectangle rect
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(rect));
            }
        }
        private ObservableCollection<Node> _nodes { get; set; }
        public ObservableCollection<Node> Nodes { get => _nodes;
            set
            {
                _nodes = value;
                OnPropertyChanged(nameof(Nodes));
            }
        }
        private ObservableCollection<Declencheur> declencheurs { get; set; }
        public ObservableCollection<Declencheur> Declencheurs
        {
            get => declencheurs;
            set
            {
                declencheurs = value;
                OnPropertyChanged(nameof(Declencheurs));
            }
        }
        private ObservableCollection<Models.Mecanique> mecaniques { get; set; }
        public ObservableCollection<Models.Mecanique> Mecaniques
        {
            get => mecaniques;
            set
            {
                mecaniques = value;
                OnPropertyChanged(nameof(Mecaniques));
            }
        }


        public NodeViewModel() {

            InitializeFileWatcher("Mecaniques");
            InitializeFileWatcher("Declencheurs");
            Nodes = Node.GetNodes();
            Mecaniques = Models.Mecanique.GetMecaniques();
            Declencheurs = Declencheur.GetDeclencheurs();
        }

        public void StartDrag(object parameter,Button button, System.Windows.Shapes.Rectangle rect)
        {
            selectedBrick = parameter as Brick;
            selectedButton = button;
            this.rect = rect;
            dragging = true;
        }

        public void ActualiseDrag(System.Windows.Point e, double width, double height)
        {
            if (!dragging) return;

            can_fuse = false;
            bool can_move_left = true;
            bool can_move_right = true;
            bool can_move_top = true;
            bool can_move_bottom = true;
            bool may_fuse = false;

            var (direction, entity_collided) = selectedBrick.CheckAllCollision(Nodes);
            if (direction == "none" && entity_collided == null) (direction, entity_collided) = selectedBrick.CheckAllCollision(Mecaniques);
            if (direction == "none" && entity_collided == null) (direction, entity_collided) = selectedBrick.CheckAllCollision(Declencheurs);

            if (direction == "right" && e.X >= selectedBrick.x) can_move_right = false;
            if (direction == "left" && e.X < entity_collided.x + entity_collided.width) can_move_left = false;
            if (direction == "bottom" && e.Y > selectedBrick.y) { can_move_bottom = false; may_fuse = true; }
            if (direction == "top" && e.Y < entity_collided.y + entity_collided.height) {can_move_top = false; may_fuse = true;}

            if (may_fuse == true && selectedBrick.Image == "pack://application:,,,/Assets/lego_rouge.png" && entity_collided.Image == "pack://application:,,,/Assets/lego_bleu.png") //temp
            { 
                rect.Visibility = System.Windows.Visibility.Visible;
                MecaniqueToFuse = selectedBrick as Mecanique;
                DeclencheurToFuse = entity_collided as Declencheur;
                can_fuse = true;
            }
            if (may_fuse == true && selectedBrick.Image == "pack://application:,,,/Assets/lego_bleu.png" && entity_collided.Image == "pack://application:,,,/Assets/lego_rouge.png")
            {
                rect.Visibility = System.Windows.Visibility.Visible;
                DeclencheurToFuse = selectedBrick as Declencheur;
                MecaniqueToFuse = entity_collided as Mecanique;
                can_fuse = true;
            }
            if (can_fuse == false) rect.Visibility = System.Windows.Visibility.Collapsed;

            if (e.X < 0) can_move_left = false;
            if (e.X >= width - selectedBrick.width) can_move_right = false;
            if (e.Y < 0) can_move_top = false;
            if (e.Y >= height - selectedBrick.height) can_move_bottom = false;

            if (e.X > selectedBrick.x && can_move_right) selectedBrick.x = e.X;
            if (e.X < selectedBrick.x && can_move_left) selectedBrick.x = e.X;
            if (e.Y < selectedBrick.y && can_move_top) selectedBrick.y = e.Y;
            if (e.Y > selectedBrick.y && can_move_bottom) selectedBrick.y = e.Y;


            selectedBrick.margin = new Thickness(selectedBrick.x, selectedBrick.y, 0, 0);
        }

        public void StopDrag()
        {
            dragging = false;
            if (can_fuse) Fuse(DeclencheurToFuse,MecaniqueToFuse);
            if (selectedBrick is Node) Node.SaveNodes(Nodes);
            else
            {
                Declencheur.SaveDeclencheurs(Declencheurs);
                Models.Mecanique.SaveMecaniques(Mecaniques);
                Node.SaveNodes(Nodes);
            }
        }



        public void OpenBrickMenu(Brick brick)
        {
            if (BrickMenu != null)
            {
                MessageBox.Show("Un menu Brick est deja ouvert veuillez d'abord la fermer.");
                return;
            }

            BrickMenu = new MenuBrick(brick);

            // Synchroniser les dimensions et l'état de la fenêtre
            BrickMenu.Width = 200;
            BrickMenu.Height = 300;

            // Synchroniser la position de la fenêtre
            BrickMenu.Left = 700;
            BrickMenu.Top = 200;

            BrickMenu.Show();
        }

        private void Fuse(Declencheur Declencheur, Mecanique Mecanique)
        {
            var new_node = new Node(Mecanique,Declencheur,Mecanique.Name,Mecanique.x,Mecanique.y);
            Nodes.Add(new_node);
            Declencheurs.Remove(Declencheur);
            Mecaniques.Remove(Mecanique);
        }

        private void InitializeFileWatcher(string target)
        {
            MecaWatcher = new FileSystemWatcher
            {
                Path = "../../../",
                Filter = "Mecaniques.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            MecaWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            MecaWatcher.Changed += (sender, e) =>
            {
                Mecaniques = Models.Mecanique.GetMecaniques();
            };

            MecaWatcher.EnableRaisingEvents = true; // Active la surveillance

            DeclWatcher = new FileSystemWatcher
            {
                Path = "../../../",
                Filter = "Declencheurs.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            DeclWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            DeclWatcher.Changed += (sender, e) =>
            {
                Declencheurs = Declencheur.GetDeclencheurs();
            };

            DeclWatcher.EnableRaisingEvents = true; // Active la surveillance

            NodeWatcher = new FileSystemWatcher
            {
                Path = "../../../",
                Filter = "Nodes.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            NodeWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            NodeWatcher.Changed += (sender, e) =>
                {
                    Nodes = Node.GetNodes();
                };

            NodeWatcher.EnableRaisingEvents = true; // Active la surveillance
        }

    }
}
