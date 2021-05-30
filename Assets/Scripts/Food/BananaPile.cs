using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPile : FoodSource, IInteractable
{
    public void Interact(GameObject user, string input)
    {
        switch (input)
        {
            case PlayerInput.Interact_A:
                GameObject food = SpawnFood(user.transform.position);
                user.GetComponent<Player.Controller>().GrabItem(food);
                break;
            case PlayerInput.Interact_B:
                break;
            default:
                Debug.Log("No appropriate input");
                return;
        }
    }
}
