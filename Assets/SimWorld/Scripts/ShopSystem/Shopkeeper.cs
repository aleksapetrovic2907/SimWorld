using UnityEngine;

namespace AP.ShopSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class Shopkeeper : MonoBehaviour, IMouseInteractable, IMouseHoverable
    {
        [SerializeField] private ShopManager shopManager;
        [SerializeField] private Color bubbleInRangeColor;
        [SerializeField] private Color bubbleOutOfRangeColor;
        [SerializeField] private SpriteRenderer bubble;

        public void OnMouseClick(bool isInRange)
        {
            if (!isInRange) { return; }
            bubble.gameObject.SetActive(false);
            shopManager.OpenShopAndInventory();
        }

        public void OnMouseEntered(bool isInRange)
        {
            bubble.gameObject.SetActive(true);
            bubble.color = isInRange ? bubbleInRangeColor : bubbleOutOfRangeColor;
        }

        public void OnMouseExited()
        {
            bubble.gameObject.SetActive(false);
        }

        public void OnMouseStay(bool isInRange)
        {
            bubble.color = isInRange ? bubbleInRangeColor : bubbleOutOfRangeColor;
        }
    }
}