using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A1;

namespace A1 {
    public class GridTesting : MonoBehaviour {
        // [ ] Provide some of this. 
        public bool runPlayerTests = true;
        public bool runGridValidityTests = true;
        public bool runTileWallAccessTests = true;

        // Phase 1 - Player
        public Player testPlayer;

        // Phase 2 - Grid Validity
        public Grid2D grid;

        // Start is called before the first frame update
        void Start() {
            // While you don't strictly need the { } for one line calls, its often good practice to use them. 
            if(runPlayerTests)
                TestPlayer();

            if (runGridValidityTests)
                TestGridValidity();

            if (runTileWallAccessTests) {
                TestGridAccessTest();
            }
        }

        // --- Student Section ---
        // Phase 1
        // Update is called once per frame
        void TestPlayer() {
            // test position 1,1
            testPlayer.SetPlayerPosition(1, 1);
            Debug.Log(testPlayer.ToString());

            // Add more tests here. 
        }

        // Phase 2
        void TestGridValidity() {
            // Init
            Debug.Log("Generating a new Test Grid of size 3,4");
            grid.GenerateNewGrid_WithRandomWalls(3, 4, .5f);
            bool validFlag = false;

            // tests for boundaries - IsValid Tile: 
            validFlag = grid.IsValidTile(0, 0);
            Debug.Log("Testing Position (0,0): Should be True: " + validFlag);

            // More here

        }

        // Phase 2
        void TestGridAccessTest() {
            // Init
            Debug.Log("Starting Testing Grid Access - Generating a new Test Grid of size 5,6");
            grid.GenerateNewGrid_WithRandomWalls(5, 6, 1f); // [ ] Replace with a pattern later
            bool validFlag = false;

            // tests for boundaries - IsValid Tile: 
            Debug.Log(" --- Valid Tests --- ");
            validFlag = grid.IsAWallTile(0, 0);
            Debug.Log("Testing Position (0,0): Should be True: " + validFlag);

            // More here

        }
    }
}

