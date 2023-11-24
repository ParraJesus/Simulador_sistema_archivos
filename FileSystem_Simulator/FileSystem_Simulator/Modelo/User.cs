namespace FileSystem_Simulator.Modelo
{
    public class User
    {
        #region Attributes
        private string name;
        private string password;
        private string group;
        private Directory homeDirectory; 
        #endregion

        #region Constructor
        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
            group = name;

            homeDirectory = new Directory(name, null, this);
        } 
        #endregion

        #region GetterSetters
        public string Name { get => name; set => name = value; }

        public string Password { get => password; set => password = value; }

        public string Group { get => group; set => group = value; }

        public Directory HomeDirectory { get => homeDirectory; set => homeDirectory = value; }
        #endregion
    }
}
