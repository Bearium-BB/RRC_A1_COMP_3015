using A1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour , AbleToPathfind
{
    private Grid2D grid;

    public int currentPosX;
    public int currentPosY;
    private float timePassFromLastInput;


    public void Start()
    {

    }

    public void Update()
    {
        Physics.SyncTransforms();
    }

    //private void AIProcessingMovement()
    //{
    //    Debug.Log("AIProcessingMovement");
    //    if (new Vector2Int((int)winPointPos.position.x, (int)winPointPos.position.y) != new Vector2Int(currentPosX, currentPosY))
    //    {
    //        List<AStarNode> nodes = aStar.PathFinding(new Vector2Int(currentPosX, currentPosY), new Vector2Int((int)winPointPos.position.x, (int)winPointPos.position.y));
    //        if (nodes.Count != 0)
    //        {
    //            nodes.Reverse();

    //            ValidateMove(nodes[0].pos.x, nodes[0].pos.y);
    //            SetPosition(nodes[0].pos.x, nodes[0].pos.y);
    //            timePassFromLastInput = 0;
    //        }

    //    }
    //    else
    //    {
    //        try
    //        {
    //            string currentSceneName = SceneManager.GetActiveScene().name;
    //            SceneManager.LoadScene(currentSceneName);
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogError(e);
    //        }
    //    }
    //}

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

    public async void MoveAgent(List<AStarNode> aStarNodes)
    {
        aStarNodes.Reverse();

        for (int i = 0; i < aStarNodes.Count; i++)
        {
            await Timer(0.3f, () => SetPosition(aStarNodes[i].pos.x, aStarNodes[i].pos.y));
        }

    }

    public async void MoveAgent(AStarNode aStarNodes)
    {
        await Timer(0.3f, () => SetPosition(aStarNodes.pos.x, aStarNodes.pos.y));
    }

    public async Task Timer(float duration, Action action)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            await Task.Yield();
        }

        action?.Invoke();
    }

}
