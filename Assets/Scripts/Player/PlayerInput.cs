using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        public float Horizontal => Input.GetAxisRaw("Horizontal");
        public float Vertical   => Input.GetAxisRaw("Vertical");
        public bool Dodge       => Input.GetButtonDown("Dodge");
        public enum Action
        {
            Dodge
        };
    }
}