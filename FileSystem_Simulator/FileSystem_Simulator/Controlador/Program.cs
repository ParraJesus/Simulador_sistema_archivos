using FileSystem_Simulator.Controllador;
using FileSystem_Simulator.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystem_Simulator
{
    //HECHO POR JESUS PARRA Y ELKIN MORILLO

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            UserController userController = new UserController();
            userController.regUser("root", "123");
            userController.regUser("jesus", "123");
            userController.regUser("elkin", "123");

            Directory sharedRootDirectory = new Directory("root", null, userController.getUserByName("root"));

            Application.Run(new Terminal(userController));
        }
    }
}
