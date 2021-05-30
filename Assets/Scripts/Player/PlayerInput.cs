using UnityEngine;

public struct PlayerInput
{
    public static float Horizontal    => Input.GetAxisRaw("Horizontal");
    public static float Vertical      => Input.GetAxisRaw("Vertical");
    public const string Interact_A = "Interact A";
    public const string Interact_B = "Interact B";
}