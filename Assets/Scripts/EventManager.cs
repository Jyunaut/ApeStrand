using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void TakeDamage();
    public static event TakeDamage Damaged;

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))
        {
            if (Damaged != null)
                Damaged();
        }
    }
}
