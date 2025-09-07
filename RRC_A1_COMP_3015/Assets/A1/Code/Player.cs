using UnityEngine;
using A1;
using System.Threading;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

namespace A1 {


    public class Player : MonoBehaviour {

        // Object References
        private Grid2D grid;

        private int currentPosX;
        private int currentPosY;
        private float timePassFromLastInput;

        private AStar aStar = new AStar();
        private Vector2Int winPointPos;

        public GameObject aStarLogNodeInWord;
        List<GameObject> aStarList = new List<GameObject>();
        public List<AStarNode> aStarNodes = new List<AStarNode>();
        // --- Input  --- 

        public void Start()
        {
            aStar.SetGrid(grid);
            winPointPos = grid.GetWinPointPos();
        }

        public void Update() {
            ListenForInput();

            timePassFromLastInput += Time.deltaTime;

            if (timePassFromLastInput >= 0.3f)
            {
                AIProcessingMovement();

                //ProcessInput();
            }
        }

        // [ ] Provide Method structure but let them fill in body. 
        private void ListenForInput() {

            // --- Input --- 


        }

        // ProcessInput will convert the Input data you stored earlier into an
        // X and Y coordinate that represents the next potential move. 
        // Note: You should only have one move at a time here. 
        private void ProcessInput() {
            // [ ] Convert Input into the X and Y values of the position we are trying to move to on the grid.
            Vector3Int movementVector = Vector3Int.zero;
            if (Input.GetKeyDown(KeyCode.W))
            {

                movementVector = new Vector3Int(currentPosX, currentPosY + 1);
                if (ValidateMove(movementVector.x, movementVector.y))
                {
                    SetPlayerPosition(movementVector.x, movementVector.y);
                }
                timePassFromLastInput = 0;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                movementVector = new Vector3Int(currentPosX, currentPosY - 1);
                if (ValidateMove(movementVector.x, movementVector.y))
                {
                    SetPlayerPosition(movementVector.x, movementVector.y);
                }
                timePassFromLastInput = 0;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                movementVector = new Vector3Int(currentPosX - 1, currentPosY);
                if (ValidateMove(movementVector.x, movementVector.y))
                {
                    SetPlayerPosition(movementVector.x, movementVector.y);
                }
                timePassFromLastInput = 0;

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                movementVector = new Vector3Int(currentPosX + 1, currentPosY);
                if (ValidateMove(movementVector.x, movementVector.y))
                {
                    SetPlayerPosition(movementVector.x, movementVector.y);
                }
                timePassFromLastInput = 0;
            }

        }
        private void AIProcessingMovement()
        {
            Debug.Log("AIProcessingMovement");
            if (winPointPos != new Vector2Int(currentPosX,currentPosY))
            {
                List<AStarNode> nodes = aStar.PathFinding(new Vector2Int(currentPosX, currentPosY), winPointPos);
                aStarNodes = nodes;
                if (nodes.Count != 0)
                {
                    nodes.Reverse();

                    foreach (GameObject obj in aStarList)
                    {
                        if (obj != null)
                        {
                            Destroy(obj);
                        }
                    }
                    for(int i = 0; i < nodes.Count; i++)
                    {
                        GameObject obj = Instantiate(aStarLogNodeInWord, new Vector3(nodes[i].pos.x, nodes[i].pos.y,0), Quaternion.identity);
                        aStarList.Add(obj);
                        obj.GetComponent<AStarLogNodeInWord>().g.text = nodes[i].g.ToString();
                        obj.GetComponent<AStarLogNodeInWord>().h.text = nodes[i].h.ToString();
                        obj.GetComponent<AStarLogNodeInWord>().f.text = nodes[i].f.ToString();
                        obj.GetComponent<AStarLogNodeInWord>().pos.text = nodes[i].pos.ToString();
                        obj.GetComponent<AStarLogNodeInWord>().index.text = i.ToString();

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

        // You've converted the input into a target position, make sure that position is valid on the Grid. 
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

        // Sets the position of the Player in the Game world, and updates the local ints. 
        public void SetPlayerPosition(int xTargetPosition, int yTargetPosition)
        {
            currentPosX = xTargetPosition;
            currentPosY = yTargetPosition;
            transform.position = new Vector3(xTargetPosition, yTargetPosition, 0);
        }

        // Supplied Methods: 
        public void SetGrid(Grid2D newGrid) {
            grid = newGrid;
        }

        // Add for testing
        // Override is needed since MonoBehaviour already has a ToString method we will be hiding. 
        // See Polymorphism later in the course for details. 

        public override string ToString() {

            return $"Player Location ({currentPosX},{currentPosY}) and position at {transform.position}";
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

        //public List<AStarNode> pathFinding(Vector2Int startingNode, Vector2Int endNode)
        //{
        //    //GridTile[,] noWallGrid = RemoveWallPositions(grid);

        //    List<AStarNode> notSearchNodes = new List<AStarNode>();
        //    notSearchNodes.Add(new AStarNode(null, startingNode, endNode, startingNode));
        //    List<AStarNode> searchedNodes = new List<AStarNode>();

        //    AStarNode currentnode = notSearchNodes.First();

        //    while (notSearchNodes.Count > 0)
        //    {
        //        if (!ValidateMove(endNode.x, endNode.y))
        //        {
        //            break;
        //        }

        //        List<AStarNode> neighbors = GetNeighbors(currentnode, endNode);

        //        foreach (AStarNode neighbor in neighbors) 
        //        {
        //            if (!searchedNodes.Any(x => x.pos == neighbor.pos))
        //            {
        //                notSearchNodes.Add(neighbor);
        //            }
        //        }

        //        if (neighbors.Count == 0)
        //        {
        //            break;
        //        }

        //        currentnode = notSearchNodes.OrderBy(x => x.f).FirstOrDefault();

        //        notSearchNodes.Remove(currentnode);

        //        searchedNodes.Add(currentnode);

        //        if (currentnode.pos == endNode)
        //        {
        //            Debug.Log("Done");
        //            List<AStarNode> parents = currentnode.GetAllParent();

        //            parents.Reverse();
        //            parents.Add(currentnode);
        //            parents.Reverse();

        //            Debug.Log(parents.Count);
        //            foreach (AStarNode node in parents)
        //            {
        //                Debug.Log(node.pos);
        //            }
        //            return parents;
        //        }
        //    }
        //    return new List<AStarNode>();

        //}

        //public List<AStarNode> GetNeighbors(AStarNode node , Vector2Int endNode)
        //{
        //    List<AStarNode> aStarNodes = new List<AStarNode>();

        //    foreach (Vector2Int item in directions)
        //    {
        //        Vector2Int newPos = node.pos + item;

        //        if (ValidateMove(newPos.x, newPos.y))
        //        {
        //            aStarNodes.Add(new AStarNode(node, newPos, endNode, newPos));
        //        }
        //    }
        //    return aStarNodes;
        //}



    }
}

