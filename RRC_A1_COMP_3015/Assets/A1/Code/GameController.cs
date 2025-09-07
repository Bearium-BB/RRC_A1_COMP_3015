using A1;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace A1 {
    public class GameController : MonoBehaviour {

        // Should be private but I'll leave it public so we can find it for now
        public Player currentPlayer;
        public NPC currentNPC;
        public Transform cam;

        public Player thePlayer_Prefab;
        public NPC theNPC_Prefab;

        // Initialization Settings: 

        public int xStartTiles = 6;
        public int yStartTiles = 4;
        // Max of 1
        [Header("Max value of 1f")]
        public float wallProbability = .7f;

        // Must be assigned in the scene first. 
        public Grid2D grid;

        private AStar aStar = new AStar();


        public void Start() {
            // Create the Grid
            grid.GenerateNewGrid_Witch_RandomWalls_And_Win_Point(xStartTiles, yStartTiles, wallProbability);
            currentPlayer = Instantiate(thePlayer_Prefab);
            float NPCX = Random.Range(0, 50);
            float NPCY = Random.Range(0, 50);

            //currentNPC = Instantiate(theNPC_Prefab, new Vector2(NPCX, NPCY), Quaternion.identity);
            //currentNPC.currentPosX = (int)NPCX;
            //currentNPC.currentPosY = (int)NPCY;

            currentPlayer.SetGrid(grid);
            //currentNPC.SetGrid(grid);
            aStar.SetGrid(grid);

            //currentNPC.winPointPos = currentPlayer.transform;

            List<AStarNode> aStarNodes = aStar.PathFinding(Vector2Int.zero, grid.GetWinPointPos());
            if (aStarNodes.Count == 0)
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
        public void Update() 
        {
            cam.position = new Vector3(currentPlayer.transform.position.x, currentPlayer.transform.position.y,cam.position.z);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }
    }
}

