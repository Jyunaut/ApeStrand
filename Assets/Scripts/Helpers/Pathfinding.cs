using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private Grid<PathNode> _grid;
    private PathNode _start;
    private PathNode _end;
    public List<PathNode> openList;
    public List<PathNode> closedList;

    public Pathfinding(int width, int height) {
        _grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g,x,y));
        closedList = new List<PathNode>();
        openList = new List<PathNode>();
    }

    public void SetStartNode(PathNode node) {
        _start = node;
        Debug.Log("Start Node: " + _start);
    }

    public void SetEndNode(PathNode node) {
        _end = node;
        Debug.Log("End Node: " + _end);
    }

    public PathNode GetNode(Vector3 pos) {
        return _grid.GetGridObject(pos);
    }

    public PathNode GetNode(int x, int y) {
        return _grid.GetGridObject(x, y);
    }

    public List<PathNode> FindPath(PathNode start, PathNode end) {
        SetStartNode(start);
        SetEndNode(end);

        List<PathNode> path = new List<PathNode>();
        return path;

    }
}
