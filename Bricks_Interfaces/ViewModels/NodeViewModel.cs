using Bricks_Interfaces.Models;
using Bricks_Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        string fuse_direction = "";
        public Brick BrickToFuse; 
        private FileSystemWatcher MecaWatcher;
        private FileSystemWatcher DeclWatcher;
        private FileSystemWatcher NodeWatcher;

        private string debug;
        public string Debug
        {
            get => debug;
            set
            {
                debug = value;
                OnPropertyChanged(nameof(Debug));
            }
        }


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
        private ObservableCollection<Event> events { get; set; }
        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }
        private ObservableCollection<Models.Action> actions { get; set; }
        public ObservableCollection<Models.Action> Actions
        {
            get => actions;
            set
            {
                actions = value;
                OnPropertyChanged(nameof(Actions));
            }
        }


        public NodeViewModel() {

            InitializeFileWatcher();
            Nodes = Node.GetNodes();
            Actions = Models.Action.GetSavedActions();
            Events = Event.GetSavedEvents();
            Debug = "TEST";
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
            double dx = 0;
            double dy = 0;
            Node node = null;
            double saveY = selectedBrick.y;
            double saveHeight = selectedBrick.height;
            double lowerActionY = selectedBrick.y;
            double lowerEventY = selectedBrick.y;

            if (selectedBrick.NodeId != string.Empty)
            {
                node = Nodes.FirstOrDefault(obj => obj.id == selectedBrick.NodeId);
                lowerActionY = Actions.Where(obj=> obj.NodeId == selectedBrick.NodeId ).MinBy(obj => obj.y)?.y ?? selectedBrick.y; 
                if (Events.Any())lowerEventY = Events.Where(obj => obj.NodeId == selectedBrick.NodeId).MinBy(obj => obj.y)?.y ?? selectedBrick.y; 
                selectedBrick.y = Math.Min(lowerActionY, lowerEventY); 
                selectedBrick.height = (node.Mecanique.Actions.Count() + node.Declencheur.Events.Count()) * 35;
            }

            Debug = height.ToString() + " " + selectedBrick.y.ToString();

            var (direction, entity_collided) = selectedBrick.CheckAllCollision(Actions, ref debug);
            OnPropertyChanged(nameof(Debug));
            if (direction == "none" && entity_collided == null) (direction, entity_collided) = selectedBrick.CheckAllCollision(Events, ref debug);
            if (direction == "right" && e.X >= selectedBrick.x) can_move_right = false;
            if (direction == "left" && e.X < entity_collided.x + entity_collided.width) can_move_left = false;
            if (direction == "bottom" && e.Y > selectedBrick.y) { can_move_bottom = false; may_fuse = true; fuse_direction = "bottom"; }
            if (direction == "top" && e.Y < entity_collided.y + entity_collided.height) {can_move_top = false; may_fuse = true; fuse_direction = "top"; }

            if (may_fuse) if (selectedBrick.NodeId == entity_collided.NodeId && selectedBrick.NodeId != string.Empty) may_fuse = false;


            if (may_fuse == true) //temp
            {
                rect.Visibility = System.Windows.Visibility.Visible;
                BrickToFuse = entity_collided;
                can_fuse = true;
            }
          
            if (can_fuse == false) rect.Visibility = System.Windows.Visibility.Collapsed;

            if (e.X < 0) can_move_left = false;
            if (e.X >= width - selectedBrick.width*2) can_move_right = false;
            if (e.Y < 0) can_move_top = false;
            if (e.Y >= height - selectedBrick.height*2) can_move_bottom = false;

            if (e.X > selectedBrick.x && can_move_right) dx = e.X - selectedBrick.x ;
            if (e.X < selectedBrick.x && can_move_left) dx = e.X - selectedBrick.x;
            if (e.Y < selectedBrick.y && can_move_top) dy = e.Y - saveY;
            if (e.Y > selectedBrick.y && can_move_bottom) dy = e.Y - saveY;

            selectedBrick.y = saveY;
            selectedBrick.height = saveHeight;
            move_brick(selectedBrick, dx,dy);

        }

        public void StopDrag()
        {
            dragging = false;
            if (can_fuse) Fuse(BrickToFuse);
            Node.SaveNodes(Nodes);
            Models.Action.SaveActions(Actions);
            Models.Event.SaveEvents(Events);
        }

        private void Fuse(Brick BrickToFuse)
        {
            if (selectedBrick.NodeId == string.Empty && BrickToFuse.NodeId == string.Empty)
            {
                var new_node = new Node(
                    new Mecanique([], ""),
                    new Declencheur([], ""),
                    "",
                    0,
                    0
                );
                Nodes.Add(new_node);

                if (selectedBrick is Models.Action) new_node.Mecanique.Actions.Add((Models.Action)selectedBrick);
                else new_node.Declencheur.Events.Add((Event)selectedBrick);

                if (BrickToFuse is Models.Action) new_node.Mecanique.Actions.Add((Models.Action)BrickToFuse);
                else new_node.Declencheur.Events.Add((Event)BrickToFuse);

                selectedBrick.NodeId = new_node.id;
                BrickToFuse.NodeId = new_node.id;
            }
            if (selectedBrick.NodeId != string.Empty && BrickToFuse.NodeId == string.Empty)
            {

            }
            if (BrickToFuse.NodeId != string.Empty && selectedBrick.NodeId == string.Empty)
            {

            }

            if (fuse_direction == "bottom")
            {
                selectedBrick.x = BrickToFuse.x;
                selectedBrick.y = BrickToFuse.y - selectedBrick.height;
            }
            if (fuse_direction == "top")
            {
                selectedBrick.x = BrickToFuse.x;
                selectedBrick.y = BrickToFuse.y + BrickToFuse.height;
            }

            selectedBrick.margin = new Thickness(selectedBrick.x, selectedBrick.y, 0, 0);

        }

        private void move_brick(Brick brick, double dx, double dy)
        {
            if (brick.NodeId == string.Empty)
            {
                brick.x += dx;
                brick.y += dy;
                brick.margin = new Thickness(brick.x, brick.y, 0, 0);
                return;
            }

            Node node = null;

            foreach (var item in Nodes)
            {
                if(item.id == brick.NodeId) { node = item; break; }
            }

            foreach (var item in Actions)
            {
                if (item.NodeId == brick.NodeId)
                {
                    item.x += dx; item.y += dy;
                    item.margin = new Thickness(item.x, item.y, 0, 0);
                }
            }
            foreach (var item in Events)
            {
                if (item.NodeId == brick.NodeId)
                {
                    item.x += dx; item.y += dy;
                    item.margin = new Thickness(item.x, item.y, 0, 0);
                }
            }
        }

        private void InitializeFileWatcher()
        {
            MecaWatcher = new FileSystemWatcher
            {
                Path = MainWindowViewModel.ProjectPath,
                Filter = "Actions.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            MecaWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            MecaWatcher.Changed += (sender, e) =>
            {
                Actions = Models.Action.GetSavedActions();
            };

            MecaWatcher.EnableRaisingEvents = true; // Active la surveillance

            DeclWatcher = new FileSystemWatcher
            {
                Path = MainWindowViewModel.ProjectPath,
                Filter = "Events.json",
                NotifyFilter = NotifyFilters.LastWrite
            };


            DeclWatcher.InternalBufferSize = 65536; // Taille du buffer en octets (64 Ko)

            DeclWatcher.Changed += (sender, e) =>
            {
                Events = Event.GetSavedEvents();
            };

            DeclWatcher.EnableRaisingEvents = true; // Active la surveillance

            NodeWatcher = new FileSystemWatcher
            {
                Path = MainWindowViewModel.ProjectPath,
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
