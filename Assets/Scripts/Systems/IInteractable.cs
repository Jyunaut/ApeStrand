using UnityEngine;

public interface IInteractable
{
    float UseDuration { get; }
    void Interact(GameObject user);
}
