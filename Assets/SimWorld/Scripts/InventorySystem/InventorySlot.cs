using UnityEngine;
using UnityEngine.UI;

namespace AP.InventorySystem
{
    [RequireComponent(typeof(Button))]
    public class InventorySlot : MonoBehaviour
    {
        public Button button;
        public Image itemIconHolder;
        public InventoryItem inventoryItem;

        public void Add(InventoryItem inventoryItem)
        {
            this.inventoryItem = inventoryItem;
            itemIconHolder.sprite = inventoryItem.icon;
            itemIconHolder.enabled = true;
        }

        public void Empty()
        {
            inventoryItem = null;
            itemIconHolder.sprite = null;
            itemIconHolder.enabled = false;
        }
    }
}