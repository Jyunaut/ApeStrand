using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Hunger : MonoBehaviour
{
    [field: SerializeField] public float MaxHunger { get; private set; }
    [field: SerializeField] public float CurrentHunger { get; private set; }

    public void DecreaseHunger(float amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Negative values are not allowed");
            return;
        }
        CurrentHunger -= amount;
        if (CurrentHunger < 0)
            CurrentHunger = 0;
    }

    public void IncreaseHunger(float amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Negative values are not allowed");
            return;
        }
        CurrentHunger += amount;
        if (CurrentHunger > MaxHunger)
            CurrentHunger = MaxHunger;
    }

    void Start()
    {
        CurrentHunger = MaxHunger;
    }

    void Update()
    {
        if (CurrentHunger <= 0 && GameManager.GameState != GameManager.State.Lose)
            GameManager.SetGameState(GameManager.State.Lose);
        else if (GameManager.GameState == GameManager.State.Game)
            DecreaseHunger(Time.deltaTime / 2);
    }
}
