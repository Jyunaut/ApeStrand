using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : FoodSource, IInteractable
{
    public void Interact(GameObject user)
    {
        Player.Controller controller = user.GetComponent<Player.Controller>();

        if (!controller.IsNearRaftEdge)
            return;

        if (Inputs.InteractAPress)
        {
            GameObject food = SpawnFood(user.transform.position);
            controller.GrabItem(food);
        }
    }
}
