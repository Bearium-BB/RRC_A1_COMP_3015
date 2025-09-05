using A1;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AStar
{
    private Grid2D grid;

    private List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    public AStar(Grid2D grid)
    {
        this.grid = grid;
    }
    public AStar()
    {

    }
    public void SetGrid(Grid2D grid)
    {
        this.grid = grid;
    }
    public List<AStarNode> pathFinding(Vector2Int startingNode, Vector2Int endNode)
    {
        List<AStarNode> notSearchNodes = new List<AStarNode>();
        notSearchNodes.Add(new AStarNode(null, startingNode, endNode, startingNode));
        List<AStarNode> searchedNodes = new List<AStarNode>();

        AStarNode currentnode = notSearchNodes.First();

        while (notSearchNodes.Count > 0)
        {
            if (!ValidateMove(endNode.x, endNode.y))
            {
                break;
            }

            List<AStarNode> neighbors = GetNeighbors(currentnode, endNode);

            foreach (AStarNode neighbor in neighbors)
            {
                if (!searchedNodes.Any(x => x.pos == neighbor.pos))
                {
                    notSearchNodes.Add(neighbor);
                }
            }

            if (neighbors.Count == 0)
            {
                break;
            }

            currentnode = notSearchNodes.OrderBy(x => x.f).FirstOrDefault();

            notSearchNodes.Remove(currentnode);

            searchedNodes.Add(currentnode);

            if (currentnode.pos == endNode)
            {
                Debug.Log("Done");
                List<AStarNode> parents = currentnode.GetAllParent();

                parents.Reverse();
                parents.Add(currentnode);
                parents.Reverse();

                Debug.Log(parents.Count);
                foreach (AStarNode node in parents)
                {
                    Debug.Log(node.pos);
                }
                return parents;
            }
        }
        return new List<AStarNode>();

    }

    public List<AStarNode> GetNeighbors(AStarNode node, Vector2Int endNode)
    {
        List<AStarNode> aStarNodes = new List<AStarNode>();

        foreach (Vector2Int item in directions)
        {
            Vector2Int newPos = node.pos + item;

            if (ValidateMove(newPos.x, newPos.y))
            {
                aStarNodes.Add(new AStarNode(node, newPos, endNode, newPos));
            }
        }
        return aStarNodes;
    }

    private bool ValidateMove(int xTargetPosition, int yTargetPosition)
    {
        bool isValid = true;

        if (grid.IsAWallTile(xTargetPosition, yTargetPosition))
        {
            isValid = false;
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }
}
