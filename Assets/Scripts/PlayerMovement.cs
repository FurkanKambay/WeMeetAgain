using Game.Common;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerControl playerControl;
        [SerializeField] private float speed = 5f;

        private CharacterController character;

        private void Awake() => character = GetComponent<CharacterController>();

        private void Update() => Move();

        private void Move()
        {
            Vector3 direction = GetMovementDirection();
            character.Move(direction * (speed * Time.deltaTime));
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
