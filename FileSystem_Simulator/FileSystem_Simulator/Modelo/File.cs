namespace FileSystem_Simulator.Modelo
{
    public class File : IFileSystemElement
    {
        #region Attributes
        private string name;
        private string text;
        private User creator;
        public int[] Permissions { get; set; } = { 6, 4, 4 }; 
        #endregion

        #region Constructor
        public File(string name, User userCreator)
        {
            this.name = name;
            this.text = "This is the content inside the file " + name;
            creator = userCreator;
        }
        #endregion

        #region Functions
        public string display(string indent)
        {
            return ($"{indent}Archivo: {name}");
        } 
        #endregion

        #region GetterSetters
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
        public User Creator
        {
            get { return creator; }
        } 
        #endregion
    }
}
