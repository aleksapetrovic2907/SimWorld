using UnityEngine;

namespace AP.Player
{
    public class PlayerAnimationsController : MonoBehaviour
    {
        private Animator m_animator;
        private PlayerKeyboardInputController m_playerKeyboardInputController;
        private PlayerMovementController m_playerMovementController;

        #region ANIMATION_NAMES
        private const string MOVE_ANIMATION = "Movement Blend Tree";
        private const string IDLE_ANIMATION = "Idle Blend Tree";
        #endregion

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_playerKeyboardInputController = GetComponent<PlayerKeyboardInputController>();
            m_playerMovementController = GetComponent<PlayerMovementController>();
        }

        private void Update()
        {
            SetMovementAnimations();
        }

        private void SetMovementAnimations()
        {
            bool moving = m_playerMovementController.enabled && m_playerKeyboardInputController.InputDirection != Vector2.zero;

            m_animator.SetFloat("Horizontal", m_playerKeyboardInputController.LastFacingDirection.x);
            m_animator.SetFloat("Vertical", m_playerKeyboardInputController.LastFacingDirection.y);

            if (moving)
            {
                m_animator.Play(MOVE_ANIMATION);
            }
            else
            {
                m_animator.Play(IDLE_ANIMATION);
            }
        }
    }
}