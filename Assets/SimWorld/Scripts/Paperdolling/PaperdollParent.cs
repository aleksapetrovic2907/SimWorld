// Figures out which sprite in the spritesheet
// corresponds to current sprite in spriteRenderer.

using UnityEngine;

namespace AP.Paperdolling
{
    public class PaperdollParent : MonoBehaviour
    {
        public int SpriteIndexInSheet { get; private set; } = 0;

        [SerializeField] private string texturePath;

        private Sprite[] m_allSprites;
        private SpriteRenderer m_spriteRenderer;

        private void Awake()
        {
            m_allSprites = Resources.LoadAll<Sprite>(texturePath);
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            for (int i = 0; i < m_allSprites.Length; i++)
            {
                if (m_spriteRenderer.sprite == m_allSprites[i])
                {
                    SpriteIndexInSheet = i;
                    break;
                }
            }
        }
    }
}