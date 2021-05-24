using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Controller))]
    public class PlatformBuild : MonoBehaviour
    {
        [SerializeField]
        private GameObject _platform;
        [SerializeField, Range(0.5f, 2.0f)]
        private float _buildDistance = 1f;
        
        private Controller _controller;
        private CircleCollider2D _circleOrigin;

        void Awake()
        {
            _controller = GetComponent<Controller>();
            _circleOrigin = GetComponent<CircleCollider2D>();
        }

        void Update()
        {
            // Draw ray
            Vector2 origin = (Vector2)transform.position + _circleOrigin.offset;
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, _controller.Direction, _buildDistance, LayerMask.GetMask("Platform"));
            Debug.DrawLine(origin, (Vector2)origin + _controller.Direction, Color.red);

            const float xSpace = 2.5f;
            const float ySpace = 1.75f;
            
            // Check for platform edges
            foreach (RaycastHit2D hit in hits)
            {
                if (!(hit.collider is EdgeCollider2D)) continue;

                var col = hit.collider as EdgeCollider2D;
                // Check left and right walls
                if (col.points[0].x == col.points[1].x)
                {   // Right
                    if (col.points[0].x > 0 && col.points[1].x > 0)
                    {
                        var pos = (Vector2)hit.collider.transform.position + new Vector2(xSpace, 0);
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                            Instantiate(_platform, pos, Quaternion.identity);
                        break;
                    }
                    // Left
                    else
                    {
                        var pos = (Vector2)hit.collider.transform.position + new Vector2(-xSpace, 0);
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                            Instantiate(_platform, pos, Quaternion.identity);
                        break;
                    }
                }
                // Check top and bottom walls
                else
                {   // Top
                    if (col.points[0].y > 0.25f && col.points[1].y > 0.25f) // Hardcoded values
                    {
                        var pos = (Vector2)hit.collider.transform.position + new Vector2(0, ySpace);
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                            Instantiate(_platform, pos, Quaternion.identity);
                        break;
                    }
                    // Bottom
                    else
                    {
                        var pos = (Vector2)hit.collider.transform.position + new Vector2(0, -ySpace);
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                            Instantiate(_platform, pos, Quaternion.identity);
                        break;
                    }
                }
            }
        }
    }
}