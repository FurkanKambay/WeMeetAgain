using Game.Common;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerControl playerControl;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float turnSpeed = 100f;

        private CharacterController character;

        private void Awake() => character = GetComponent<CharacterController>();

        private void Update()
        {
            Vector3 direction = GetMovementDirection();
            Move(direction);
            Turn(direction);
        }

        private void Move(Vector3 direction) => character.Move(direction * (speed * Time.deltaTime));

        private void Turn(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        private Vector3 GetMovementDirection()
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
