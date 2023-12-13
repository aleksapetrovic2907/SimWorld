using UnityEngine;

namespace AP.InventorySystem
{
    [CreateAssetMenu(fileName = "Inventory Item", menuName = "InventoryItem")]
    public class InventoryItem : ScriptableObject
    {
        public Sprite icon;
        public int cost;
        public GameObject prefab;
    }
}