using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [field: SerializeField] public float HungerRestored { get; private set; }
    [field: SerializeField] public float UseDuration    { get; private set; }

    public void Interact(GameObject user)
    {
        Player.Hunger playerHunger = user.GetComponent<Player.Hunger>();
        Player.Controller playerController = user.GetComponent<Player.Controller>();
        if (Inputs.InteractAPress)
        {
            playerController.GrabItem(gameObject);
        }
        else if (Inputs.InteractBPress)
        {
            if (playerController.HeldItem == null)
            {
                return;
            }
            if (playerHunger != null)
            {
                playerHunger.IncreaseHunger(HungerRestored);
            }
            if (playerController != null)
            {
                playerController.HeldItem = null;
            }
            Destroy(gameObject);
        }
    }
}