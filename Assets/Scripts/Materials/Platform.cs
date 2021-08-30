using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : RaftObject
{
    [SerializeField]
    private EdgeCollider2D[] _walls = new EdgeCollider2D[4];

    [SerializeField] private List<GameObject> _adjacentPlatforms = new List<GameObject>();
    [SerializeField] private List<GameObject> _objectsOnRaft = new List<GameObject>();

    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        // Update the colliders of this and adjacent platforms
        UpdateColliders();
        foreach (GameObject platform in _adjacentPlatforms)
            platform.GetComponent<Platform>().UpdateColliders();
    }

    protected override void OnDisable()
    {
        foreach (GameObject obj in _objectsOnRaft)
            Destroy(obj);
    }

    private void Update()
    {
        foreach (GameObject obj in _objectsOnRaft)
        {
            Vector2 origin = (Vector2)obj.transform.position + obj.GetComponent<Collider2D>().offset;
            if (!_collider.bounds.Contains(origin)
                || obj.layer == LayerMask.NameToLayer(Layer.IgnorePlatform))
            {
                _objectsOnRaft.Remove(obj);
                break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Vector2 origin = (Vector2)col.transform.position + col.offset;
        if (_collider.bounds.Contains(origin)
            && !_objectsOnRaft.Contains(col.gameObject)
            && col.gameObject.layer != LayerMask.NameToLayer(Layer.IgnorePlatform))
            _objectsOnRaft.Add(col.gameObject);
    }

    private void OnMouseDown()
    {
        DestroyRaft();
    }

    /* ============================================================================================ 
    * Cast a ray in all four directions (right, up, left, down) and check if there is a platform
    * adjacent to this one.  If there is an adjacent platform, disable the corresponding edge collider.
    */
    public void UpdateColliders()
    {
        _adjacentPlatforms = new List<GameObject>();

        const float yOffset  = 1.755f - 0.9225f; // Calculated from the edge collider height
        Vector2 origin = (Vector2)transform.position + new Vector2(0, yOffset);
        const float rotation = 90;
        const float xLen     = 1.5f;
        const float yLen     = 1.25f;
        for (int i = 0; i < 4; i++)
        {
            float angle = i * rotation * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            float length = xLen * Mathf.Abs(direction.x) + yLen * Mathf.Abs(direction.y);
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, length, LayerMask.GetMask("Platform"))
                                           .Where(hit => hit.collider.gameObject != gameObject).ToArray();
            // Debug.DrawLine(origin, origin + new Vector2(xLen * direction.x, yLen * direction.y), Color.red);
            
            // Enable wall collider if no adjacent platform found
            if (hits.Length == 0)
            {
                _walls[i].enabled = true;
                continue;
            }

            // Store a list of adjacent platforms
            foreach (RaycastHit2D hit in hits)
            {
                _walls[i].enabled = false;
                if (!_adjacentPlatforms.Contains(hit.collider.gameObject))
                    _adjacentPlatforms.Add(hit.collider.gameObject);
            }
        }
    }

    private void DestroyRaft()
    {
        Destroy(gameObject);

        // Update the colliders of adjacent platforms
        foreach (GameObject platform in _adjacentPlatforms)
            platform.GetComponent<Platform>().UpdateColliders();
    }
}