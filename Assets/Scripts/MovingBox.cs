using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MovingBox : MonoBehaviour
    {
        private float _pushPower = 2f;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.transform.CompareTag("Moving Box"))
            {
                Rigidbody box = hit.collider.GetComponent<Rigidbody>();

                if (box != null)
                {
                    // Use the full move direction instead of just the x-component
                    Vector3 pushDirection = hit.moveDirection.normalized;
                    box.velocity = pushDirection * _pushPower;
                }
            }
        }
    }
}
