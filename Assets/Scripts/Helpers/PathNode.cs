using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> _grid;
    private int _g;
    private int _h;
    private int _f;
    private int _x;
    private int _y;
    private bool _isBlock;

    public PathNode prevNode;


    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _grid = grid;
        _grid.SetGridObject(x, y, this);
        _x = x;
        _y = y;
        _g = _h = _f = 0;
        _isBlock = false;
    }

    public List<PathNode> GetAdjacent()
    {
        List<PathNode> adjacentNodes = new List<PathNode>();

        for (int x = _x - 1; x != _x + 2; x++)
        {
            for (int y = _y - 1; y != _y + 2; y++)
            {
                if (_grid.GetGridObject(x, y) != null)
                    adjacentNodes.Add(_grid.GetGridObject(x, y));
            }
        }
        adjacentNodes.Remove(this);
        return adjacentNodes;
    }

    public int GetF()
    {
        return _f;
    }

    public void ClearNode()
    {
        _g = _h = _f = 0;
    }

    private int ScoreG(PathNode parent)
    {
        _g = (int)Vector2.SqrMagnitude(new Vector2(_x + parent._x, _y + parent._y));
        return _g;
    }

    private int ScoreH(PathNode end)
    {
        _h = (end._x - _x) + (end._y - _y);
        return _h;
    }

    public int ScoreNode(PathNode parent, PathNode end)
    {
        _f = ScoreG(parent) + ScoreH(end);
        _grid.TriggeredGridObjectChanged(_x, _y);
        return _f;
    }

    public override string ToString()
    {
        return this._x + "," + this._y + ":\n" + " g" + _g + "," + " h" + _h + "," + " f" + _f;
    }
}