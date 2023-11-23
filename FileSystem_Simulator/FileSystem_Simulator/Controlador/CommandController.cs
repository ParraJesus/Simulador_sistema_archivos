using FileSystem_Simulator.Controlador;
using FileSystem_Simulator.Modelo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static FileSystem_Simulator.Controlador.PermissionController;

namespace FileSystem_Simulator.Controllador
{
    internal class CommandController
    {
        private List<string> historyList = new List<string>();

        private User user;
        private UserController userController;
        private Terminal terminal;
        private TerminalController terminalController;
        private Directory currentDirectory;
        private PermissionController permissionController = new PermissionController();

        public CommandController(User user, UserController userController, Terminal terminal)
        {
            this.user = user;
            this.userController = userController;
            this.terminal = terminal;

            terminalController = new TerminalController(terminal);

            currentDirectory = user.HomeDirectory;  
            updatePrompt();
        }

        public string executeCommand(string command)
        {
            if (command == null) return "";
            string[] commandParts = command.Split(' ');
            string mainCommand = commandParts[0].ToLower();

            historyList.Add(command);

            switch (mainCommand)
            {
                case "echo":
                    return executeEcho(commandParts);

                case "mkdir":
                    return executeMkdir(commandParts);

                case "pwd":
                    return executePwd(commandParts);

                case "ls":
                    return executeLs(commandParts);

                case "cd":                   
                    return executeCd(commandParts);

                case "cat":
                    return executeCat(commandParts);

                case "mv":
                    return executeMv(commandParts);

                case "rm":
                    return executeRm(commandParts);

                case "chmod":
                    return executeChmod(commandParts);

                case "format":
                    return executeFormat(commandParts);

                case "cls":
                    return executeCls(commandParts);

                case "history":
                    return executeHistory(commandParts);

                case "su":
                    return executeSu(commandParts);
                case "help":
                    return executeHelp();
                default:
                    return "Invalid command";
            }

        }

        private string executeChmod(string[] command)
        {
            if (command.Length != 3)
            {
                return "Invalid 'chmod' command. Usage: chmod <permissions> <element_name>";
            }

            string permissionsStr = command[1];
            string elementName = command[2];

            if (permissionsStr.Length == 3 && int.TryParse(permissionsStr, out int permissions))
            {
                // Divide el número de permisos en tres dígitos individuales.
                int ownerPermission = permissions / 100;
                int groupPermission = (permissions / 10) % 10;
                int otherPermission = permissions % 10;

                // Luego, crea un array de enteros con estos valores.
                int[] permissionArray = { ownerPermission, groupPermission, otherPermission };

                IFileSystemElement targetElement = findElementByName(elementName, currentDirectory);

                if (targetElement != null)
                {
                    permissionController.ChangePermissions(targetElement, user, permissionArray);
                    return $"Permissions changed for {elementName}.";
                }
                else
                {
                    return $"Error: '{elementName}' not found in the current directory.";
                }
            }
            else
            {
                return "Invalid permissions value. Permissions must be a three-digit integer in the range 0-7.";
            }
        }



        private string executeEcho(string[] parts)
        {
            if (parts.Length != 2)
            {
                return "Invalid 'echo' command. Usage: echo 'body'";
            }

            if (parts[1].StartsWith("\"") && parts[parts.Length - 1].EndsWith("\""))
            {
                if (parts.Length >= 2)
                {
                    parts[1] = parts[1].Substring(1);
                    parts[parts.Length - 1] = parts[parts.Length - 1].Substring(0, parts[parts.Length - 1].Length - 1);
                }
            }
            else return "Invalid 'echo' command. Usage: echo 'body'";

            string[] arrAux = new string[parts.Length - 1];
            Array.Copy(parts, 1, arrAux, 0, arrAux.Length);

            string result = string.Join(" ", arrAux);

            return result;
        }

