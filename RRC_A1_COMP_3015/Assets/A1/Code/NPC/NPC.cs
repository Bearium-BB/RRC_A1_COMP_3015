using A1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    private Grid2D grid;

    public int currentPosX;
    public int currentPosY;
    private float timePassFromLastInput;


    public void Start()
    {

    }

    private bool ValidateMove(int xTargetPosition, int yTargetPosition)
    {
        bool isValid = true;

        if (grid.IsAWallTile(xTargetPosition, yTargetPosition))
        {
            StartCoroutine(InvalidMoveIndicator());
            isValid = false;
        }
        else
        {
            StartCoroutine(validMoveIndicator());
            isValid = true;
        }

        return isValid;
    }

    public void SetPosition(int xTargetPosition, int yTargetPosition)
    {
        currentPosX = xTargetPosition;
        currentPosY = yTargetPosition;
        transform.position = new Vector3(xTargetPosition, yTargetPosition, 0);
    }
    public Vector2Int GetPosition()
    {
        return new Vector2Int(currentPosX, currentPosY);
    }

    public void SetGrid(Grid2D newGrid)
    {
        grid = newGrid;
    }
 

    public override string ToString()
    {

        return $"NPC Location ({currentPosX},{currentPosY}) and position at {transform.position}";
    }

    IEnumerator InvalidMoveIndicator()
    {
        SpriteRenderer spriteRenderer;
        TryGetComponent(out spriteRenderer);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;

        }
        yield return new WaitForSeconds(0.25f);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0f, 78f / 255f, 80f / 255f);

        }

        yield return null;

    }
    IEnumerator validMoveIndicator()
    {
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

        yield return new WaitForSeconds(0.25f);

        transform.localScale = new Vector3(1, 1, 1);

        yield return null;

    }

    public void MoveAgent(List<AStarNode> aStarNodes)
    {
        aStarNodes.Reverse();

        for (int i = 0; i < aStarNodes.Count; i++)
        {
            SetPosition(aStarNodes[i].pos.x, aStarNodes[i].pos.y);
        }

    }

    public void MoveAgent(AStarNode aStarNode)
    {
        SetPosition(aStarNode.pos.x, aStarNode.pos.y);
    }

}
