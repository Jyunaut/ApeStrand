using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : FoodSource, IInteractable
{
    public void Interact(GameObject user)
    {
        if (!user.GetComponent<Player.Controller>().NearRaftEdge) return;
        
        if (Inputs.InteractAPress)
        {
            GameObject food = SpawnFood(user.transform.position);
            user.GetComponent<Player.Controller>().GrabItem(food);
        }
        else if (Inputs.InteractBPress)
        {

        }
    }
}
