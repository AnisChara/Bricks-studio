using Bricks_Interfaces.Models;
using Bricks_Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace Bricks_Interfaces.ViewModels
{
    public class NodeViewModel : BaseNotifyPropertyChanged
    {
        public static MenuBrick BrickMenu = null;

        private string fuse_image = MainWindowViewModel.AssetsPath + "/lego_jaune.png";
        public Brick selectedBrick;
        public bool dragging = false;
        bool can_fuse = false;
        string fuse_direction = "";
        public Brick BrickToFuse;
        private double mouseX;
        private double mouseY;
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

        public void StartDrag(System.Windows.Point _startPoint,object parameter,Button button, System.Windows.Shapes.Rectangle rect)
        {
            selectedBrick = parameter as Brick;
            selectedButton = button;
            this.rect = rect;
            dragging = true;
            mouseX = _startPoint.X;
            mouseY = _startPoint.Y;
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
            double x = selectedBrick.x;
            double y = selectedBrick.y;
            double saveY = selectedBrick.y;
            double saveHeight = selectedBrick.height;
            int NodeIndex = 0;
            int NodeLength = 1;

            if (selectedBrick.NodeId != string.Empty)
            {
                NodeIndex = GetNodeIndex(selectedBrick);
                NodeLength = GetNodeLength(selectedBrick);
                //Debug = GetNodeIndex(selectedBrick).ToString();
                ResizeToNode(selectedBrick);
            }

            //Debug = width.ToString() + " " + height.ToString() + " " + selectedBrick.x.ToString() + " " + selectedBrick.y.ToString();

            //Debug = selectedBrick.x.ToString() + " " + selectedBrick.y.ToString() + " "+ selectedBrick.width.ToString() + " " + selectedBrick.height.ToString();

            var (direction, entity_collided) = selectedBrick.CheckAllCollision(Actions);
            if (direction == "none" && entity_collided == null) (direction, entity_collided) = selectedBrick.CheckAllCollision(Events);
            if (direction == "right") can_move_right = false;
            if (direction == "left" ) can_move_left = false;
            if (direction == "bottom" ) { can_move_bottom = false; may_fuse = true; fuse_direction = "bottom"; }
            if (direction == "top") {can_move_top = false; may_fuse = true; fuse_direction = "top"; }

            if (may_fuse) if (selectedBrick.NodeId == entity_collided.NodeId && selectedBrick.NodeId != string.Empty) may_fuse = false;


            if (may_fuse == true) 
            {
                //rect.Visibility = System.Windows.Visibility.Visible;
                BrickToFuse = entity_collided;
                can_fuse = true;

                if (selectedBrick.NodeId == string.Empty)
                {
                    selectedBrick.Image = fuse_image;
                }
                else if (fuse_direction == "bottom")
                {
                    double minY = selectedBrick.y;
                    selectedBrick.y = saveY;
                    Brick HigherAction = null;
                    Brick HigherEvent = null;
                    var node = Nodes.FirstOrDefault(obj => obj.id == selectedBrick.NodeId);
                    HigherAction = Actions.Where(obj => obj.NodeId == selectedBrick.NodeId).MaxBy(obj => obj.y) ?? selectedBrick;
                    HigherEvent = Events.Where(obj => obj.NodeId == selectedBrick.NodeId).MaxBy(obj => obj.y) ?? selectedBrick;
                    if (HigherAction.y > HigherEvent.y) HigherAction.Image = fuse_image;
                    else HigherEvent.Image = fuse_image;
                    selectedBrick.y = minY;
                }
                else if (fuse_direction == "top")
                {
                    double maxY = selectedBrick.y;
                    selectedBrick.y = saveY;
                    Brick LowerAction = null;
                    Brick LowerEvent = null;
                    var node = Nodes.FirstOrDefault(obj => obj.id == selectedBrick.NodeId);
                    LowerAction = Actions.Where(obj => obj.NodeId == selectedBrick.NodeId).MinBy(obj => obj.y) ?? selectedBrick;
                    LowerEvent = Events.Where(obj => obj.NodeId == selectedBrick.NodeId).MinBy(obj => obj.y) ?? selectedBrick;
                    if (LowerAction.y < LowerEvent.y) LowerAction.Image = fuse_image;
                    else LowerEvent.Image = fuse_image;
                    selectedBrick.y = maxY;
                }
            }
            else
            {
                ResetImage();
            }
          
            if (can_fuse == false) rect.Visibility = System.Windows.Visibility.Collapsed;

            if (selectedBrick.x <= 0) { can_move_left = false; x = 0; }
            if (selectedBrick.x >= width - selectedBrick.width - 100) { can_move_right = false; x = width - selectedBrick.width - 100; }  // 100 à cause des menus actions et events
            if (selectedBrick.y <= 0) { can_move_top = false; y = NodeIndex * saveHeight; }
            if (selectedBrick.y >= height - selectedBrick.height - 100) { can_move_bottom = false; y = height - ((NodeLength- NodeIndex) * saveHeight) - 100; }
            //Debug = height.ToString() + " " +y.ToString() + " " + saveHeight.ToString() + " " + (height - ((NodeLength - NodeIndex) * saveHeight) - 100).ToString();
            selectedBrick.y = saveY;
            selectedBrick.height = saveHeight;
           // Debug = e.X.ToString() + " " + mouseX.ToString() + " " + selectedBrick.x.ToString();
            if (e.X > mouseX && can_move_right) { x = selectedBrick.x + (e.X - mouseX); mouseX = e.X; };
            if (e.X < mouseX && can_move_left) { x = selectedBrick.x + (e.X - mouseX); mouseX = e.X; };
            if (e.Y < mouseY && can_move_top) { y = selectedBrick.y + (e.Y - mouseY); mouseY = e.Y; };
            if (e.Y > mouseY && can_move_bottom) { y = selectedBrick.y + (e.Y - mouseY); mouseY = e.Y; };


            move_brick(selectedBrick, x,y);

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
            int NodeIndex = 0;
            int NodeLength = 1;

            if (selectedBrick.NodeId != string.Empty)
            {
                NodeIndex = GetNodeIndex(selectedBrick);
                NodeLength = GetNodeLength(selectedBrick);
            }

            if (fuse_direction == "bottom")
            {
                move_brick(selectedBrick, BrickToFuse.x, BrickToFuse.y - selectedBrick.height* (NodeLength - NodeIndex));
            }
            else if (fuse_direction == "top")
            {
                move_brick(selectedBrick, BrickToFuse.x , BrickToFuse.y+BrickToFuse.height* (NodeIndex+1));
            }

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
            else if (selectedBrick.NodeId != string.Empty && BrickToFuse.NodeId == string.Empty)
            {
                var node = Nodes.FirstOrDefault(obj => obj.id == selectedBrick.NodeId);
                if (BrickToFuse is Models.Action) { node.Mecanique.Actions.Add((Models.Action)BrickToFuse); }
                else if (BrickToFuse is Models.Event) { node.Declencheur.Events.Add((Models.Event)BrickToFuse); }
                BrickToFuse.NodeId = node.id;
            }
            else if (BrickToFuse.NodeId != string.Empty && selectedBrick.NodeId == string.Empty)
            {
                var node = Nodes.FirstOrDefault(obj => obj.id == BrickToFuse.NodeId);
                if (selectedBrick is Models.Action) { node.Mecanique.Actions.Add((Models.Action)selectedBrick); }
                else if (selectedBrick is Models.Event) { node.Declencheur.Events.Add((Models.Event)selectedBrick); }
                selectedBrick.NodeId = node.id;
            }
            else if (BrickToFuse.NodeId != string.Empty && selectedBrick.NodeId != string.Empty)
            {
                var selectedNode = Nodes.FirstOrDefault(obj => obj.id == selectedBrick.NodeId);
                var BrickToFuseNode = Nodes.FirstOrDefault(obj => obj.id == BrickToFuse.NodeId);
                BrickToFuseNode.Declencheur.Events.AddRange(selectedNode.Declencheur.Events);
                BrickToFuseNode.Mecanique.Actions.AddRange(selectedNode.Mecanique.Actions);
                foreach (var item in Actions)
                {
                    if (item.NodeId == selectedNode.id) item.NodeId = BrickToFuseNode.id;
                }
                foreach (var item in Events)
                {
                    if (item.NodeId == selectedNode.id) item.NodeId = BrickToFuseNode.id;
                }
                Nodes.Remove(selectedNode);
            }

            ResetImage();
        }

        private void move_brick(Brick brick, double x, double y)
        {
            double dx = x - brick.x;
            double dy = y - brick.y;
            if (brick.NodeId == string.Empty)
            {
                brick.x = x;
                brick.y = y;
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

        private void ResizeToNode(Brick brick)
        {
            double lowerActionY = brick.y;
            double lowerEventY = brick.y;

            var node = Nodes.FirstOrDefault(obj => obj.id == brick.NodeId);
            lowerActionY = Actions.Where(obj => obj.NodeId == brick.NodeId).MinBy(obj => obj.y)?.y ?? lowerActionY;
            if (Events.Any()) lowerEventY = Events.Where(obj => obj.NodeId == brick.NodeId).MinBy(obj => obj.y)?.y ?? lowerEventY;
            brick.y = Math.Min(lowerActionY, lowerEventY);
            brick.height = (node.Mecanique.Actions.Count() + node.Declencheur.Events.Count()) * 35;
        }

        private int GetNodeIndex(Brick brick)
        {
            int index = 0;
            foreach(var Act in Actions)
            {
                if (Act.NodeId != brick.NodeId) continue;
                if (Act.y < brick.y) index++;
            }
            foreach (var Act in Events)
            {
                if (Act.NodeId != brick.NodeId) continue;
                if (Act.y < brick.y) index++;
            }
            return index;
        }
        private int GetNodeLength(Brick brick)
        {
            var node = Nodes.FirstOrDefault(obj => obj.id == brick.NodeId);
            return node.Declencheur.Events.Count() + node.Mecanique.Actions.Count();
        }

        private void ResetImage()
        {
            foreach (var item in Actions)
            {
                item.Image = MainWindowViewModel.AssetsPath + "/lego_rouge.png";
            }
            foreach (var item in Events)
            {
                item.Image = MainWindowViewModel.AssetsPath + "/lego_bleu.png";
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
