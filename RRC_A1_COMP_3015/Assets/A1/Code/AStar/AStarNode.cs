using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AStarNode
{
    public Vector2Int pos;
    public float g = 0;
    public float h = 0;
    public float f = 0;

    public AStarNode parent = null;

    public AStarNode(AStarNode parent , Vector2 currentVector, Vector2 endGoal, Vector2Int pos)
    {
        this.pos = pos;
        this.parent = parent;
        CalculateHCost(currentVector, endGoal);
        CalculateGCost();
        CalculateFCost();
        this.pos = pos;
    }
    public AStarNode()
    {

    }

    public void CalculateFCost()
    {
        f = g + h;
    }

    public void CalculateHCost(Vector2 currentVector, Vector2 endGoal)
    {
        h = (currentVector - endGoal).magnitude;
    }

    public void CalculateGCost()
    {
        AStarNode currentParent = this;
        // We can use a HashSet here because we’re not creating new objects, 
        // we’re reusing existing ones. This way, the HashSet will recognize 
        // duplicates when they’re added.
        HashSet<AStarNode> visited = new HashSet<AStarNode>();

        int countParents = 0;
        while (currentParent != null)
        {
            if (!visited.Add(currentParent))
            {
                break;
            }
            countParents++;
            currentParent = currentParent.parent;

        }
        g = countParents;
    }

    // Using 'this.parent' here can be dangerous because you might accidentally grab a different object 
    // instead of the current one. I figured this out when I ran into an issue where the last node 
    // adjacent to it was never included, and the path skipped straight to the end. 
    // The problem was that I used 'parent' instead of 'this', which started the chain from the 
    // node’s parent instead of from the node itself.
    
    public List<AStarNode> GetAllParent()
    {
        AStarNode currentParent = this;
        HashSet<AStarNode> visited = new HashSet<AStarNode>();
        List<AStarNode> parents = new List<AStarNode> { this };

        while (currentParent != null)
        {

            if (!visited.Add(currentParent))
            {
                break;
            }
            currentParent = currentParent.parent;
            if (currentParent == null)
            {
                break;
            }
            parents.Add(currentParent);
        }

        return parents;
    }
}
