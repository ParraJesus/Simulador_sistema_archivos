using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileSystem_Simulator.Modelo
{
    internal class Command
    {
        private List<string> historyList = new List<string>();

        public string executeCommand(string command)
        {
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
                    ;
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
                    //borrar pantalla

                    return "";

                case "history":
                    return executeHistory();

                default:
                    return "command not found";
            }

        }

        private string executeEcho(string[] parts)
        {
            if (parts[1].StartsWith("\"") && parts[parts.Length - 1].EndsWith("\""))
            {
                if (parts.Length >= 2)
                {
                    parts[1] = parts[1].Substring(1);
                    parts[parts.Length - 1] = parts[parts.Length - 1].Substring(0, parts[parts.Length - 1].Length - 1);
                }
            }
            else return "command body is invalid";

            string[] arrAux = new string[parts.Length - 1];
            Array.Copy(parts, 1, arrAux, 0, arrAux.Length);

            string result = string.Join(" ", arrAux);
            Debug.WriteLine(result);

            return result;
        }

        private void executeCls() 
        {

        }

        private string executeHistory()
        {
            Debug.WriteLine(string.Join("\n", historyList));

            return string.Join("\n", historyList);
        }

    }
}
