using UnityEngine;

namespace Player
{
    public struct PlayerInput
    {
        public static float Horizontal    => Input.GetAxisRaw("Horizontal");
        public static float Vertical      => Input.GetAxisRaw("Vertical");
        public static bool Interact_A     => Input.GetButtonDown("Interact A");
        public static bool Interact_B     => Input.GetButtonDown("Interact B");
        public static bool InteractHold_A => Input.GetButton("Interact A");
        public static bool InteractHold_B => Input.GetButton("Interact B");
    }
}