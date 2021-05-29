using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private Grid<PathNode> _grid;

    public Pathfinding(int width, int height) {
        PathNode n = new PathNode(_grid, 10, 10);
        _grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g,x,y));
    }
}
