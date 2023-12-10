using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Collider))]
    public class Lever : MonoBehaviour
    {
        [Range(0, 1)]
        public float ToggleSpeed;

        public UnityEvent OnToggleOn;
        public UnityEvent OnToggleOff;

        private Transform stick;
        private Quaternion targetStickRotation;

        private bool isToggledOn;
        [SerializeField] private AudioSource triggeredSound;

        private void Awake()
        {
            Assert.IsTrue(GetComponent<Collider>().isTrigger, "Lever needs a trigger collider.");
            stick = transform.GetChild(0);
        }

        private void Update()
        {
            stick.localRotation = Quaternion.Lerp(stick.localRotation, targetStickRotation, ToggleSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            Interactor interactor = other.GetComponent<Interactor>();
            if (!interactor) return;
            interactor.OnInteractRequested += Toggle;
        }

        private void OnTriggerExit(Collider other)
        {
            Interactor interactor = other.GetComponent<Interactor>();
            if (!interactor) return;
            interactor.OnInteractRequested -= Toggle;
        }

        private void Toggle()
        {
            isToggledOn = !isToggledOn;
            UpdateVisuals();

            if (isToggledOn)
                OnToggleOn.Invoke();
            else
                OnToggleOff.Invoke();
            triggeredSound.Play();
        }

        private void UpdateVisuals()
        {
            targetStickRotation = Quaternion.Euler(isToggledOn ? 45 : -45, 0, 0);
        }
    }
}
