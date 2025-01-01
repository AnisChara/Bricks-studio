using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        string docPath = "A:\\Code\\bricks-studio\\code\\python\\test.py";
        string output_path = "A:\\Code\\bricks-studio\\code\\python";



        Compile(docPath,output_path);
    }

    public static void Compile(string project_path, string output_path)
    {
        // Création du script Python

        // Chemin vers le script PowerShell
        string pathToScript = "A:\\Code\\bricks-studio\\code\\powershell\\script.ps1";

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
