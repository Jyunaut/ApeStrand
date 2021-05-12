using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _colx;
    [SerializeField] private BoxCollider2D _coly;

    private GameObject _raft;
    private bool _up, _down, _left, _right;

    void Awake()
    {
        _raft = GameObject.FindGameObjectWithTag("Raft");
    }

    void Start()
    {
        CheckNeighbours();
        transform.parent = _raft.transform;
    }
    // TODO: Add Unity Event feature which calls this function each time something happens to the raft
    public void CheckNeighbours()
    {
        if (!_raft) return;

        // Check right
        RaycastHit2D[] hits = new RaycastHit2D[2];
        _colx.Raycast(Vector2.right, hits, _colx.size.x/2 + 0.25f, LayerMask.GetMask("Platform"));
        foreach(var hit in hits)
        {
            if (hit.collider == _coly) continue;
            if (hit && !_right)
            {
                _colx.size = new Vector2(_colx.size.x + 0.25f, _colx.size.y);
                _colx.offset = new Vector2(_colx.offset.x + 0.25f/2, _colx.offset.y);
                _right = true;
                break;
            }
            else if (!hit && _right)
            {
                _colx.size = new Vector2(_colx.size.x - 0.25f, _colx.size.y);
                _colx.offset = new Vector2(_colx.offset.x - 0.25f/2, _colx.offset.y);
                _right = false;
                break;
            }   
        }
        // Check left
        hits = new RaycastHit2D[2];
        _colx.Raycast(Vector2.left, hits, _colx.size.x/2 + 0.25f, LayerMask.GetMask("Platform"));
        foreach(var hit in hits)
        {
            if (hit.collider == _coly) continue;
            if (hit && !_left)
            {
                _colx.size = new Vector2(_colx.size.x + 0.25f, _colx.size.y);
                _colx.offset = new Vector2(_colx.offset.x - 0.25f/2, _colx.offset.y);
                _left = true;
                break;
            }
            else if (!hit && _left)
            {
                _colx.size = new Vector2(_colx.size.x - 0.25f, _colx.size.y);
                _colx.offset = new Vector2(_colx.offset.x + 0.25f/2, _colx.offset.y);
                _left = false;
                break;
            }   
        }
        // Check up
        hits = new RaycastHit2D[2];
        _coly.Raycast(Vector2.up, hits, _coly.size.y/2 + 0.25f, LayerMask.GetMask("Platform"));
        foreach(var hit in hits)
        {
            if (hit.collider == _colx) continue;
            if (hit && !_up)
            {
                _coly.size = new Vector2(_coly.size.x, _coly.size.y + 0.25f);
                _coly.offset = new Vector2(_coly.offset.x, _coly.offset.y + 0.25f/2);
                _up = true;
                break;
            }
            else if (!hit && _up)
            {
                _coly.size = new Vector2(_coly.size.x, _coly.size.y - 0.25f);
                _coly.offset = new Vector2(_coly.offset.x, _coly.offset.y - 0.25f/2);
                _up = false;
                break;
            }   
        }
        // Check down
        hits = new RaycastHit2D[2];
        _coly.Raycast(Vector2.down, hits, _coly.size.y/2 + 0.25f, LayerMask.GetMask("Platform"));
        foreach(var hit in hits)
        {
            if (hit.collider == _colx) continue;
            if (hit && !_down)
            {
                _coly.size = new Vector2(_coly.size.x, _coly.size.y + 0.25f);
                _coly.offset = new Vector2(_coly.offset.x, _coly.offset.y - 0.25f/2);
                _down = true;
                break;
            }
            else if (!hit && _down)
            {
                _coly.size = new Vector2(_coly.size.x, _coly.size.y - 0.25f);
                _coly.offset = new Vector2(_coly.offset.x, _coly.offset.y + 0.25f/2);
                _down = false;
                break;
            }   
        }
    }
}
