using Bricks_Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bricks_Interfaces.ViewModels
{
    public class MainWindowViewModel : BaseNotifyPropertyChanded
    {
        public ICommand CompileCommand { get; set; }

        public static string project_path = "A:\\Code\\bricks-studio\\code\\python\\test.py";// a rendre modulable
        public static string output_path = "A:\\Code\\bricks-studio\\code\\python";


        public MainWindowViewModel(string project_path)
        {

            CompileCommand = new RelayCommand(Compile);

        }

        public static void Compile(object parameter)
        {
            // Création du script Python
            string json = System.IO.File.ReadAllText("A:/Code/bricks-studio/Bricks_Interfaces/Nodes.json");

            string text = "# Example file showing a circle moving on screen\r\nimport pygame\r\nimport random\r\n\r\n# pygame setup\r\npygame.init()\r\nscreen = pygame.display.set_mode((1280, 720))\r\nclock = pygame.time.Clock()\r\nrunning = True\r\ndt = 0\r\n\r\nplayer_pos = pygame.Vector2(screen.get_width() / 2, screen.get_height() / 2)\r\n\r\nwhile running:\r\n    # poll for events\r\n    # pygame.QUIT event means the user clicked X to close your window\r\n    for event in pygame.event.get():\r\n        if event.type == pygame.QUIT:\r\n            running = False\r\n \r\n    screen.fill(\"pink\")\r\n    pygame.draw.rect(screen, \"red\", (player_pos,(40,40)))\r\n\r\n    keys = pygame.key.get_pressed()\r\n";
            string end = "\r\n\r\n    # flip() the display to put your work on screen\r\n    pygame.display.flip()\r\n\r\n    # limits FPS to 60\r\n    # dt is delta time in seconds since last frame, used for framerate-\r\n    # independent physics.\r\n    dt = clock.tick(60) / 1000\r\n\r\npygame.quit()";

            var nodes = JsonSerializer.Deserialize<List<Node>>(json);
            string code = string.Empty;

            code += "    if keys[pygame.K_ESCAPE]: \n      running = False \n";

            foreach (Node node in nodes)
            {
                code += "    elif " + node.Declencheur.Events[0].Function + ":" +
                "\n" + "      " + node.Mecanique.Actions[0].Function + "\n";
            }

            text = text + code + end;

            // Chemin vers le script Python
            System.IO.File.WriteAllText(project_path, text);

            // Chemin vers le script PowerShell
            string pathToScript = "A:\\Code\\bricks-studio\\code\\powershell\\script.ps1";

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
