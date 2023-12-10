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
        [SerializeField] private AudioSource pressedSound;

        private new MeshRenderer renderer;
        private bool isPressed;
        private Vector3 rendererBaseScale;


        private void Awake()
        {
            Assert.IsTrue(GetComponent<Collider>().isTrigger, "PressurePlate needs a trigger collider.");
            renderer = GetComponentInChildren<MeshRenderer>();
            rendererBaseScale= renderer.GetComponent<Transform>().localScale;
        }

        private void OnTriggerEnter(Collider other)
        {
            isPressed = true;
            pressedSound.Play();
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
            renderer.transform.localScale = isPressedDown ? new Vector3(rendererBaseScale.x, rendererBaseScale.y * 0.2f, rendererBaseScale.z) : rendererBaseScale;
            renderer.material = isPressedDown ? pressedMaterial : normalMaterial;
        }
    }
}
