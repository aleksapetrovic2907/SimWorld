using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AP.InventorySystem;
using AP.Player;

namespace AP.ShopSystem
{
    public enum BuyItemPromptResult
    { Bought, Cancelled }

    public class ShopManager : MonoBehaviour
    {
        public bool IsPromptActive { get; private set; } = false;

        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private List<TabData> tabDatas;

        [SerializeField] private ShopSlot shopSlotPrefab;
        [SerializeField] private GameObject prompt;
        [SerializeField] private Button buyButton, cancelButton;
        [SerializeField] private GameObject alreadyBoughtPrompt;
        [SerializeField] private GameObject insufficientGoldPrompt;
        [SerializeField] private GameObject shopAndInventoryCanvas;

        private List<InventorySlot> m_inventorySlots = new List<InventorySlot>();
        private TaskCompletionSource<BuyItemPromptResult> m_promptTask;

        private void Awake()
        {
            GenerateSlots();
            SubscribePromptButtons();
        }

        private void GenerateSlots()
        {
            foreach (var tabData in tabDatas)
            {
                for (int i = 0; i < tabData.inventoryItems.Count; i++)
                {
                    var index = i;
                    ShopSlot slot = Instantiate(shopSlotPrefab, tabData.container);
                    slot.Add(tabData.inventoryItems[index]);
                    slot.button.onClick.AddListener(() => OnSlotPressed(slot));
                }
            }
        }

        private async void OnSlotPressed(ShopSlot slot)
        {
            if (slot.inventoryItem == null) { return; }
            if (IsPromptActive || inventoryManager.IsPromptActive) { return; }
            if (inventoryManager.HasItem(slot.inventoryItem))
            {
                DisplayAlreadyBoughtPrompt();
                return;
            }
            if (GameManager.Instance.gold < slot.inventoryItem.cost)
            {
                DisplayInsufficientGoldPrompt();
                return;
            }

            prompt.SetActive(IsPromptActive = true);
            m_promptTask = new TaskCompletionSource<BuyItemPromptResult>();
            BuyItemPromptResult result = await m_promptTask.Task;

            if (result == BuyItemPromptResult.Bought)
            {
                GameManager.Instance.LoseGold(slot.inventoryItem.cost);
                Inventory.AddItem(slot.inventoryItem);
                inventoryManager.RefreshSlots();
            }

            prompt.SetActive(IsPromptActive = false);
        }

        private void SubscribePromptButtons()
        {
            buyButton.onClick.AddListener(() => m_promptTask.SetResult(BuyItemPromptResult.Bought));
            cancelButton.onClick.AddListener(() => m_promptTask.SetResult(BuyItemPromptResult.Cancelled));
        }

        private void DisplayAlreadyBoughtPrompt() => alreadyBoughtPrompt.SetActive(true);
        private void DisplayInsufficientGoldPrompt() => insufficientGoldPrompt.SetActive(true);

        public void OpenShopAndInventory()
        {
            PlayerController.Instance.DisablePlayerInput();
            shopAndInventoryCanvas.SetActive(true);
        }

        public void CloseShopAndInventory()
        {
            PlayerController.Instance.EnablePlayerInput();
            shopAndInventoryCanvas.SetActive(false);
        }
    }

    [System.Serializable]
    public class TabData
    {
        public RectTransform container;
        public List<InventoryItem> inventoryItems;
    }
}