using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    // private Grid<TestObj> grid;
    private Pathfinding pathfinding;
    public Camera mainCamera;

    private void Start()
    {
        // grid = new Grid<TestObj>(3, 3, 10f, Vector3.zero, (Grid<TestObj> g, int x, int y) => new TestObj(g, x, y));
        pathfinding = new Pathfinding(3,3);
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            // TestObj i = grid.GetGridObject(mouseWorldPosition);
            // i.SetObj(Random.Range(0, 10));
            print(pathfinding.GetNode(mouseWorldPosition));
        }
        // if (Input.GetMouseButtonDown(1))
        // {
        //     // print(grid.GetGridObject(mouseWorldPosition));
        // }
    }
}

// public class TestObj
// {
//     private int _x;
//     private int _y;
//     public int value;
//     private Grid<TestObj> _grid;

//     public TestObj(Grid<TestObj> g, int x, int y) { _grid = g; _x = x; _y = y;}

//     public void SetObj(int i)
//     {
//         value = i;
//         _grid.TriggeredGridObjectChanged(_x, _y);
//     }

//     public int GetObj()
//     {
//         return value;
//     }

//     public override string ToString()
//     {
//         return value.ToString();
//     }
// }