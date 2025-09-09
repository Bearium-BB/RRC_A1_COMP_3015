using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface AbleToPathfind
{
    public void MoveAgent(List<AStarNode> aStarNodes);
    public void MoveAgent(AStarNode aStarNodes);
}
