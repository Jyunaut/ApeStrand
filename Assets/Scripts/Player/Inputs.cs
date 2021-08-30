using UnityEngine;

public struct Inputs
{
    public static float Horizontal => Player.Controller.ControlsEnabled && Player.Controller.CanMove ? Input.GetAxisRaw("Horizontal") : 0f;
    public static float Vertical   => Player.Controller.ControlsEnabled && Player.Controller.CanMove ? Input.GetAxisRaw("Vertical") : 0f;
    public static bool InteractAPress
    {
        get
        {
            if (!Player.Controller.ControlsEnabled || !Player.Controller.CanInteract)
                return false;
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
                return true;
            return false;
        }
    }
    public static bool InteractBPress
    {
        get
        {
            if (!Player.Controller.ControlsEnabled || !Player.Controller.CanInteract)
                return false;
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
                return true;
            return false;
        }
    }
    public static bool InteractAHold
    {
        get
        {
            if (!Player.Controller.ControlsEnabled || !Player.Controller.CanInteract)
                return false;
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.J))
                return true;
            return false;
        }
    }
    public static bool InteractBHold
    {
        get
        {
            if (!Player.Controller.ControlsEnabled || !Player.Controller.CanInteract)
                return false;
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.K))
                return true;
            return false;
        }
    }
    public static bool IsPressingMovement => Mathf.Abs(Horizontal) > 0.1f || Mathf.Abs(Vertical) > 0.1f;
}