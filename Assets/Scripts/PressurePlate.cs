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

        private bool isPressed;

        private void Awake()
            => Assert.IsTrue(GetComponent<Collider>().isTrigger, "PressurePlate needs a trigger collider.");

        private void OnTriggerEnter(Collider other)
        {
            isPressed = true;
            OnPressed.Invoke();
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
            if (!isPressed)
                OnReset.Invoke();
        }
    }
}
