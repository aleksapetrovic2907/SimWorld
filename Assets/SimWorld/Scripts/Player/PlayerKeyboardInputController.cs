using UnityEngine;

namespace AP.Player
{
    public class PlayerKeyboardInputController : MonoBehaviour
    {
        public Vector2 InputDirection => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        public Vector2 LastFacingDirection { get; private set; } = Vector2.down;

        private void LateUpdate()
        {
            if (InputDirection == Vector2.zero) { return; }
            LastFacingDirection = InputDirection;
        }
    }
}