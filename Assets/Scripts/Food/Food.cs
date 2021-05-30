using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [field: SerializeField] public float HungerRestored { get; private set; }
    [field: SerializeField] public float UseDuration    { get; private set; }

    public void Interact(GameObject user, string input)
    {
        Player.Hunger playerHunger = user.GetComponent<Player.Hunger>();
        Player.Controller playerController = user.GetComponent<Player.Controller>();
        switch (input)
        {
            case PlayerInput.Interact_A:
                playerController.GrabItem(gameObject);
                break;
            case PlayerInput.Interact_B:
                if (playerController.HeldItem == null)
                    return;
                if (playerHunger != null)
                    playerHunger.IncreaseHunger(HungerRestored);
                if (playerController != null)
                {
                    playerController.useDuration = UseDuration;
                    playerController.HeldItem = null;
                }
                Destroy(gameObject);
                break;
            default:
                Debug.Log("No appropriate input");
                return;
        }
    }
}