        private string executeCls(string[] parts)
        {
            if (parts.Length != 1)
            {
                return "Invalid 'cls' command. Usage: cls ";
            }

            terminalController.clearRtc();
            return "";
        }

        private string executeHistory(string[] parts)
        {
            if (parts.Length != 1)
            {
                return "Invalid 'history' command. Usage: history ";
            }

            return string.Join("\n", historyList);
        }

        private string executeCd(string[] parts) 
        {
            if (parts.Length == 1) 
            {
                currentDirectory = userController.RootDirectory;
            }
            if (parts.Length == 2)
            {
                string targetDirectoryName = parts[1];
                Directory targetDirectory = findDirectoryByName(targetDirectoryName, currentDirectory);
                if (targetDirectory != null)
                {
                    currentDirectory = targetDirectory;
                }
                else
                {
                    return $"Directory '{targetDirectoryName}' not found.";
                }
            }
            if (parts.Length >= 3)
            {
                return "Invalid 'cd' command. Usage: cd <directory_name>";
            }

            updatePrompt();

            return "";
        }

        private Directory findDirectoryByName(string directoryName, Directory parentDirectory)
        {
            foreach (var element in parentDirectory.elements)
            {
                if (element is Directory directory && directory.getName().Equals(directoryName, StringComparison.OrdinalIgnoreCase))
                {
                    return directory;
                }
            }

            return null;
        }

        private string executeMkdir(string[] parts)
        {
            if (!permissionController.HasWritePermission(currentDirectory, user))
            {
                return "Error: Permission denied. You don't have the required permissions.";
            }

            if (parts.Length != 2)
            {
                return "Invalid 'mkdir' command. Usage: mkdir <directory_name>";
            }

            string directoryName = parts[1];

            Directory newDirectory = new Directory(directoryName, currentDirectory, user);

            foreach (var element in currentDirectory.elements)
            {
                if (element is IFileSystemElement fileSystemElement && fileSystemElement.getName().Equals(directoryName, StringComparison.OrdinalIgnoreCase))
                {
                    return $"Error: Directory '{directoryName}' already exists.";
                }
            }

            newDirectory.Permissions = new int[] { 7, 5, 5 };
            currentDirectory.AddElement(newDirectory);

            return $"Directory '{directoryName}' created successfully.";
        }

        private string executeSu(string[] parts)
        {
            if (parts.Length != 3)
            {
                return "Invalid 'su' command. Usage: su <username> <password>";
            }

            string usernameToSwitch = parts[1];

            User newUser = userController.getUserByName(usernameToSwitch);

            if (newUser == null)
            {
                return $"User '{usernameToSwitch}' not found.";
            }

            string enteredPassword = parts[2];

            if (userController.verifyPassword(newUser, enteredPassword))
            {
                user = newUser;
                updatePrompt();

                string[] auxParts = new string[] {"cd"};

                executeCd(auxParts);

                return $"Switched to user {usernameToSwitch}.";
            }
            else
            {
                return "Incorrect password. Authentication failed.";
            }
        }

        private string executeLs(string[] parts)
        {
            if (parts.Length != 1)
            {
                return "Invalid 'ls' command. Usage: ls";
            }

            StringBuilder result = new StringBuilder();

            foreach (var element in currentDirectory.elements)
            {
                if (element is IFileSystemElement fileSystemElement)
                {
                    result.AppendLine(fileSystemElement.getName());
                }
            }

            return result.ToString();
        }

        private void updatePrompt()
        {
            terminal.UserPrompt = $"{user.Name}@linux:{currentDirectory.FullName} -$ ";
        }

        private string executePwd(string[] parts)
        {
            if (parts.Length != 1)
            {
                return "Invalid 'pwd' command. Usage: pwd";
            }

            return currentDirectory.FullName;
        }

