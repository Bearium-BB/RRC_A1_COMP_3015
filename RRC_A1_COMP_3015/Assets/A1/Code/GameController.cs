using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A1;
using UnityEngine.SceneManagement;

namespace A1 {
    public class GameController : MonoBehaviour {

        // Should be private but I'll leave it public so we can find it for now
        public Player currentPlayer;

        public Player thePlayer_Prefab;
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
            currentPlayer.SetGrid(grid);
            aStar.SetGrid(grid);
            List<AStarNode> aStarNodes = aStar.pathFinding(Vector2Int.zero, grid.GetWinPointPos());
            if (aStarNodes.Count == 0)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
    }
}
}

