using UnityEngine;

namespace Locomotion
{
    public class AgentLocomotion : MonoBehaviour
    {
        public float speed = 3.0f;
        private Vector3 _normalizedVector = Vector3.zero;
        private Transform _transform;

        public void SetNormalizedVector(Vector3 normalizedVector) => _normalizedVector = normalizedVector;
        public void Stop() => _normalizedVector = Vector3.zero;
        private void HandleMovement()
        {
            if (_normalizedVector == Vector3.zero) return;
            else
                _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _normalizedVector,
                    speed * Time.deltaTime);
        }
        private void Start() => _transform = transform;
        private void Update() => HandleMovement();
    }
}