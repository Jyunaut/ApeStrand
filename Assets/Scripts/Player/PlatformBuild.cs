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

        private void Awake()
        {
            _controller = GetComponent<Controller>();
            _circleOrigin = GetComponent<CircleCollider2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                BuildPlatform();
            }
        }

        public void BuildPlatform()
        {
                Vector2 origin = (Vector2)transform.position + _circleOrigin.offset;
                RaycastHit2D hit = Physics2D.Raycast(origin, _controller.Direction, _buildDistance, LayerMask.GetMask(Tag.Platform));
                //Debug.DrawLine(origin, (Vector2)origin + _controller.Direction, Color.red);

                const float xSpace = 2.5f;
                const float ySpace = 1.75f;
                
                // Check for platform edges
                if (hit.collider is EdgeCollider2D)
                {
                    var col = hit.collider as EdgeCollider2D;
                    if (col.points[0].x == col.points[1].x)
                    {  
                        // Check left and right walls
                        var pos = col.points[0].x > 0 && col.points[1].x > 0
                                ? (Vector2)col.transform.position + new Vector2(xSpace, 0)
                                : (Vector2)col.transform.position + new Vector2(-xSpace, 0);
                        Instantiate(_platform, pos, Quaternion.identity);
                    }
                    else
                    {
                        // Check top and bottom walls
                        var pos = col.points[0].y > 0.25f && col.points[1].y > 0.25f
                                ? (Vector2)col.transform.position + new Vector2(0, ySpace)
                                : (Vector2)col.transform.position + new Vector2(0, -ySpace);
                        Instantiate(_platform, pos, Quaternion.identity);
                    }
                }
        }
    }
}