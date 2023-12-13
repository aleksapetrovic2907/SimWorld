// Swaps current sprite with the sprite in spritesheet
// corresponding to their parent's index of sprite in spritesheet.

using UnityEngine;

namespace AP.Paperdolling
{
    public class PaperdollChild : MonoBehaviour
    {
        [Tooltip("Used as a reference point for easily accessing the texture it uses.")]
        public Texture2D texture2D;
        
        public string texturePath;
        public PaperdollParent paperdollParent;

        private Sprite[] m_allSprites;
        private SpriteRenderer m_spriteRenderer;

        private void Start()
        {
            m_allSprites = Resources.LoadAll<Sprite>(texturePath);
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            if (paperdollParent == null) { return; }
            m_spriteRenderer.sprite = m_allSprites[paperdollParent.SpriteIndexInSheet];
        }
    }
}