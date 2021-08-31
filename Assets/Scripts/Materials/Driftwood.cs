using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driftwood : MonoBehaviour, IInteractable
{
    [field: SerializeField] public float UseDuration { get; private set; }

    private void FixedUpdate()
    {
        // Drift when in water
        if (!Manager.RaftManager.Instance.RaftObjects.Contains(gameObject))
        {
            Drift();
        }
    }

    public void Interact(GameObject user)
    {
        Player.Controller controller = user.GetComponent<Player.Controller>();
        Player.PlatformBuild platformBuild = user.GetComponent<Player.PlatformBuild>();
        
        if (Inputs.InteractAPress)
        {
            if (controller.HeldItem == null && controller.HeldItem != gameObject)
            {
                controller.GrabItem(gameObject);
            }
        }
        else if (Inputs.InteractBPress)
        {
            if (controller.IsNearRaftEdge)
            {
                platformBuild.BuildPlatform();
                controller.SetState(new Player.UsingItem(controller, UseDuration));
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Must build near water!", this);
            }
        }
    }

    private void Drift()
    {
        
    }
}