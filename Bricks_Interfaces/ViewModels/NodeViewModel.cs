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
        private string action_image = MainWindowViewModel.AssetsPath + "/lego_rouge.png";
        private string event_image = MainWindowViewModel.AssetsPath + "/lego_bleu.png";
        private string fuse_top_image = MainWindowViewModel.AssetsPath + "/lego_jaune_top.png";
        private string fuse_bottom_image = MainWindowViewModel.AssetsPath + "/lego_jaune_bottom.png";
        private string action_bottom_image = MainWindowViewModel.AssetsPath + "/lego_rouge_bottom.png";
        private string action_top_image = MainWindowViewModel.AssetsPath + "/lego_rouge_top.png";
        private string action_mid_image = MainWindowViewModel.AssetsPath + "/lego_rouge_mid.png";
        private string event_bottom_image = MainWindowViewModel.AssetsPath + "/lego_bleu_bottom.png";
        private string event_top_image = MainWindowViewModel.AssetsPath + "/lego_bleu_top.png";
        private string event_mid_image = MainWindowViewModel.AssetsPath + "/lego_bleu_mid.png";

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
            ResetImage();
        }

        public void StartDrag(System.Windows.Point _startPoint,object parameter,Button button)
        {
            selectedBrick = parameter as Brick;
            selectedButton = button;
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
            Brick entity_collided = null;

            if (selectedBrick.NodeId != string.Empty)
            {
                NodeIndex = GetNodeIndex(selectedBrick);
                NodeLength = GetNodeLength(selectedBrick);
                //Debug = GetNodeIndex(selectedBrick).ToString();
                ResizeToNode(selectedBrick);
            }

            //Debug = width.ToString() + " " + height.ToString() + " " + selectedBrick.x.ToString() + " " + selectedBrick.y.ToString();

            //Debug = selectedBrick.x.ToString() + " " + selectedBrick.y.ToString() + " "+ selectedBrick.width.ToString() + " " + selectedBrick.height.ToString();

            var (directions, entities_collided) = selectedBrick.CheckAllCollision(Actions);
            var (directions2, entities_collided2) = selectedBrick.CheckAllCollision(Events);
            directions.AddRange(directions2);
            Debug = directions.Contains("bottom").ToString();
            entities_collided.AddRange(entities_collided2);
            if (directions.Contains("right")) can_move_right = false;
            if (directions.Contains("left")) can_move_left = false;
            if (directions.Contains("bottom")) { can_move_bottom = false; may_fuse = true; fuse_direction = "bottom"; }
            if (directions.Contains("top")) {can_move_top = false; may_fuse = true; fuse_direction = "top"; }

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

            if (may_fuse)
            {
                int entity_collided_index = directions.IndexOf(fuse_direction);
                entity_collided = entities_collided[entity_collided_index];
                BrickToFuse = entity_collided;
                can_fuse = true;
                ResetImage();

                if (selectedBrick.NodeId == string.Empty)
                {
                    selectedBrick.Image = fuse_image;
                }
                else if (fuse_direction == "bottom")
                {
                    var node = Nodes.FirstOrDefault(obj => obj.id == selectedBrick.NodeId);
                    var BottomBrick = GetBottomBrick(node);
                    BottomBrick.Image = fuse_bottom_image;
                }
                else if (fuse_direction == "top")
                {
                    var node = Nodes.FirstOrDefault(obj => obj.id == selectedBrick.NodeId);
                    var TopBrick = GetTopBrick(node);
                    TopBrick.Image = fuse_top_image;
                }

                if (entity_collided.NodeId == string.Empty)
                {
                    entity_collided.Image = fuse_image;
                }
                else if (fuse_direction == "bottom")
                {
                    var node = Nodes.FirstOrDefault(obj => obj.id == entity_collided.NodeId);
                    var TopBrick = GetTopBrick(node);
                    TopBrick.Image = fuse_top_image;
                }
                else if (fuse_direction == "top")
                {
                    var node = Nodes.FirstOrDefault(obj => obj.id == entity_collided.NodeId);
                    var BottomBrick = GetBottomBrick(node);
                    BottomBrick.Image = fuse_bottom_image;
                }
            }
            else
            {
                ResetImage();
            }

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
            var node = Nodes.FirstOrDefault(obj => obj.id == brick.NodeId);
            var TopBrick = GetTopBrick(node);
            brick.y = TopBrick.y;
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
            List<string> NodesDone = new List<string>();
            List<string> BorderBricks = new List<string>();

            foreach (var item in Actions)
            {
                if (item.NodeId == string.Empty) { item.Image = action_image; continue; }
                if (BorderBricks.Contains(item.id)) continue;
                if (NodesDone.Contains(item.NodeId)) { item.Image = action_mid_image; continue; }

                var node = Nodes.FirstOrDefault(obj => obj.id == item.NodeId);
                var TopBrick = GetTopBrick(node);
                var BottomBrick = GetBottomBrick(node);
                if (TopBrick is Models.Action) TopBrick.Image = action_top_image; else TopBrick.Image = event_top_image;
                if (BottomBrick is Models.Action) BottomBrick.Image = action_bottom_image; else BottomBrick.Image = event_bottom_image;
                NodesDone.Add(node.id);
                BorderBricks.Add(TopBrick.id);
                BorderBricks.Add(BottomBrick.id);
                if (item != BottomBrick && item != TopBrick) item.Image = action_mid_image;
            }
            foreach (var item in Events)
            {
                if (item.NodeId == string.Empty) { item.Image = event_image; continue; }
                if (BorderBricks.Contains(item.id)) continue;
                if (NodesDone.Contains(item.NodeId)) { item.Image = event_mid_image; continue; }
                var node = Nodes.FirstOrDefault(obj => obj.id == item.NodeId);
                var TopBrick = GetTopBrick(node);
                var BottomBrick = GetBottomBrick(node);
                TopBrick.Image = event_top_image;
                BottomBrick.Image = event_bottom_image;
                NodesDone.Add(node.id);
                BorderBricks.Add(TopBrick.id);
                BorderBricks.Add(BottomBrick.id);
                if (item != BottomBrick && item != TopBrick) item.Image = event_mid_image;
            }
        }

        private Brick GetTopBrick(Node node)
        {
            Brick higherAction = null;
            Brick higherEvent = null;
            higherAction = Actions.Where(obj => obj.NodeId == node.id).MinBy(obj => obj.y) ?? higherAction;
            higherEvent = Events.Where(obj => obj.NodeId == node.id).MinBy(obj => obj.y) ?? higherEvent;
            if (higherAction == null) return higherEvent;
            if (higherEvent == null) return higherAction;
            if (higherAction.y < higherEvent.y) return higherAction;
            else return higherEvent;
        }

        private Brick GetBottomBrick(Node node)
        {
            Brick lowerAction = null;
            Brick lowerEvent = null;
            lowerAction = Actions.Where(obj => obj.NodeId == node.id).MaxBy(obj => obj.y) ?? lowerAction;
            lowerEvent = Events.Where(obj => obj.NodeId == node.id).MaxBy(obj => obj.y) ?? lowerEvent;
            if (lowerAction == null) return lowerEvent;
            if (lowerEvent == null) return lowerAction;
            if (lowerAction.y > lowerEvent.y) return lowerAction;
            else return lowerEvent;
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
