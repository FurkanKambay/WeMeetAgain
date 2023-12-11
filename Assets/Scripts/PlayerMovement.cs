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
        private Transform playerCamera; // Use a separate variable for the player's camera

        private void Awake()
        {
            character = GetComponent<CharacterController>();
            playerCamera = transform.GetChild(0); // Assuming the camera is the first child of the player
        }

        private void Update()
        {
            Vector3 inputDirection = GetMovementInput();

            // Use local rotation of the player's camera instead of the main camera
            Vector3 movementDirection = Quaternion.Euler(0, playerCamera.eulerAngles.y, 0) * inputDirection;
            Turn(movementDirection);

            movementDirection.y = gravity;
            Move(movementDirection);
        }

        private void Move(Vector3 direction) => character.Move(direction * (speed * Time.deltaTime));

        private void Turn(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        private Vector3 GetMovementInput()
        {
            return playerControl switch
            {
                PlayerControl.Player1 => new Vector3(Input.GetAxisRaw("P1 Horizontal"), 0, Input.GetAxisRaw("P1 Vertical")).normalized,
                PlayerControl.Player2 => new Vector3(Input.GetAxisRaw("P2 Horizontal"), 0, Input.GetAxisRaw("P2 Vertical")).normalized,
                _ => Vector3.zero
            };
        }
    }
}
