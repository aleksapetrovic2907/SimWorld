using UnityEngine;

namespace AP.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        private PlayerKeyboardInputController m_playerKeyboardInputController;
        private PlayerMouseInputController m_playerMouseInputController;
        private PlayerMovementController m_playerMovementController;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            m_playerKeyboardInputController = GetComponent<PlayerKeyboardInputController>();
            m_playerMouseInputController = GetComponent<PlayerMouseInputController>();
            m_playerMovementController = GetComponent<PlayerMovementController>();
        }

        public void EnablePlayerInput()
        {
            m_playerKeyboardInputController.enabled = true;
            m_playerMouseInputController.enabled = true;
            m_playerMovementController.enabled = true;
        }

        public void DisablePlayerInput()
        {
            m_playerKeyboardInputController.enabled = false;
            m_playerMouseInputController.enabled = false;
            m_playerMovementController.enabled = false;
        }
    }
}