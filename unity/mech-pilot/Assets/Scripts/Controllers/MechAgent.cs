using UnityEngine;

namespace Controllers
{
    public class MechAgent : MonoBehaviour
    {
        public Transform head;
        private readonly string _shaderColorReference = "Color_876475b9a8494fc3b31d8b52b532115e";
        private Color _originalColor;
        private Renderer _renderer;

        private void Awake() =>
            // TODO: Complain if head is not set
            _renderer = head.GetComponent<Renderer>();
        private void Start() => _originalColor = _renderer.sharedMaterial.GetColor(_shaderColorReference);

        public void SetColor(Color color) => _renderer.material.SetColor(_shaderColorReference, color);
        public void ResetColor() => _renderer.material.SetColor(_shaderColorReference, _originalColor);
    }
}