using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileSystem_Simulator.Modelo
{
    public class Directory : IFileSystemElement
    {
        private string name;

        public List<IFileSystemElement> elements = new List<IFileSystemElement>();


        public Directory parentDirectory { get; set; }

        public Directory(string name, Directory parentDirectory) 
        {
            this.name = name;
            this.parentDirectory = parentDirectory;
        }

        public void AddElement(IFileSystemElement element)
        {
            elements.Add(element);
        }

        public void RemoveElement(IFileSystemElement element)
        {
            elements.Remove(element);
        }

        public string display(string indent)
        {
            string result;

            result = ($"{indent}Carpeta: {name}");

            foreach (var element in elements)
            {
                result += element.display(indent + "  ");
            }

            return result;
        }

        public string DisplayContents(Directory directory)
        {
            string result;
            result = ($"Contenido de la carpeta '{directory.getName()}':");

            foreach (var element in directory.elements)
            {
                if (element is File)
                {
                    element.display("");
                }
                else if (element is Directory)
                {
                    result += ($"  Carpeta: {((Directory)element).getName()}");
                }
            }

            return result;
        }

        public string FullName
        {
            get
            {
                // Devolver la ruta completa de este directorio
                if (parentDirectory != null)
                {
                    return $"{parentDirectory.FullName}/{name}";
                }
                else
                {
                    return $"/{name}";
                }
            }
        }

        public string FullPath
        {
            get
            {
                if (parentDirectory != null)
                {
                    return $"{parentDirectory.FullPath}/{name}";
                }
                else
                {
                    return $"/{name}";
                }
            }
        }
        public File findFileByName(string fileName)
        {
            return findFileInDirectory(this, fileName);
        }

        private File findFileInDirectory(Directory directory, string fileName)
        {
            foreach (var element in directory.elements)
            {
                if (element is File file && file.getName().Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    return file;
                }
                else if (element is Directory subdirectory)
                {
                    // Recursivamente buscar en subdirectorios
                    File foundFile = findFileInDirectory(subdirectory, fileName);
                    if (foundFile != null)
                    {
                        return foundFile;
                    }
                }
            }

            return null;  // No se encontró el archivo en este directorio o sus subdirectorios
        }

        public void clearElements()
        {
            elements.Clear();
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }
    }
}
