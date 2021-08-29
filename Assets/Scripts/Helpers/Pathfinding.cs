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

    public Pathfinding(int width, int height)
    {
        _grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
        closedList = new List<PathNode>();
        openList = new List<PathNode>();
    }

    // TODO: Merge set start+end so that scoring start node initializes properly
    public void SetStartNode(PathNode node)
    {
        _start = node;
        _start.ScoreNode(_start, _end);
        closedList.Add(_start);
        Debug.Log("Start Node: " + _start);
    }

    public void SetEndNode(PathNode node)
    {
        _end = node;
        Debug.Log("End Node: " + _end);
    }

    public void DebugClosedList()
    {
        Debug.Log("===This is my path===");
        for(int n = 0; n <= closedList.Count - 1; n++)
        {
            Debug.Log("Path #" + n + ": " + closedList[n]);
        }
    }

    public void FindPath()
    {
        if(_start != null && _end != null)
        {
            ScoreList();
            DebugClosedList(); // holy god
        }
    }

    private void ScoreList()
    {
        // Score all adjacent nodes
        PathNode parent = closedList[closedList.Count - 1];
        List<PathNode> adjacentNodes = parent.GetAdjacent();
        foreach (PathNode n in adjacentNodes)
        {
            // Score nodes that has not been added to a list
            if(!openList.Contains(n) && !closedList.Contains(n))
            {
                n.ScoreNode(parent, _end);
                openList.Add(n);
            }
        }

        // Find lowest F score
        PathNode lowestF = adjacentNodes[0];
        for(int n = 1; n <= adjacentNodes.Count - 1; n++)
        {
            if(adjacentNodes[n].GetF() < lowestF.GetF())
            {
                lowestF = adjacentNodes[n];
            }
            if(n == adjacentNodes.Count - 1)
            {
                openList.Remove(lowestF);
                closedList.Add(lowestF);
            }
        }
    }

    public PathNode GetNode(Vector3 pos)
    {
        return _grid.GetGridObject(pos);
    }

    public PathNode GetNode(int x, int y)
    {
        return _grid.GetGridObject(x, y);
    }
}
