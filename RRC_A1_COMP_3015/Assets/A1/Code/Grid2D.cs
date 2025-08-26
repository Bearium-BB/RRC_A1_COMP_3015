using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A1;


namespace A1 {

    /*
     * A class for holding a Grid of tiles. Basically just a Data Structure object.
     */
    public class Grid2D : MonoBehaviour {

        // Removed this from here as I'm setting it from GameController now. 
        // public int xMaxTiles = 6;
        // public int yMaxTiles = 4;

        private GridTile[,] gridOfTiles;

        // Note: Make sure this is set. 
        public GridTile tilePrefabTemplate;

        // Provided - Useful to turn on and off debugging info. 
        public bool DEBUG_MODE = true; 

        // --- Backdoor methods for testing ---
        // Warning: This will break things, use only for testing. 
        public void DeleteTile(int x, int y) {
            Debug.LogWarning("Deleting Tile at Position " + x + ", " + y);
            gridOfTiles[x, y] = null;
        }

        // --- Accessors - Provided Dont Change --- 
        // Returns the Tile at the given position. 
        private GridTile GetTile(int xPosition, int yPosition) {
            // Check the boundaries. 
            if (xPosition >= 0 && xPosition < gridOfTiles.GetLength(0) && yPosition >= 0 && yPosition < gridOfTiles.GetLength(1)) {

                // Grab the tile if appropriate
                GridTile t = gridOfTiles[xPosition, yPosition];
                if (t != null) {
                    return t;
                } else {
                    Debug.LogError("Even though we checked the position, the Tile was null");
                }

            } else {
                // Tile is out of bounds
                Debug.LogError("Tile you tried to access was out of bounds for X: " + xPosition + " Y: " + yPosition);
            }

            return null; // Just to satisfy code paths for Error routes
        }


        // Provided - Don't change
        // Generates a new black grid of Tiles and initializes them. 
        public void GenerateNewGrid(int xSize, int ySize) {

            // Create a new grid
            gridOfTiles = new GridTile[xSize, ySize];

            // Question: Where does this leave the old grid? Do we need to destroy it?             Yes- The Tiles remain in the scene, just the references have been cleared. 

            // Iterate the Grid
            for (int i = 0; i < gridOfTiles.GetLength(0); i++) {
                for (int j = 0; j < gridOfTiles.GetLength(1); j++) {
                    CreateOneTile(i, j, false);
                }
            }
        }

        // Provided - Don't change
        // name is a little long but very descriptive. I'll refactor it later to shorten it
        public void GenerateNewGrid_WithRandomWalls(int xSize, int ySize, float randomChanceTileIsWall) {
            // Mostly same as above but with random chance of being a wall. 
            // Create a new grid
            gridOfTiles = new GridTile[xSize, ySize];

            // Question: Where does this leave the old grid? Do we need to destroy it?             Yes- The Tiles remain in the scene, just the references have been cleared. 

            // Iterate the Grid
            // Since this is a 2D array (not an array of arrays) GetLength(0) is the X value, and GetLength(1) is the Y Value. 
            for (int i = 0; i < gridOfTiles.GetLength(0); i++) {
                for (int j = 0; j < gridOfTiles.GetLength(1); j++) {

                    // same as before but making some a random wall segment. 
                    float randomRoll = Random.value;
                    if (randomRoll < randomChanceTileIsWall) {
                        CreateOneTile(i, j, true);
                    } else {
                        CreateOneTile(i, j, false);
                    }
                }
            }
        }

        // Create with Fixed pattern
        // [ ] For testing

        // Provided - Don't change
        // Instantiate a single Tile with a specific starting value. 
        private void CreateOneTile(int xPosition, int yPosition, bool isWall) {

            // Create a new Tile object from the Prefab. 
            // Because the Prefab reference asks for GridTile_A1, it will create it will that type and we won't have to cast it. 
            GridTile tile = Instantiate(tilePrefabTemplate);

            // Note: Might want to verify it is a valid object, and check that the X and Y position will fit in the grid here. 

            // Add the Tile Object as a reference into the reference grid. 
            gridOfTiles[xPosition, yPosition] = tile; // Could have combined in one step but its helpful for debugging to not (especially when you are starting out). 

            gridOfTiles[xPosition, yPosition].InitializeSelf(isWall);
            // Note: We could have instead used 
            // tile.InitializeSelf(); 
            // Question: Why would both of these options work. 

            // Update the position of the tile we just created. 
            // Create a new Vector 3 to set the tile Position (Z will be 0)
            // Note: Could use a Vector2 as Unity will automatically convert back and
            // forth from V2 to V3 and back when it can. It will simply drop the Z value V3->V2 or add a Z=0 value for V2->V3

            // Once again its helpful when starting out to break down the steps, later not as necessary. 
            // We are also converting the int positions to floats here.
            // Note we are using the "new" keyword to create the Vector3 object. Instantiate will create a GameObject into the Scene, 
            // but if you are creating an object that doesn't derive from GameObject (more on this later), new will be used, similar to other language. 
            Vector3 newPosition = new Vector3(xPosition, yPosition, 0);

            tile.transform.position = newPosition; // Set the position explicitly. 

            // Quality of Life Improvement - Put the position in the GameObjects Name
            // Note that .gameObject (or .transform) is just a shortcut for GetComponent<GameObject>()
            // or GetComponent<Transform>() and is returning a reference to the associated object. 
            tile.gameObject.name = "Tile (" + xPosition + ", " + yPosition + ")"; // See Strings for details on what's happening here. 

            tile.transform.SetParent(transform);// makes the tile a child of this Grid. 
        }



        // TODO: Accessor method for the current size of the grid as a 2D Vector

        // --- Student Section - Complete these methods ---

        // [ ] Returns True if the tile is within the Grid
        public bool IsValidTile(int xPosition, int yPosition) {
            // --- Provided Code - Don't Change me ---
            string debugMessage = "IsValidTile at (" + xPosition + ", " + yPosition + ")";
            bool returnValue = false;

            // --- Student section ---
            

            // --- Provided Code - Don't Change me ---
            Debug.Log(debugMessage);
            return returnValue;
        }

        
        /* Method IsAWallTile(.. ) Check if the given Tile is a Wall Tile or not.
         * It will return the bool state of the referenced Tile. If the location
         * provided is not within the Grid, it should fail gracefully (e.g.Not crash) 
         * and return false, however it should also print a Warning to the Console
         * using the Debug.Warning(..) method if the value given is not within the Grid, 
         * OR if the Tile object doesn’t exist at that location.
         */ 
        public bool IsAWallTile(int xPosition, int yPosition) {
            bool isAWall = false; // Provided flag

            // --- Student Solution Section ---
            

            return isAWall; // Provided return
        }



        // Stretch Goals
        // TODO: [ ] Destroy all tiles before refreshing to allow us to Regenerate the Grid.  
    }
}


