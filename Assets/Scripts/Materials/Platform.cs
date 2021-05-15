using System;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IDamageable
{
    [SerializeField] private BoxCollider2D _colx;
    [SerializeField] private BoxCollider2D _coly;

    private GameObject _raft;
    private List<GameObject> _neighbours = new List<GameObject>();
    private struct expanded
    {
        public bool right;
        public bool up;
        public bool left;
        public bool down;
    }
    private expanded _expanded;

    void Awake()
    {
        _raft = GameObject.FindGameObjectWithTag("Raft");
    }

    void OnEnable()
    {
        _raft.GetComponent<Raft>().Platforms.Add(gameObject);
        CheckAdjacent();
    }

    void OnDisable()
    {
        _raft.GetComponent<Raft>().Platforms.Remove(gameObject);
    }

    public void TakeDamage()
    {
        CheckAdjacent();
    }

    void RemoveFromRaft() => transform.parent = null;
    void AddToRaft() => transform.parent = _raft.transform;

    void CheckAdjacent()
    {
        if (!_raft) return;

        // Unparent platform so the raycast can detect other platforms
        RemoveFromRaft();

        float expandSize = 0.5f;
        Func<int, bool> IsEven = (int i) => i % 2 == 0;

        // Check all four sides of the platform for any adjacent platforms
        for (int i = 0; i < 4; i++)
        {
            RaycastHit2D[] hits = new RaycastHit2D[2];
            Vector2 direction = new Vector2(Mathf.Cos(i * 90 * Mathf.Deg2Rad), Mathf.Sin(i * 90 * Mathf.Deg2Rad));
            if (IsEven(i))
                _colx.Raycast(direction, hits, _colx.size.x/2 + expandSize, LayerMask.GetMask("Platform"));
            else
                _coly.Raycast(direction, hits, _coly.size.y/2 + expandSize, LayerMask.GetMask("Platform"));

            foreach(var hit in hits)
            {
                // Left and Right
                if (IsEven(i))
                {
                    // Ignore own collider
                    if (hit.collider == _coly) continue;

                    if (hit)
                    {
                        // Neighbour found
                        if (direction.x > 0 && !_expanded.right) 
                        {
                            // Shift offset right
                            _colx.size = new Vector2(_colx.size.x + expandSize, _colx.size.y);
                            _colx.offset = new Vector2(_colx.offset.x + expandSize/2, _colx.offset.y);
                            _expanded.right = true;
                        }
                        else if (direction.x < 0 && !_expanded.left)
                        {
                            // Shift offset left
                            _colx.size = new Vector2(_colx.size.x + expandSize, _colx.size.y);
                            _colx.offset = new Vector2(_colx.offset.x - expandSize/2, _colx.offset.y);
                            _expanded.left = true;
                        }
                        break;
                    }
                    else
                    {
                        // No Neighbour found
                        if (direction.x > 0 && _expanded.right)
                        {
                            // Shift offset back left
                            _colx.size = new Vector2(_colx.size.x - expandSize, _colx.size.y);
                            _colx.offset = new Vector2(_colx.offset.x - expandSize/2, _colx.offset.y);
                            _expanded.right = false;
                        }
                        else if (direction.x < 0 && _expanded.left)
                        {
                            // Shift offset back right
                            _colx.size = new Vector2(_colx.size.x - expandSize, _colx.size.y);
                            _colx.offset = new Vector2(_colx.offset.x + expandSize/2, _colx.offset.y);
                            _expanded.left = false;
                        }
                        break;
                    }   
                } 
                // Up and Down
                else if (!IsEven(i))
                {
                    // Ignore own collider
                    if (hit.collider == _colx) continue;

                    if (hit)
                    {
                        // Neighbour found
                        if (direction.y > 0 && !_expanded.up)
                        {
                            // Shift offset up
                            _coly.size = new Vector2(_coly.size.x, _coly.size.y + expandSize);
                            _coly.offset = new Vector2(_coly.offset.x, _coly.offset.y + expandSize/2);
                            _expanded.up = true;
                        }
                        else if (direction.y < 0 && !_expanded.down)
                        {
                            // Shift offset down
                            _coly.size = new Vector2(_coly.size.x, _coly.size.y + expandSize);
                            _coly.offset = new Vector2(_coly.offset.x, _coly.offset.y - expandSize/2);
                            _expanded.down = true;
                        }
                        break;
                    }
                    else
                    {
                        // No Neighbour found
                        if (direction.y > 0 && _expanded.up)
                        {
                            // Shift offset back down
                            _coly.size = new Vector2(_coly.size.x, _coly.size.y - expandSize);
                            _coly.offset = new Vector2(_coly.offset.x, _coly.offset.y - expandSize/2);
                            _expanded.up = false;
                        }
                        else if (direction.y < 0 && _expanded.down)
                        {
                            // Shift offset back up
                            _coly.size = new Vector2(_coly.size.x, _coly.size.y - expandSize);
                            _coly.offset = new Vector2(_coly.offset.x, _coly.offset.y + expandSize/2);
                            _expanded.down = false;
                        }
                        break;
                    }
                }
            }
        }
        AddToRaft();
    }
}
