// Used on actual item prefab
// for recognizing which type of clothing item it is.

using UnityEngine;

namespace AP.InventorySystem
{
    public enum ClothesType
    { Hat, Hair, Outfit }

    public class ClothingItem : MonoBehaviour
    {
        public ClothesType clothesType;
        public InventoryItem correspondingInventoryItem;
    }
}