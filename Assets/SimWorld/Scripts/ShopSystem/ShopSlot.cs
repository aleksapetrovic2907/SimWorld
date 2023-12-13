using UnityEngine;
using UnityEngine.UI;
using AP.InventorySystem;
using TMPro;

namespace AP.ShopSystem
{
    [RequireComponent(typeof(Button))]
    public class ShopSlot : MonoBehaviour
    {
        public Button button;
        public Image itemIconHolder;
        public TextMeshProUGUI cost;
        public InventoryItem inventoryItem;

        public void Add(InventoryItem inventoryItem)
        {
            this.inventoryItem = inventoryItem;
            itemIconHolder.sprite = inventoryItem.icon;
            itemIconHolder.enabled = true;
            cost.text = inventoryItem.cost.ToString();
            cost.gameObject.SetActive(true);
        }

        public void Empty()
        {
            inventoryItem = null;
            itemIconHolder.sprite = null;
            itemIconHolder.enabled = false;
            cost.gameObject.SetActive(false);
        }
    }
}