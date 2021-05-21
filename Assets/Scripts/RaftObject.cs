using UnityEngine;

public class RaftObject : MonoBehaviour
{
    protected virtual void Start()
    {
        Manager.RaftManager.Instance.AddRaftObject(this.gameObject);
    }

    protected virtual void OnDestroy()
    {
        Manager.RaftManager.Instance.RemoveRaftObject(this.gameObject);
    }
}