using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPile : FoodSource, IInteractable
{
    public void OnDisable()
    {
        Manager.GameManager.Instance.SetGameState(Manager.GameManager.State.Lose);
    }

    public void Interact(GameObject user)
    {
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