using System;
using Game.Common;
using UnityEngine;

namespace Game
{
    public class Interactor : MonoBehaviour
    {
        public Action OnInteractRequested;
        public PlayerControl PlayerControl;

        private void Update()
        {
            string buttonName = PlayerControl switch
            {
                PlayerControl.Player1 => "P1 Interact",
                PlayerControl.Player2 => "P2 Interact",
                _ => throw new NotImplementedException()
            };

            if (Input.GetButtonDown(buttonName))
            {
                OnInteractRequested?.Invoke();
            }
        }
    }
}
