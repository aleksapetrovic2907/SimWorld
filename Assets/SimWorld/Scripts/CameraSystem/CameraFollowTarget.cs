using UnityEngine;

namespace AP.CameraSystem
{
    public class CameraFollowTarget : MonoBehaviour
    {
        public Transform target;

        [SerializeField] private Vector3 offset;
        [SerializeField] private float speed;

        private void LateUpdate()
        {
            if (target == null) { return; }
            transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);
        }
    }
}