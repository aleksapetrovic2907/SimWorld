using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AP.ShopSystem;
using AP.Player;

namespace AP.InventorySystem
{
    public enum ItemSelectionPromptResult
    { Sold, Equipped }

    public class InventoryManager : MonoBehaviour
    {
        public bool IsPromptActive { get; private set; } = false;

        [SerializeField] private ShopManager shopManager;
        [SerializeField] private PlayerClothingManager playerClothingManager;
        [SerializeField] private InventorySlot inventorySlotPrefab;
        [SerializeField] private RectTransform slotsParent;
        [SerializeField] private GameObject prompt;
        [SerializeField] private Button sellButton, equipButton;

        private List<InventorySlot> m_inventorySlots = new List<InventorySlot>();
        private TaskCompletionSource<ItemSelectionPromptResult> m_promptTask;

        private void Awake()
        {
            GenerateEmptySlots();
            FillSlots();
            SubscribePromptButtons();
        }

        private void GenerateEmptySlots()
        {
            for (int i = 0; i < Inventory.MAX_SLOTS; i++)
            {
                InventorySlot slot = Instantiate(inventorySlotPrefab, slotsParent);
                m_inventorySlots.Add(slot);
                slot.button.onClick.AddListener(() => OnSlotPressed(slot));
            }
        }

        private void FillSlots()
        {
            for (int i = 0; i < Inventory.items.Count; i++)
            {
                m_inventorySlots[i].Add(Inventory.items[i]);
            }
        }

        public void RefreshSlots()
        {
            foreach (var slot in m_inventorySlots)
            {
                slot.Empty();
            }

            FillSlots();
        }

        private async void OnSlotPressed(InventorySlot slot)
        {
            if (slot.inventoryItem == null) { return; }
            if (IsPromptActive || shopManager.IsPromptActive) { return; }

            prompt.SetActive(IsPromptActive = true);
            m_promptTask = new TaskCompletionSource<ItemSelectionPromptResult>();
            ItemSelectionPromptResult result = await m_promptTask.Task;

            if (result == ItemSelectionPromptResult.Sold)
            {
                GameManager.Instance.GainGold(slot.inventoryItem.cost);
                playerClothingManager.CheckIfNeedToUnequip(slot.inventoryItem);
                Inventory.RemoveItem(slot.inventoryItem);
                slot.Empty();
                RefreshSlots();
            }
            else
            {
                playerClothingManager.EquipItem(slot.inventoryItem);
            }

            prompt.SetActive(IsPromptActive = false);
        }

        public bool HasItem(InventoryItem inventoryItem)
        {
            return Inventory.items.Contains(inventoryItem);
        }

        private void SubscribePromptButtons()
        {
            sellButton.onClick.AddListener(() => m_promptTask.SetResult(ItemSelectionPromptResult.Sold));
            equipButton.onClick.AddListener(() => m_promptTask.SetResult(ItemSelectionPromptResult.Equipped));
        }
    }
}