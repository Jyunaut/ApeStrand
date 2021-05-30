using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [field: SerializeField] public float HungerRestored { get; private set; }
    [field: SerializeField] public float UseDuration    { get; private set; }

    public void Interact(GameObject user)
    {
        Player.Hunger hunger = user.GetComponent<Player.Hunger>();
        Player.Controller controller = user.GetComponent<Player.Controller>();
        if (hunger != null)
            hunger.IncreaseHunger(HungerRestored);
        if (controller != null)
        {
            controller.UseDuration = UseDuration;
            controller.HeldItem = null;
        }
        Destroy(gameObject);
    }
}