using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;

    void Awake()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        
    }
}