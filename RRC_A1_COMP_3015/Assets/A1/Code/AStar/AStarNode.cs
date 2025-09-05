using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
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
        AStarNode currentParent = parent;
        AStarNode oldParent = new AStarNode();

        int countParents = 0;
        while (currentParent != null)
        {
            currentParent = currentParent.parent;
            if (oldParent == currentParent)
            {
                break;
            }
            if (currentParent != null)
            {
                oldParent = currentParent.parent;

            }
            countParents++;
        }
        g = countParents;
    }


    public List<AStarNode> GetAllParent()
    {
        AStarNode currentParent = parent;
        AStarNode oldParent = new AStarNode();
        List <AStarNode> parents = new List <AStarNode>();

        while (currentParent != null)
        {
            currentParent = currentParent.parent;

            if (currentParent == null)
            {
                break;
            }
            oldParent = currentParent.parent;

            parents.Add(currentParent);
        }

        return parents;
    }
}
