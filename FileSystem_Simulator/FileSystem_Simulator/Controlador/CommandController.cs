using FileSystem_Simulator.Controlador;
using FileSystem_Simulator.Modelo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace FileSystem_Simulator.Controllador
{
    internal class CommandController
    {
        private List<string> historyList = new List<string>();

        private User user;
        private UserController userController;
        private Terminal terminal;
        private TerminalController terminalController;

        public CommandController(User user, UserController userController, Terminal terminal)
        {
            this.user = user;
            this.userController = userController;
            this.terminal = terminal;

            terminalController = new TerminalController(terminal);
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
                    //crear directorio
                    return "";

                case "pwd":
                    //mostrar directorio actual
                    return "";

                case "ls":
                    //mostrar contenido del directorio
                    return "";

                case "cd":
                    //dirigirse a un directorio o a raíz
                    return "";

                case "cat":
                    //crear o visualizar el contenido de un txt (necesita extensión .txt)
                    return "";

                case "mv":
                    //renombrar archivo o directorio
                    return "";

                case "rm":
                    //eliminar archivo o directorio
                    return "";

                case "chmod":
                    // permisos
                    return "";

                case "format":
                    //formatear todo
                    return "";

                case "cls":
                    return executeCls(commandParts);

                case "history":
                    return executeHistory(commandParts);

                case "su":
                    return executeSu(commandParts);

                default:
                    return "Invalid command";
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
            Debug.WriteLine(result);

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
            Debug.WriteLine(string.Join("\n", historyList));

            return string.Join("\n", historyList);
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
                terminal.Prompt = $"{user.Name}@linux:-$ ";
                return $"Switched to user {usernameToSwitch}.";
            }
            else
            {
                return "Incorrect password. Authentication failed.";
            }
        }

        #region GetterSetters
        public List<string> HistoryList { get => historyList; set => historyList = value; }
        public User User { get => user; set => user = value; }
        #endregion
    }
}