        private string executeCat(string[] parts)
        {
            if (!permissionController.HasWritePermission(currentDirectory, user))
            {
                return "Error: Permission denied. You don't have the required permissions.";
            }

            if (parts.Length != 2)
            {
                return "Invalid 'cat' command. Usage: cat <filename.txt>";
            }

            string fileName = parts[1];

            File targetFile = currentDirectory.findFileByName(fileName);

            if (targetFile != null && permissionController.HasReadPermission(currentDirectory, user))
            {
                Debug.WriteLine(string.Join(" ", targetFile.Permissions));
                return targetFile.Text;
            }
            else
            {
                if (fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    File newFile = new File(fileName, user);
                    currentDirectory.AddElement(newFile);
                    return $"File '{fileName}' created.";
                }
                else
                {
                    return "Error: Invalid file extension. The file name must end with '.txt'.";
                }
            }
        }

        private string executeMv(string[] parts)
        {
            if (!permissionController.HasWritePermission(currentDirectory, user))
            {
                return "Error: Permission denied. You don't have the required permissions.";
            }

            if (parts.Length != 3)
            {
                return "Invalid 'mv' command. Usage: mv <old_name> <new_name>";
            }

            string oldName = parts[1];
            string newName = parts[2];

            IFileSystemElement targetElement = findElementByName(oldName, currentDirectory);

            if (targetElement != null)
            {
                if (targetElement is File && !newName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    return "Error: Invalid file extension. The new file name must end with '.txt'.";
                }

                targetElement.setName(newName);

                return $"{targetElement.GetType().Name} '{oldName}' renamed to '{newName}'.";
            }
            else
            {
                return $"Error: '{oldName}' not found in the current directory.";
            }
        }

        private IFileSystemElement findElementByName(string elementName, Directory directory)
        {
            return findElementInDirectory(elementName, directory);
        }

        private IFileSystemElement findElementInDirectory(string elementName, Directory directory)
        {
            foreach (var element in directory.elements)
            {
                if (element.getName().Equals(elementName, StringComparison.OrdinalIgnoreCase))
                {
                    return element;
                }
            }

            return null;
        }

        private string executeRm(string[] parts)
        {
            if (!permissionController.HasWritePermission(currentDirectory, user))
            {
                return "Error: Permission denied. You don't have the required permissions.";
            }

            if (parts.Length != 2)
            {
                return "Invalid 'rm' command. Usage: rm <element_name>";
            }

            string elementName = parts[1];

            IFileSystemElement targetElement = findElementByName(elementName, currentDirectory);

            if (targetElement != null)
            {
                currentDirectory.RemoveElement(targetElement);

                return $"{targetElement.GetType().Name} '{elementName}' removed.";
            }
            else
            {
                return $"Error: '{elementName}' not found in the current directory.";
            }
        }

        private string executeFormat(string[] parts)
        {
            if (!permissionController.HasWritePermission(currentDirectory, user))
            {
                return "Error: Permission denied. You don't have the required permissions.";
            }

            if (parts.Length != 1)
            {
                return "Invalid 'format' command. Usage: format";
            }

            currentDirectory.clearElements();

            return "File system formatted successfully.";
        }

        private string executeHelp() 
        {
            string result;

            result = "  Implemented command list: \n" +
                     "  echo. Usage: echo <body> \n" +
                     "  mkdir. Usage: mkdir <directory_name> \n" +
                     "  pwd. Usage: pwd \n" +
                     "  ls. Usage: ls \n" +
                     "  cd. Usage: cd <directory_name> \n" +
                     "  cat. cat <filename.txt> \n" +
                     "  mv. Usage: mv <old_name> <new_name> \n" +
                     "  rm. Usage: rm <element_name> \n" +
                     "  chmod.  \n" +
                     "  format.  Usage: format \n" +
                     "  cls.  Usage: cls \n" +
                     "  history.  Usage: history \n" +
                     "  su.  Usage: su <surname> <password> \n";

            return result;
        }

        #region GetterSetters
        public List<string> HistoryList { get => historyList; set => historyList = value; }
        public User User { get => user; set => user = value; }
        #endregion
    }
}
