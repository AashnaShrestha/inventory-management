using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Data
{
    public static class Utils
    {
        public static string GetAppDirectoryPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Islington-Todo"
            );
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "users.json");
        }

        public static string GetInventoryItemFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "inventory.json");
        }

        public static string GetInventoryLogFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "inventoryLog.json");
        }
    }
}
