namespace FileSystem_Simulator.Modelo
{
    public interface IFileSystemElement
    {
        string display(string indent);

        string getName();

        void setName(string name);

        User Creator { get; }

        int[] Permissions { get; set; }
    }
}
