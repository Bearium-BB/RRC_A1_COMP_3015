using A1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Rendering.Universal;
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

        private float turnTimer = 0;
        private bool turnDecider = false;

        public void Start() {
            // Create the Grid
            grid.GenerateNewGrid_Witch_RandomWalls_And_Win_Point(xStartTiles, yStartTiles, wallProbability);

            currentPlayer = Instantiate(thePlayer_Prefab);
            float NPCX = Random.Range(0, xStartTiles);
            float NPCY = Random.Range(0, yStartTiles);

            currentNPC = Instantiate(theNPC_Prefab, new Vector2(NPCX, NPCY), Quaternion.identity);
            currentNPC.currentPosX = (int)NPCX;
            currentNPC.currentPosY = (int)NPCY;

            currentPlayer.SetGrid(grid);
            currentPlayer.transformNPC = currentNPC.transform;
            currentNPC.SetGrid(grid);
            aStar.SetGrid(grid);

            List<AStarNode> aStarNodes = aStar.PathFinding(Vector2Int.zero, grid.GetWinPointPos());
            if (aStarNodes.Count == 0)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);

            }


        }
        public void Update() 
        {
            turnTimer += Time.deltaTime;

            if (turnTimer >= 0.5f)
            {
                turnDecider = !turnDecider;
                if (turnDecider)
                {
                    currentPlayer.WhatToDO();
                }
                else
                {
                    List<AStarNode> aStarNodes = aStar.PathFinding(currentNPC.GetPosition(), currentPlayer.GetPosition());
                    aStarNodes.Reverse();
                    currentNPC.MoveAgent(aStarNodes[0]);
                }
                turnTimer = 0;
            }

            if (currentPlayer.GetPosition() == grid.GetWinPointPos())
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }

        public void LateUpdate()
        {
            cam.position = new Vector3(currentPlayer.transform.position.x, currentPlayer.transform.position.y, cam.position.z);
        }
    }
}

