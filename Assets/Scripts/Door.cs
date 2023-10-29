using UnityEngine;

namespace Game
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float openAngle = 90f;
        [SerializeField] private float openSpeed = 1f;

        private BoxCollider boxCollider;
        private Transform door;

        private bool isOpen;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
            door = transform.GetChild(0);
        }

        private void Update()
        {
            var targetEuler = new Vector3(0f, isOpen ? openAngle : 0f, 0f);
            door.localEulerAngles = Vector3.Lerp(door.localEulerAngles, targetEuler, openSpeed * Time.deltaTime);
        }

        public void SetOpen(bool value)
        {
            isOpen = value;
            boxCollider.enabled = !value;
        }

        public void Toggle() => SetOpen(!isOpen);
    }
}
