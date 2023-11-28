using FileSystem_Simulator.Modelo;
using System.Diagnostics;

namespace FileSystem_Simulator.Controlador
{
    public class PermissionController
    {
        #region Functions
        public bool HasReadPermission(IFileSystemElement element, User currentUser)
        {
            if (element.Creator.Name.Equals(currentUser.Name))
            {
                return (element.Permissions[0] & 4) >= 4;
            }
            else if (element.Creator.Group.Equals(currentUser.Group))
            {
                return (element.Permissions[1] & 4) >= 4;
            }
            else
            {
                return (element.Permissions[2] & 4) >= 4;
            }
        }

        public bool HasWritePermission(IFileSystemElement element, User currentUser)
        {
            Debug.WriteLine(element.getName(), currentUser.Name);

            if (element.getName().Equals("HOME")) return false;

            if (element.Creator.Name.Equals(currentUser.Name))
            {
                return (element.Permissions[0] == 2 || element.Permissions[0] == 3 || element.Permissions[0] == 6 || element.Permissions[0] == 7);
            }
            else if (element.Creator.Group.Equals(currentUser.Group))
            {
                return (element.Permissions[1] == 2 || element.Permissions[1] == 3 || element.Permissions[1] == 6 || element.Permissions[1] == 7);
            }
            else
            {
                return (element.Permissions[2] == 2 || element.Permissions[2] == 3 || element.Permissions[2] == 6 || element.Permissions[2] == 7);
            }
        }

        public bool HasExecutePermission(IFileSystemElement element, User currentUser)
        {
            if (element.Creator.Name.Equals(currentUser.Name))
            {
                return (element.Permissions[0] % 2 != 1);
            }
            else if (element.Creator.Group.Equals(currentUser.Group))
            {
                return (element.Permissions[1] % 2 != 1);
            }
            else
            {
                return (element.Permissions[2] % 2 != 1);
            }
        }

        public void ChangePermissions(IFileSystemElement element, User currentUser, int[] permissions)
        {
            if (currentUser == null || element == null)
            {
                return;
            }

            if (AreValidPermissions(permissions))
            {
                element.Permissions = permissions;
            }
        }

        private bool AreValidPermissions(int[] permissions)
        {
            foreach (var permission in permissions)
            {
                if (permission < 0 || permission > 7)
                {
                    return false;
                }
            }

            return true;
        } 
        #endregion
    }
}
