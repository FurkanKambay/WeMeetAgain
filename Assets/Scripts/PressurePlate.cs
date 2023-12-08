using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Collider))]
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField] private bool shouldReset;
        [SerializeField] private float resetSeconds;

        public UnityEvent OnPressed;
        public UnityEvent OnReset;

        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material pressedMaterial;

        private new MeshRenderer renderer;
        private bool isPressed;

        private void Awake()
        {
            Assert.IsTrue(GetComponent<Collider>().isTrigger, "PressurePlate needs a trigger collider.");
            renderer = GetComponentInChildren<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            isPressed = true;
            OnPressed.Invoke();
            UpdateVisuals(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!shouldReset)
                return;

            isPressed = false;
            Invoke(nameof(SafeReset), resetSeconds);
        }

        private void SafeReset()
        {
            if (isPressed)
                return;

            OnReset.Invoke();
            UpdateVisuals(false);
        }

        private void UpdateVisuals(bool isPressedDown)
        {
            transform.localScale = isPressedDown ? new Vector3(1, 0.2f, 1) : Vector3.one;
            renderer.material = isPressedDown ? pressedMaterial : normalMaterial;
        }
    }
}
