using UnityEngine;

namespace AP.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody2D m_rigidbody;
        private PlayerKeyboardInputController m_playerKeyboardInputController;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_playerKeyboardInputController = GetComponent<PlayerKeyboardInputController>();
        }

        private void FixedUpdate()
        {
            m_rigidbody.velocity = m_playerKeyboardInputController.InputDirection * speed;
        }

        private void OnDisable()
        {
            m_rigidbody.velocity = Vector2.zero;
        }
    }
}