using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [field: SerializeField] public float HungerRestored { get; private set; }
    [field: SerializeField] public float UseDuration    { get; private set; }

    public void Interact(GameObject user)
    {
        Player.Hunger hunger = user.GetComponent<Player.Hunger>();
        Player.Controller controller = user.GetComponent<Player.Controller>();
        
        if (Inputs.InteractAPress)
        {
            if (controller.HeldItem == null && controller.HeldItem != gameObject)
            {
                controller.GrabItem(gameObject);
            }
        }
        else if (Inputs.InteractBPress)
        {
            if (controller.HeldItem == gameObject)
            {
                hunger.IncreaseHunger(HungerRestored);
                controller.HeldItem = null;
                Destroy(gameObject);
            }
        }
    }
}