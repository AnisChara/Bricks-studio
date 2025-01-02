Param(
    [string]$customDistPath
)

# Chemin vers PyInstaller
$pyinstaller = "C:\Users\anisc\AppData\Roaming\Python\Python312\Scripts\pyinstaller.exe"

# Chemin du script Python à compiler
$pythonScript = Join-Path -Path $customDistPath -ChildPath "test.py"

# Compilation avec PyInstaller
& $pyinstaller --onefile --windowed --distpath $customDistPath $pythonScript

# # Chemin du Bureau
$desktopPath = [Environment]::GetFolderPath("Desktop")

# # Nom du fichier exécutable généré par PyInstaller (dans le dossier dist personnalisé)
$executableName = "test.exe"  # Nom basé sur le script original
$executablePath = Join-Path -Path $customDistPath -ChildPath $executableName

# # Chemin pour le raccourci
$shortcutPath = Join-Path -Path $desktopPath -ChildPath "test.lnk"

# Création du raccourci avec Windows Script Host
$WshShell = New-Object -ComObject WScript.Shell
$shortcut = $WshShell.CreateShortcut($shortcutPath)
$shortcut.TargetPath = $executablePath
$shortcut.IconLocation = $executablePath  # Utilise l'icône de l'exécutable
$shortcut.WindowStyle = 1                 # Fenêtre normale
$shortcut.Description = "Raccourci pour test.exe"
$shortcut.Save()

Write-Host "Compilation terminée, exécutable généré dans '$customDistPath' et raccourci créé sur le Bureau."

& $executablePath
