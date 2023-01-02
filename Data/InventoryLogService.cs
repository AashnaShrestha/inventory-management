using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coursework.Data
{
    public static class InventoryLogService
    {
        private static void SaveAll(List<InventoryLog> inventoryLogs)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string inventoryFilePath = Utils.GetInventoryLogFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(inventoryLogs);
            File.WriteAllText(inventoryFilePath, json);
        }

        public static List<InventoryLog> GetAllInventoryLogs()
        {
            string inventoryFilePath = Utils.GetInventoryLogFilePath();
            if (!File.Exists(inventoryFilePath))
            {
                return new List<InventoryLog>();
            }

            var json = File.ReadAllText(inventoryFilePath);

            return JsonSerializer.Deserialize<List<InventoryLog>>(json);
        }

        public static List<InventoryLog> Create(InventoryLog itemObj)
        {
            List<InventoryLog> inventoryLogs = GetAllInventoryLogs();
            inventoryLogs.Add(itemObj);
            var inventoryItem = itemObj.Item;
            inventoryItem.AvailableQuantity -= itemObj.Quantity;
            inventoryItem.LastTakenOut = DateTime.Now;
            SaveAll(inventoryLogs);
            InventoryService.Update(inventoryItem);
            return inventoryLogs;
        }

        public static List<InventoryLog> Delete(Guid id)
        {
            List<InventoryLog> inventoryLogs = GetAllInventoryLogs();
            InventoryLog inventoryLog = inventoryLogs.FirstOrDefault(x => x.Id == id);

            if (inventoryLog == null)
            {
                throw new Exception("Item not found.");
            }

            inventoryLogs.Remove(inventoryLog);
            SaveAll(inventoryLogs);
            return inventoryLogs;
        }

        public static List<InventoryLog> Update(InventoryLog itemObj)
        {
            List<InventoryLog> inventoryLogs = GetAllInventoryLogs();
            InventoryLog itemToUpdate = inventoryLogs.FirstOrDefault(x => x.Id == itemObj.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemToUpdate = itemObj;
            SaveAll(inventoryLogs);
            return inventoryLogs;
        }
    }
}
