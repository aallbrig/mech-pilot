using UnityEngine;

namespace Locomotion
{
    public interface ILocomotion
    {
        void SetNormalizedVector(Vector3 normalizedVector);
        void Stop();
    }

    public class AgentLocomotion : MonoBehaviour, ILocomotion
    {
        public float speed = 3.0f;
        [SerializeField] private Vector3 currentDirection = Vector3.zero;
        private Transform _transform;
        private void Start() => _transform = transform;
        private void Update() => HandleMovement();

        public void SetNormalizedVector(Vector3 normalizedVector) => currentDirection = normalizedVector;
        public void Stop() => currentDirection = Vector3.zero;
        private void HandleMovement()
        {
            if (currentDirection == Vector3.zero) return;

            var projection = _transform.position + currentDirection;
            var newPosition = Vector3.MoveTowards(_transform.position, projection, speed * Time.deltaTime);
            _transform.position = newPosition;
        }
    }
}