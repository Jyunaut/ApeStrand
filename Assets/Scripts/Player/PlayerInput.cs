using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        public static float Horizontal => Input.GetAxisRaw("Horizontal");
        public static float Vertical   => Input.GetAxisRaw("Vertical");
        public bool Dodge       => Input.GetButtonDown("Dodge");
        public enum Action
        {
            Dodge
        };
    }
}