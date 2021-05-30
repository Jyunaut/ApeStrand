using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodSource : MonoBehaviour
{
    [field: SerializeField] public GameObject FoodItem { get; private set; }
    [field: SerializeField] public float UseDuration   { get; private set; }

    public Dictionary<Conditions, bool> _interactConditions = new Dictionary<Conditions, bool>
    {
        { Conditions.NearRaftEdge, true }
    };

    public GameObject SpawnFood(Vector2 position)
    {
        return Instantiate(FoodItem, position, Quaternion.identity);
    }
}
