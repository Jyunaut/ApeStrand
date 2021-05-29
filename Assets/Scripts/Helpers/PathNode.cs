using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> _grid;
    private int _x;
    private int _y;
    private bool _isWalkable;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode prevNode;
    public List<PathNode> myNeighbors;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
    }

    public void CheckNeighbors()
    {
        for (int x = _x - 1; x != _x + 1; x++)
        {
            for (int y = _y - 1; y != _y + 1; y++)
            {
                myNeighbors.Add(_grid?.GetGridObject(x, y));
                Debug.Log(_grid?.GetGridObject(x, y));
            }
        }
    }

    public int CalculateG()
    {
        int cost = 0;
        return cost;
    }

    public int CalculateH()
    {
        int cost = 0;
        return cost;

    }

    public int CalculateF()
    {
        return CalculateG() + CalculateH();
    }

    public override string ToString()
    {
        return _x + "," + _y;
    }
}
