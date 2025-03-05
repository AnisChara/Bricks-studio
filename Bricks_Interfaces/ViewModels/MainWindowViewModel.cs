using Bricks_Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bricks_Interfaces.ViewModels
{
    public class MainWindowViewModel : BaseNotifyPropertyChanged
    {
        public ICommand CompileCommand { get; set; }

        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string formattedPath = currentDirectory.Replace("\\", "/");

        public static string PythonScriptPath = "";
        public static string PythonScriptPathOutput = "";
        public static string ProjectPath = "";
        public static string AssetsPath = "";
        public static string NodesPath = "";
        public static string MecaniquesPath = "";
        public static string DeclencheursPath = "";
        public static string ActionsPath = "";
        public static string EventsPath = "";
        public static string EntityPath = "";



        private bool _gravity { get; set; } = true;
        public bool Gravity
        {
            get => _gravity;
            set
            {
                _gravity = value;
                OnPropertyChanged(nameof(Gravity));
            }
        }



        public MainWindowViewModel(string project_path)
        {

            CompileCommand = new RelayCommand(Compile);
            ProjectPath = project_path.Replace("\\", "/");
            NodesPath = project_path + "/Nodes.json";
            ActionsPath = project_path + "/Actions.json";
            EventsPath = project_path + "/Events.json";
            EntityPath = project_path + "/Entity.json";
            AssetsPath = project_path + "/Assets";
            PythonScriptPath = project_path + "/python/index.py";
            PythonScriptPathOutput = project_path + "/python";
        }

        public void Compile(object parameter)
        {
            // Création du script Python
            string json = System.IO.File.ReadAllText(NodesPath);

            

            string text = "import pygame\r\nfrom Pawn_class import Pawn\r\nfrom Game_class import Game\r\nfrom Gravity_class import Gravity\r\nfrom Jump_class import Jump\r\nfrom Structure_class import Structure\r\nfrom Entity_class import Entity\r\nfrom Projectile_class import Projectile\r\nfrom OffensiveStructure_class import Offensive_Structure\r\nfrom OffensiveEntity_class import Offensive_Entity\r\nfrom loading_function import instance_entities\r\n\r\n# pygame setup\r\npygame.init()\r\nGame.directory = \""+ProjectPath+"\"\r\nlevel = 0\r\n\r\ndef next_level(Player,level):\r\n    level = level +1\r\n    Game.clean_entities()\r\n    if level == level_max :return Player,level\r\n    Player = instance_entities(entities, level)\r\n    return Player,level\r\n\r\nGameBackground = pygame.image.load(Game.directory+\"/Assets/Fond.jpg\")\r\nGameBackground = pygame.transform.scale(GameBackground,(1280,720))\r\n\r\nVictoryScreen = pygame.image.load(Game.directory+\"/Assets/Victoire.png\")\r\nVictoryScreen = pygame.transform.scale(VictoryScreen,(1280,720))\r\n\r\nleft_border = Structure(\"left_border\",-2147483646,-1,2147483646,2147483646,0,True,\"square\",0,False)\r\nright_border = Structure(\"right_border\",Game.screen_width+1,-1,2147483646, 2147483646,0,True,\"square\",0,False)\r\nup_border = Structure(\"up_border\",-1,-2147483646,2147483646, 2147483646,0,True,\"square\",0,False)\r\ndown_border = Structure(\"down_border\",-1,Game.screen_height+1,2147483646, 2147483646,0,True,\"square\",0,False)\r\n\r\nGame.entities.append(left_border)\r\nGame.entities.append(right_border)\r\nGame.entities.append(up_border)\r\nGame.entities.append(down_border)\r\nentities = Game.load_entities(Game.directory+\"/Entity.json\")\r\nlevel_max = len(entities)\r\nGravity.is_gravity =" + Gravity.ToString() +"\r\n\r\nPlayer = instance_entities(entities,level)\r\nbackground = GameBackground\r\n\r\nwhile Game.running:\r\n    \r\n    if (level == level_max):\r\n        background = VictoryScreen\r\n        if Game.keys.ESPACE == True:\r\n            background = GameBackground\r\n            level = 0\r\n            Player = instance_entities(entities, level)\r\n\r\n\r\n    Game.screen.blit(background, (0,0))\r\n    Game.render()\r\n    Game.reset_keys()\r\n\r\n    for event in pygame.event.get():\r\n        if event.type == pygame.QUIT:\r\n            Game.running = False\r\n        if event.type == pygame.KEYDOWN:\r\n            Game.keydown_handle(event)\r\n            \r\n\r\n    keys = pygame.key.get_pressed()\r\n";
            string end = "    if Player.finish() == True:\r\n        if level < level_max:\r\n            Player,level = next_level(Player,level)\r\n\r\n\r\n    Jump.handle_jump()\r\n    Gravity.fall()\r\n    Projectile.handle_projectile()\r\n    Game.check_alive()\r\n    Offensive_Structure.handle_offensive_structures()\r\n    Offensive_Entity.handle_offensive_entities()\r\n    Offensive_Entity.HandleTrackPlayer(Player)\r\n\r\n    try:\r\n        if Player.health <= 0:\r\n            Game.clean_entities()\r\n            Player = instance_entities(entities,level)\r\n    except AttributeError:\r\n        pass\r\n\r\n    pygame.display.flip()\r\n    Game.dt = Game.clock.tick(60) / 1000\r\n\r\npygame.quit()";

            var nodes = JsonSerializer.Deserialize<List<Node>>(json);
            string code = string.Empty;

            code += "    if keys[pygame.K_ESCAPE]: \n      Game.running = False \n";

            foreach (Node node in nodes)
            {
                string declencheur = string.Empty;
                string mecanique = string.Empty;

                if(node.Mecanique.Actions.Count() == 0 || node.Declencheur.Events.Count() == 0)
                {
                    continue;
                }

                for (int i = 0; i < node.Declencheur.Events.Count; i++)
                {
                    if (i == node.Declencheur.Events.Count-1)
                    {
                        declencheur += node.Declencheur.Events[i].Formate();
                    }
                    else
                    {
                        declencheur += node.Declencheur.Events[i].Formate() + " & ";
                    }
                }
                for (int i = 0; i < node.Mecanique.Actions.Count; i++)
                {
                        mecanique += node.Mecanique.Actions[i].Formate() + "\n        ";
                }


                code += "    if " + declencheur + ":" +
                "\n" + "        " + mecanique + "\n";
            }

            text = text + code + end;

            // Chemin vers le script Python
            System.IO.File.WriteAllText(PythonScriptPath, text);
            string pythonExecutable = "python"; 

            // Adapter pour Linux/macOS
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                pythonExecutable = "python3"; 
            }

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = pythonExecutable,
                Arguments = PythonScriptPath, 
                UseShellExecute = false,       
                CreateNoWindow = true          
            };

            Process process = new Process { StartInfo = psi };
            process.Start();

        }
    }
}
        //TODO recuperer le <path>/config.json
        //string poject_name = project.name
        //string poject_version = project.version
        //Level[] levels = project.levels
        //Node[] nodes = levels[0].nodes; 
        //Entity[] entities = levels[0].entities;
