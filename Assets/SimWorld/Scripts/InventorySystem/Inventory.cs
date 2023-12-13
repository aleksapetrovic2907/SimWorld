using System.Collections.Generic;

namespace AP.InventorySystem
{
    public static class Inventory
    {
        public static List<InventoryItem> items = new List<InventoryItem>();
        public static int MAX_SLOTS { get; private set; } = 18;

        public static void AddItem(InventoryItem inventoryItem)
        {
            if (items.Count == MAX_SLOTS) { return; }
            items.Add(inventoryItem);
        }

        public static void RemoveItem(InventoryItem inventoryItem)
        {
            items.Remove(inventoryItem);
        }
    }
}