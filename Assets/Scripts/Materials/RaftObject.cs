using UnityEngine;

public class RaftObject : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        Manager.RaftManager.AddRaftObject(gameObject);
    }

    protected virtual void OnDisable()
    {
        Manager.RaftManager.RemoveRaftObject(gameObject);
    }
}