using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    private Pathfinding pathfinding;
    public Camera mainCamera;
    // private int[] myArr = {1,2,3,4,5};
    // private List<int> myQueue = new List<int>();

    private void Start()
    {
        pathfinding = new Pathfinding(3,3);
        // foreach (int x in myArr)
        // {
        //     myQueue.Add(x);
        //     // Debug.Log(x);
        // }
        // Debug.Log(myQueue[myQueue.Count - 1]);
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            pathfinding.SetStartNode(pathfinding.GetNode(mouseWorldPosition));
            // print(pathfinding.GetNode(mouseWorldPosition));
        }
        if (Input.GetMouseButtonDown(1))
        {
            pathfinding.SetEndNode(pathfinding.GetNode(mouseWorldPosition));
        }
        if (Input.GetKeyDown("space"))
        {
            // pathfinding.ScoreNodes();
            pathfinding.FindPath();
        }
    }
}