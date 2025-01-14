﻿using Bricks_Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public static string project_path = "..\\..\\..\\..\\code\\python\\index.py";// a rendre modulable
        public static string output_path = "./../../../../code/python";


        public MainWindowViewModel(string project_path)
        {

            CompileCommand = new RelayCommand(Compile);
        }

        public static void Compile(object parameter)
        {
            // Création du script Python
            string json = System.IO.File.ReadAllText("../../../Nodes.json");

            string text = "import pygame\r\nfrom Pawn_class import Pawn\r\nfrom Game_class import Game\r\nfrom Gravity_class import Gravity\r\nfrom Jump_class import Jump\r\nfrom Structure_class import Structure\r\nfrom Entity_class import Entity\r\nfrom Projectile_class import Projectile\r\nfrom OffensiveStructure_class import Offensive_Structure\r\nfrom OffensiveEntity_class import Offensive_Entity\r\n\r\n# pygame setup\r\npygame.init()\r\n\r\nbackground = pygame.image.load(\"C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/Fond.jpg\")\r\nbackground = pygame.transform.scale(background,(1280,720))\r\n\r\nleft_border = Structure(\"left_border\",-2147483646,-1,2147483646,2147483646,0,True,\"square\",0,False)\r\nright_border = Structure(\"right_border\",Game.screen_width+1,-1,2147483646, 2147483646,0,True,\"square\",0,False)\r\nup_border = Structure(\"up_border\",-1,-2147483646,2147483646, 2147483646,0,True,\"square\",0,False)\r\ndown_border = Structure(\"down_border\",-1,Game.screen_height+1,2147483646, 2147483646,0,True,\"square\",0,False)\r\n\r\nGame.entities.append(left_border)\r\nGame.entities.append(right_border)\r\nGame.entities.append(up_border)\r\nGame.entities.append(down_border)\r\nentities = Game.load_entities()\r\n\r\ndef instance_entities():\r\n    for entity in entities:\r\n\r\n        if entity['type'] == 'structure':\r\n            instance = Structure(entity['id'], entity['x']/0.584, entity['y']/0.515, entity['width']/0.584, entity['height']/0.515,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render'],entity['image'] )\r\n            Game.entities.append(instance)\r\n        elif entity['type'] == 'offensive_structure':\r\n            instance = Offensive_Structure(entity['id'], entity['x']/0.584, entity['y']/0.515, entity['width']/0.584, entity['height']/0.515,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render'],entity['damage'],entity['image'])\r\n            Game.entities.append(instance)\r\n        elif entity['type'] == 'offensive_entity':\r\n            instance = Offensive_Entity(entity['id'], entity['x']/0.584, entity['y']/0.515, entity['width']/0.584, entity['height']/0.515,entity[\"max_health\"],entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render'],entity['damage'],entity['image'])\r\n            Game.entities.append(instance)\r\n        elif entity['type'] == 'zone':\r\n            instance = Structure(entity['id'], entity['x']/0.584, entity['y']/0.515, entity['width']/0.584, entity['height']/0.515,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render'],entity['image'])\r\n            Game.entities.append(instance)\r\n        elif entity['type'] == 'Player':\r\n            Player = Pawn(entity['id'], entity['x']/0.584, entity['y']/0.515, entity['width']/0.584, entity['height']/0.515,entity['max_health'],entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'],entity['image'])\r\n            Game.entities.append(Player)\r\n            Player.set_weapon()\r\n        if entity[\"has_weapon\"]:\r\n            try:\r\n                instance.set_weapon()\r\n            except NameError: pass\r\n    return Player\r\n\r\nPlayer = instance_entities()\r\n\r\nwhile Game.running:\r\n\r\n    Game.screen.blit(background, (0,0))\r\n    Game.render()\r\n    Game.reset_keys()\r\n\r\n    for event in pygame.event.get():\r\n        if event.type == pygame.QUIT:\r\n            Game.running = False\r\n        if event.type == pygame.KEYDOWN:\r\n            Game.keydown_handle(event)\r\n            \r\n\r\n    keys = pygame.key.get_pressed()\r\n";
            string end = "\r\n    Jump.handle_jump()\r\n    Gravity.fall()\r\n    Projectile.handle_projectile()\r\n    Game.check_alive()\r\n    Offensive_Structure.handle_offensive_structures()\r\n    Offensive_Entity.handle_offensive_entities()\r\n\r\n    try:\r\n        if Player.health <= 0:\r\n            Game.clean_entities()\r\n            Player = instance_entities()\r\n    except AttributeError:\r\n        pass\r\n\r\n    # flip() the display to put your work on screen\r\n    pygame.display.flip()\r\n\r\n    # limits FPS to 60\r\n    # dt is delta time in seconds since last frame, used for framerate-\r\n    # independent physics.\r\n    Game.dt = Game.clock.tick(60) / 1000\r\n\r\npygame.quit()";

            var nodes = JsonSerializer.Deserialize<List<Node>>(json);
            string code = string.Empty;

            code += "    if keys[pygame.K_ESCAPE]: \n      Game.running = False \n";

            foreach (Node node in nodes)
            {
                string declencheur = string.Empty;
                string mecanique = string.Empty;

                if(node.Mecanique == null || node.Declencheur == null)
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
            System.IO.File.WriteAllText(project_path, text);

            // Chemin vers le script PowerShell
            string pathToScript = "../../../..\\code\\powershell\\script.ps1";

            // Préparation des arguments pour PowerShell
            var scriptArguments = "-ExecutionPolicy Bypass -File \"" + pathToScript + "\"  -customDistPath \"" + output_path + "\"";


            var processStartInfo = new ProcessStartInfo("powershell.exe", scriptArguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                //WorkingDirectory = output_path,
            };

            using var process = new Process
            {
                StartInfo = processStartInfo
            };

            // Gestion de la sortie et des erreurs
            process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
            process.ErrorDataReceived += (sender, e) => Console.Error.WriteLine(e.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // Attendre la fin du script PowerShell
            process.WaitForExit();
            int exitCode = process.ExitCode;

        }
    }
}
        //TODO recuperer le <path>/config.json
        //string poject_name = project.name
        //string poject_version = project.version
        //Level[] levels = project.levels
        //Node[] nodes = levels[0].nodes; 
        //Entity[] entities = levels[0].entities;
