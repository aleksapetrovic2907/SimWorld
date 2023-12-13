using System.Collections.Generic;
using UnityEngine;

namespace AP
{
    public class SpriteSwapper : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float swapDelay;

        private int m_currentSpriteIndex = 0;
        private float m_timer = 0f;

        private void Update()
        {
            m_timer += Time.deltaTime;

            if (m_timer >= swapDelay)
            {
                m_timer = 0f;
                m_currentSpriteIndex = (m_currentSpriteIndex + 1) % sprites.Count;
                spriteRenderer.sprite = sprites[m_currentSpriteIndex];
            }
        }
    }
}