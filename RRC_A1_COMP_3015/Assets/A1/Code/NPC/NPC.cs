using A1;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    private Grid2D grid;

    public int currentPosX;
    public int currentPosY;
    private float timePassFromLastInput;

    private AStar aStar = new AStar();
    public Transform winPointPos;


    public void Start()
    {
        aStar.SetGrid(grid);
    }

    public void Update()
    {
        timePassFromLastInput += Time.deltaTime;

        if (timePassFromLastInput >= 0.3f)
        {
            AIProcessingMovement();

        }
    }

    private void AIProcessingMovement()
    {
        if (new Vector2Int((int)winPointPos.position.x, (int)winPointPos.position.y) != new Vector2Int(currentPosX, currentPosY))
        {
            List<AStarNode> nodes = aStar.pathFinding(new Vector2Int(currentPosX, currentPosY), new Vector2Int((int)winPointPos.position.x, (int)winPointPos.position.y));
            if (nodes.Count != 0)
            {
                nodes.Reverse();
                if (nodes.Count - 1 != 0)
                {
                    nodes.RemoveAt(0);
                }
                ValidateMove(nodes[0].pos.x, nodes[0].pos.y);
                SetPlayerPosition(nodes[0].pos.x, nodes[0].pos.y);
                timePassFromLastInput = 0;
            }

        }
        else
        {
            try
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
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

    public void SetPlayerPosition(int xTargetPosition, int yTargetPosition)
    {
        currentPosX = xTargetPosition;
        currentPosY = yTargetPosition;
        transform.position = new Vector3(xTargetPosition, yTargetPosition, 0);
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
}
