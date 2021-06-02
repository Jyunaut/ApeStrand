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
    

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
    }

    public List<PathNode> SetOpenList()
    {
        List<PathNode> openList = new List<PathNode>();
        
        for (int x = _x - 1; x != _x + 2; x++)
        {
            for (int y = _y - 1; y != _y + 2; y++)
            {
                openList.Add(_grid?.GetGridObject(x, y));
                Debug.Log(_grid?.GetGridObject(x, y));
            }
        }
        return openList;
    }

    public bool TestWalk(Vector3 pos) 
    {
        return false;
    }

    private int CalculateG()
    {
        return 0;
    }

    private int CalculateH()
    {
        return 0;
    }

    private int CalculateF()
    {
        return CalculateG() + CalculateH();
    }
    
    public override string ToString()
    {
        return _x + "," + _y;
    }
}