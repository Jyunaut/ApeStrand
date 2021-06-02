using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    private Pathfinding pathfinding;
    public Camera mainCamera;

    private void Start()
    {
        pathfinding = new Pathfinding(3,3);
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            pathfinding.SetStartNode(pathfinding.GetNode(mouseWorldPosition));
            print(pathfinding.GetNode(mouseWorldPosition));
        }
        if (Input.GetMouseButtonDown(1))
        {
            pathfinding.SetEndNode(pathfinding.GetNode(mouseWorldPosition));
        }
    }
}