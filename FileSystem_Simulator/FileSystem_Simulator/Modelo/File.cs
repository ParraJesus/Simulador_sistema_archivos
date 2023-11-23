using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem_Simulator.Modelo
{
    public class File : IFileSystemElement
    {
        private string name;
        private string text;

        public File(string name)
        {
            this.name = name;
            this.text = "This is the content inside the file " + name;
        }

        public string display(string indent)
        {
            return ($"{indent}Archivo: {name}");
        }

        public string getName()
        {
            return name;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public void setName(string name)
        {
            this.name = name;
        }
    }
}
