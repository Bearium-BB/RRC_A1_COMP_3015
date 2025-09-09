using A1;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoverAStartAction : MonoBehaviour
{
    public void MoveAgentStep(MoverAStartModels moverAStartModels)
    {
        AStar aStar = new AStar(moverAStartModels.player.GetGrid());
        List<AStarNode> aStarNodes = aStar.PathFinding(moverAStartModels.start, moverAStartModels.end);
        aStarNodes.Reverse();
        if (aStarNodes.Count != 0)
        {
            moverAStartModels.player.Mover(aStarNodes[0].pos);
        }
    }

}

public class MoverAStartModels
{
    public Player player;
    public Vector2Int start;
    public Vector2Int end;
    public List<Vector2Int> movingWall;
    public MoverAStartModels (Player player, Vector2Int start, Vector2Int end, List<Vector2Int> movingWall = null)
    {
        this.player = player;
        this.start = start;
        this.end = end;
        this.movingWall = movingWall;
    }
}

