using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    private Grid<objGrid> grid;
    public Camera mainCamera;

    private void Start()
    {
        grid = new Grid<objGrid>(3, 3, 10f, Vector3.zero, (Grid<objGrid> g, int x, int y) => new objGrid(g, x, y));
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            objGrid i = grid.GetGridObject(mouseWorldPosition);
            i.SetObj(Random.Range(0, 10));
            grid.SetGridObject(mouseWorldPosition, i);
        }
        if (Input.GetMouseButtonDown(1))
        {
            print(grid.GetGridObject(mouseWorldPosition));
        }
    }
}

public class objGrid
{
    private int _x;
    private int _y;
    public int value;
    private Grid<objGrid> _grid;

    public objGrid(Grid<objGrid> g, int x, int y) { _grid = g; _x = x; _y = y;}

    public void SetObj(int i)
    {
        value = i;
        _grid.TriggeredGridObjectChanged(_x, _y);
    }

    public int GetObj()
    {
        return value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}