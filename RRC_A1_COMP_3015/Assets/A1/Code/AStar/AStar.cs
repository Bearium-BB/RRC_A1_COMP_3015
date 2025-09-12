using A1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Callbacks;
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

    public List<AStarNode> PathFinding(Vector2Int startingNode, Vector2Int endNode , List<Vector2Int> movingWall = null)
    {
        List<AStarNode> notSearchNodes = new List<AStarNode>();
        notSearchNodes.Add(new AStarNode(null, startingNode, endNode, startingNode));
        List<AStarNode> searchedNodes = new List<AStarNode>();

        AStarNode currentnode = notSearchNodes.First();

        while (notSearchNodes.Count > 0)
        //for (int i = 0; i < 10000; i++)
        {
            if (!ValidateMove(endNode.x, endNode.y))
            {
                break;
            }

            if (!ValidateMove(startingNode.x, startingNode.y))
            {
                break;
            }


            List<AStarNode> neighbors = GetNeighbors(currentnode, startingNode, endNode, movingWall);

            foreach (AStarNode neighbor in neighbors)
            {
                // I was baffled by an issue I couldn’t figure out for the longest time. 
                // At first, I was only checking against the searched nodes. 
                // There would be neighbors waiting in the list to be searched. 
                // If a node was placed right beside them and the area was searched again, 
                // it could pick up another neighbor that wasn’t meant to be there, 
                // since it was already in the searched node list.                    
                // so duplicates would sneak in since they weren’t searched yet, 
                // meaning they could be added to the not-searched list again. 
                // You have to check both to prevent memory leaks from happening.
                if (!searchedNodes.Any(x => x.pos == neighbor.pos) && !notSearchNodes.Any(x => x.pos == neighbor.pos))
                {
                    notSearchNodes.Add(neighbor);
                }
                //else
                //{
                //    neighbor.parent = currentnode;
                //}
            }

            if (neighbors.Count == 0)
            {
                break;
            }


            if (currentnode != null)
            {
                if (currentnode.pos == endNode)
                {
                    List<AStarNode> parents = currentnode.GetAllParent();

                    AStarNode node = parents.Where(x => x.pos == startingNode).FirstOrDefault();
                    if (node != null)
                    {
                        parents.Remove(node);
                    }
                    else
                    {
                        Debug.Log("Did not remove starting node");
                    }

                    return parents;
                }
            }


            currentnode = notSearchNodes.OrderBy(x => x.f).FirstOrDefault();

            notSearchNodes.Remove(currentnode);

            if (currentnode != null)
            {
                if (!searchedNodes.Any(x => x.pos == currentnode.pos))
                {
                    searchedNodes.Add(currentnode);
                }
            }

        }
        return new List<AStarNode>();



    }

    public List<AStarNode> GetNeighbors(AStarNode node, Vector2Int startNode, Vector2Int endNode , List<Vector2Int> movingWall = null)
    {
        if (movingWall != null)
        {
            Debug.Log($"GetNeighbors {movingWall.Count}");
        }
        List<AStarNode> aStarNodes = new List<AStarNode>();

        foreach (Vector2Int item in directions)
        {
            if (node != null)
            {
                Vector2Int newPos = node.pos + item;

                if (ValidateMove(newPos.x, newPos.y))
                {

                    if (movingWall != null)
                    {
                        if (!movingWall.Any(x => x == newPos || newPos == startNode))
                        {
                            aStarNodes.Add(new AStarNode(node, newPos, endNode, newPos));
                        }
                    }
                    else
                    {
                        aStarNodes.Add(new AStarNode(node, newPos, endNode, newPos));

                    }
                }
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
