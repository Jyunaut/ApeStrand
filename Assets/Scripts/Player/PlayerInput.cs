using UnityEngine;

namespace Player
{
    public struct PlayerInput
    {
        public static float Horizontal  => Input.GetAxisRaw("Horizontal");
        public static float Vertical    => Input.GetAxisRaw("Vertical");
        public static bool Interact     => Input.GetButtonDown("Interact");
        public static bool InteractHold => Input.GetButton("Interact");
    }
}