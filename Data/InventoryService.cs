using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coursework.Data
{
    public static class InventoryService
    {
        public static void SaveAll(List<InventoryItem> inventory)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string inventoryFilePath = Utils.GetInventoryItemFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(inventory);
            File.WriteAllText(inventoryFilePath, json);
        }

        public static List<InventoryItem> GetAllInventoryItems()
        {
            string inventoryFilePath = Utils.GetInventoryItemFilePath();
            if (!File.Exists(inventoryFilePath))
            {
                return new List<InventoryItem>();
            }

            var json = File.ReadAllText(inventoryFilePath);

            return JsonSerializer.Deserialize<List<InventoryItem>>(json);
        }

        public static List<InventoryItem> Create(InventoryItem itemObj)
        {
            List<InventoryItem> inventory = GetAllInventoryItems();
            var existingItem = inventory.Find(x => x.ItemName == itemObj.ItemName);
            if (existingItem == null)
            {
                inventory.Add(itemObj);
            }
            else
            {
                existingItem.AvailableQuantity += itemObj.AvailableQuantity;
            }
            SaveAll(inventory);
            Console.WriteLine(inventory);
            return inventory;
        }

        public static List<InventoryItem> Delete(Guid id)
        {
            List<InventoryItem> inventory = GetAllInventoryItems();
            InventoryItem inventoryItem = inventory.FirstOrDefault(x => x.Id == id);

            if (inventoryItem == null)
            {
                throw new Exception("Item not found.");
            }

            inventory.Remove(inventoryItem);
            SaveAll(inventory);
            return inventory;
        }

        public static List<InventoryItem> Update(InventoryItem itemObj)
        {
            List<InventoryItem> inventory = GetAllInventoryItems();
            InventoryItem itemToUpdate = inventory.Find(x => x.Id == itemObj.Id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemToUpdate.ItemName = itemObj.ItemName;
            itemToUpdate.AvailableQuantity = itemObj.AvailableQuantity;
            itemToUpdate.LastTakenOut = itemObj.LastTakenOut;
            SaveAll(inventory);
            return inventory;
        }
    }
}
