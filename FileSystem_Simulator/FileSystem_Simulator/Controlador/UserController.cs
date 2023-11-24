using FileSystem_Simulator.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem_Simulator.Controllador
{
    public class UserController
    {
        #region Attributes
        private List<User> users = new List<User>();
        private Directory rootDirectory; 
        #endregion

        #region Constructor
        public UserController()
        {
            rootDirectory = new Directory("HOME", null, null);
        }
        #endregion

        #region Functions
        public void regUser(string userName, string userPass)
        {
            User newUser = new User(userName, userPass);
            users.Add(newUser);

            Directory userDirectory = new Directory(userName, rootDirectory, newUser);
            rootDirectory.AddElement(userDirectory);
            newUser.HomeDirectory = userDirectory;
        }

        public bool login(string userName, string userPass)
        {
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
        #endregion

        #region GetterSetters
        public List<User> Users { get => users; set => users = value; }
        public Directory RootDirectory { get => rootDirectory; set => rootDirectory = value; }
        #endregion
    }
}
