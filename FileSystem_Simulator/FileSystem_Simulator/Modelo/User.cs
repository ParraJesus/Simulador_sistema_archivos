using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem_Simulator.Modelo
{
    public class User
    {
        private string name;
        private string password;
        private string group;
        public User(string name, string password) 
        {
            this.name = name;
            this.password = password;

            group = name;
        }

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Group { get => group; set => group = value; }
    }
}
