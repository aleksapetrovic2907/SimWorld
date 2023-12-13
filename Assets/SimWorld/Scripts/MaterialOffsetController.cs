using UnityEngine;

namespace AP
{
    public class MaterialOffsetController : MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Vector2 speed;

        private Vector2 m_currentOffset = Vector2.zero;

        private void Update()
        {
            m_currentOffset += speed * Time.deltaTime;
            renderer.material.mainTextureOffset = m_currentOffset;
        }
    }
}