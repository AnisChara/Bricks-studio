Param(
    [string]$customDistPath
)

# Chemin vers PyInstaller
$pyinstaller = "C:\Users\user\AppData\Local\Packages\PythonSoftwareFoundation.Python.3.11_qbz5n2kfra8p0\LocalCache\local-packages\Python311\Scripts\pyinstaller.exe"

# Chemin du script Python à compiler
$pythonScript = Join-Path -Path $customDistPath -ChildPath "index.py"

# Compilation avec PyInstaller
& $pyinstaller --onefile --windowed --distpath $customDistPath $pythonScript

# # Chemin du Bureau
$desktopPath = [Environment]::GetFolderPath("Desktop")

# # Nom du fichier exécutable généré par PyInstaller (dans le dossier dist personnalisé)
$executableName = "index.exe"  # Nom basé sur le script original
$executablePath = Join-Path -Path (Join-Path -Path (Get-Location) -ChildPath $customDistPath) -ChildPath $executableName

# # Chemin pour le raccourci
$shortcutPath = Join-Path -Path $desktopPath -ChildPath "index.lnk"

# Création du raccourci avec Windows Script Host
$WshShell = New-Object -ComObject WScript.Shell
$shortcut = $WshShell.CreateShortcut($shortcutPath)
$shortcut.TargetPath = $executablePath
$shortcut.IconLocation = $executablePath  # Utilise l'icône de l'exécutable
$shortcut.WindowStyle = 1                 # Fenêtre normale
$shortcut.Description = "Raccourci pour index.exe"
$shortcut.Save()

Write-Host "Compilation terminée, exécutable généré dans '$customDistPath' et raccourci créé sur le Bureau."

& $executablePath
