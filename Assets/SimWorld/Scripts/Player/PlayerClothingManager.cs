using System.Collections.Generic;
using UnityEngine;
using AP.InventorySystem;
using AP.Paperdolling;

namespace AP.Player
{
    public class PlayerClothingManager : MonoBehaviour
    {
        [SerializeField] private List<ClothesTypeData> clothesTypeDatas;
        [SerializeField] private PaperdollParent playerPaperdoll;

        public void EquipItem(InventoryItem inventoryItem)
        {
            if (inventoryItem.prefab.GetComponent<ClothingItem>() == null) { return; }
            
            var ctd = GetCorrectClothesTypeData(inventoryItem.prefab.GetComponent<ClothingItem>());
            var item = Instantiate(inventoryItem.prefab, ctd.parent);
            var clothingItem = item.GetComponent<ClothingItem>();
            clothingItem.correspondingInventoryItem = inventoryItem;
            if (ctd.currentItem != null) { Destroy(ctd.currentItem.gameObject); }
            ctd.currentItem = clothingItem;
            item.GetComponent<PaperdollChild>().paperdollParent = playerPaperdoll;
        }

        public void CheckIfNeedToUnequip(InventoryItem inventoryItem)
        {
            if (inventoryItem.prefab.GetComponent<ClothingItem>() == null) { return; }

            var ctd = GetCorrectClothesTypeData(inventoryItem.prefab.GetComponent<ClothingItem>());
            if (ctd.currentItem.correspondingInventoryItem == inventoryItem)
            {
                Destroy(ctd.currentItem.gameObject);
                ctd.currentItem = null;
            }
        }

        public ClothesTypeData GetCorrectClothesTypeData(ClothingItem clothingItem)
        {
            int correctIndex = 0;

            for (int i = 0; i < clothesTypeDatas.Count; i++)
            {
                if (clothesTypeDatas[i].clothesType != clothingItem.clothesType) { continue; }
                correctIndex = i;
            }

            return clothesTypeDatas[correctIndex];
        }
    }

    [System.Serializable]
    public class ClothesTypeData
    {
        public ClothesType clothesType;
        public Transform parent;
        public ClothingItem currentItem;
    }
}