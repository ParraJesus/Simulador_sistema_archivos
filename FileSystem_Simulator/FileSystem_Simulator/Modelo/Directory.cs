using System;
using System.Collections.Generic;

namespace FileSystem_Simulator.Modelo
{
    public class Directory : IFileSystemElement
    {
        #region Attributes
        private string name;
        private User creator;
        public List<IFileSystemElement> elements = new List<IFileSystemElement>();
        public int[] Permissions { get; set; } = { 7, 5, 5 };
        public Directory parentDirectory { get; set; } 
        #endregion

        #region Constructor
        public Directory(string name, Directory parentDirectory, User userCreator)
        {
            this.name = name;
            this.parentDirectory = parentDirectory;
            this.creator = userCreator;
        }
        #endregion

        #region Functions
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

            result = ($"{indent}Folder: {name}");

            foreach (var element in elements)
            {
                result += element.display(indent + "  ");
            }

            return result;
        }

        public string DisplayContents(Directory directory)
        {
            string result;
            result = ($"Folder content: '{directory.getName()}':");

            foreach (var element in directory.elements)
            {
                if (element is File)
                {
                    element.display("");
                }
                else if (element is Directory)
                {
                    result += ($"  Folder: {((Directory)element).getName()}");
                }
            }

            return result;
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
                    File foundFile = findFileInDirectory(subdirectory, fileName);
                    if (foundFile != null)
                    {
                        return foundFile;
                    }
                }
            }

            return null;
        }

        public void clearElements()
        {
            elements.Clear();
        } 
        #endregion

        #region GetterSetters
        public string FullName
        {
            get
            {
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

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public User Creator
        {
            get { return creator; }
        } 
        #endregion
    }
}
