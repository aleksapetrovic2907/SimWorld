using UnityEngine;

namespace AP.Player
{
    public class PlayerMouseInputController : MonoBehaviour
    {
        [SerializeField] private LayerMask raycastLayerMask;
        [SerializeField] private float interactRange;

        private RaycastHit2D m_raycastHit2D;
        private IMouseHoverable m_currentlyHoveredHoverable = null;

        private void Update()
        {
            m_raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, raycastLayerMask);
            CheckForHovers();
            CheckForClicks();
        }

        private void CheckForHovers()
        {
            if (m_raycastHit2D.collider != null && m_raycastHit2D.collider.TryGetComponent<IMouseHoverable>(out var mouseHoverable))
            {
                if (m_currentlyHoveredHoverable == mouseHoverable)
                {
                    mouseHoverable.OnMouseStay(IsInteractableInRange(m_raycastHit2D.collider.transform));
                }
                else
                {
                    mouseHoverable.OnMouseEntered(IsInteractableInRange(m_raycastHit2D.collider.transform));
                    m_currentlyHoveredHoverable = mouseHoverable;
                }

            }
            else if (m_currentlyHoveredHoverable != null)
            {
                m_currentlyHoveredHoverable.OnMouseExited();
                m_currentlyHoveredHoverable = null;
            }
        }

        private void CheckForClicks()
        {
            if (!Input.GetMouseButtonDown(0)) { return; }
            if (m_raycastHit2D.collider == null) { return; }

            if (m_raycastHit2D.collider.TryGetComponent<IMouseInteractable>(out var mouseInteractable))
            {
                mouseInteractable.OnMouseClick(IsInteractableInRange(m_raycastHit2D.collider.transform));
            }
        }

        private bool IsInteractableInRange(Transform target)
        {
            return Vector3.Distance(target.position, transform.position) <= interactRange;
        }

#if UNITY_EDITOR
        [SerializeField] private bool drawRangeGizmos;
        [SerializeField] private Color rangeGizmosColor;

        private void OnDrawGizmos()
        {
            if (!drawRangeGizmos) { return; }

            Gizmos.color = rangeGizmosColor;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
#endif
    }
}