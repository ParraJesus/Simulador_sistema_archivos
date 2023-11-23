using FileSystem_Simulator.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem_Simulator.Controllador
{
    public class UserController
    {
        private List<User> users = new List<User>();

        public UserController() { }

        public void regUser(string userName, string userPass)
        {
            // Lógica para registrar un nuevo usuario
            users.Add(new User(userName, userPass));
        }

        public bool login(string userName, string userPass)
        {
            // Lógica para verificar las credenciales y permitir el inicio de sesión
            User user = users.FirstOrDefault(u => u.Name == userName && u.Password == userPass);
            return user != null;
        }

        public User getUserByName(string userName) 
        {
            User foundUser = users.Find(user => user.Name == userName);

            if (foundUser != null)
            {
                return foundUser;
            }
            else
            {
                Console.WriteLine("Usuario no encontrado");
                return null;
            }
        }

        public bool verifyPassword(User user, string enteredPassword)
        {
            return user.Password == enteredPassword;
        }

        #region GetterSetters
        public List<User> Users { get => users; set => users = value; } 
        #endregion
    }
}
