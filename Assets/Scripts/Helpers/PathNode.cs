using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> _grid;
    private int _x;
    private int _y;
    private bool _isWalkable;

    public int g;
    public int h;
    public int f;

    public PathNode prevNode;
    public List<PathNode> myNeighbors;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
        myNeighbors = new List<PathNode>();
    }

    public void CheckNeighbors()
    {
        for (int x = _x - 1; x != _x + 2; x++)
        {
            for (int y = _y - 1; y != _y + 2; y++)
            {
                if (_grid.GetGridObject(x,y) != _grid.GetGridObject(_x,_y))
                {
                    myNeighbors.Add(_grid?.GetGridObject(x, y));
                    Debug.Log(_grid?.GetGridObject(x, y));
                }
            }
        }
    }

    public override string ToString()
    {
        return _x + "," + _y;
    }

    public int CalculateG()
    {
        return 0;
    }

    public int CalculateH()
    {
        return 0;
    }

    public int CalculateF()
    {
        return CalculateG() + CalculateH();
    }
}