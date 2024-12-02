using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        string docPath = "C:\\Users\\user\\Documents\\COURS\\C#\\Projet\\bricks-studio\\code\\python\\test.py";
        string output_path = "C:\\Users\\user\\Documents\\COURS\\C#\\Projet\\bricks-studio\\code\\python";


        Node node = new Node("keys[pygame.K_m]","running = False", "Reset");
        Node node2 = new Node("keys[pygame.K_x]","player_pos.x = 100", "TP");
        List<Node> nodes = new List<Node>();
        nodes.Add(node);
        nodes.Add(node2);
        string json = JsonSerializer.Serialize(nodes);

        Compile(docPath,output_path,json);
    }

    public static void Compile(string project_path, string output_path,string json)
    {
        // Création du script Python
        string text = "# Example file showing a circle moving on screen\r\nimport pygame\r\n\r\n# pygame setup\r\npygame.init()\r\nscreen = pygame.display.set_mode((1280, 720))\r\nclock = pygame.time.Clock()\r\nrunning = True\r\ndt = 0\r\n\r\nplayer_pos = pygame.Vector2(screen.get_width() / 2, screen.get_height() / 2)\r\n\r\nwhile running:\r\n    # poll for events\r\n    # pygame.QUIT event means the user clicked X to close your window\r\n    for event in pygame.event.get():\r\n        if event.type == pygame.QUIT:\r\n            running = False\r\n \r\n    screen.fill(\"pink\")\r\n    pygame.draw.rect(screen, \"red\", (player_pos,(40,40)))\r\n\r\n    keys = pygame.key.get_pressed()\r\n";
        string end = "\r\n\r\n    # flip() the display to put your work on screen\r\n    pygame.display.flip()\r\n\r\n    # limits FPS to 60\r\n    # dt is delta time in seconds since last frame, used for framerate-\r\n    # independent physics.\r\n    dt = clock.tick(60) / 1000\r\n\r\npygame.quit()";

        var nodes = JsonSerializer.Deserialize<List<Node>>(json);
        string code = string.Empty;

        foreach (Node node in nodes)
        {
            code += "    if " + node.trigger + ":" +
            "\n" + "      " + node.action + "\n";
        }

        text = text + code + end;

        // Chemin vers le script Python
        File.WriteAllText(project_path, text);

        // Chemin vers le script PowerShell
        string pathToScript = "C:\\Users\\user\\Documents\\COURS\\C#\\Projet\\bricks-studio\\code\\powershell\\script.ps1";

        // Préparation des arguments pour PowerShell
        var scriptArguments = "-ExecutionPolicy Bypass -File \"" + pathToScript + "\"  -customDistPath \""+ output_path + "\"";


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
        Console.WriteLine($"PowerShell script exited with code: {exitCode}");

    }

    public class Node
    {
        public string Name { get; set; }

        public string action { get; set; }

        public string trigger { get; set; }

        public Node(string trigger, string action, string name) { 

            this.action = action;
            this.trigger = trigger;
            this.Name = name;

        }
    }
}
