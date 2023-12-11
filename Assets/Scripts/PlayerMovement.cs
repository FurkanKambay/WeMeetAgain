using Game.Common;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerControl playerControl;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float turnSpeed = 100f;
        [SerializeField] private float gravity = -10f;

        private CharacterController character;
        private Transform playerCamera;

        private void Awake()
        {
            character = GetComponent<CharacterController>();
            playerCamera = GetComponentInChildren<Camera>().transform;
        }

        private void Update()
        {
            Vector3 moveInput = GetMovementInput();
            Vector3 turnInput = GetTurnInput();

            var playerRef = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Vector3 direction = playerRef * turnInput;
            Turn(direction);

            var dir = playerRef * moveInput;
            var movement = new Vector3(dir.x, gravity, dir.z);
            Move(movement);
        }

        private void Move(Vector3 direction) => character.Move(direction * (speed * Time.deltaTime));

        private void Turn(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            var targetRotation = Quaternion.LookRotation(direction);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        private Vector3 GetMovementInput()
        {
            return playerControl switch
            {
                PlayerControl.Player1 => new Vector3(0, 0, Input.GetAxisRaw("P1 Vertical")).normalized,
                PlayerControl.Player2 => new Vector3(0, 0, Input.GetAxisRaw("P2 Vertical")).normalized,
                _ => Vector3.zero
            };
        }

        private Vector3 GetTurnInput()
        {
            return playerControl switch
            {
                PlayerControl.Player1 => new Vector3(Input.GetAxisRaw("P1 Horizontal"), 0, 0).normalized,
                PlayerControl.Player2 => new Vector3(Input.GetAxisRaw("P2 Horizontal"), 0, 0).normalized,
                _ => Vector3.zero
            };
        }
    }
}
