using UnityEngine;
using A1;

namespace A1 {
    

    public class Player : MonoBehaviour {

        // Object References
        private Grid2D grid;

        private int currentPosX;
        private int currentPosY;

        // --- Input  --- 



        public void Update() {
            ListenForInput();

            ProcessInput();
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
            


        }

        // You've converted the input into a target position, make sure that position is valid on the Grid. 
        private bool ValidateMove(int xTargetPosition, int yTargetPosition) {
            // complete this method. 
            return false;
        }

        // Sets the position of the Player in the Game world, and updates the local ints. 
        public void SetPlayerPosition(int xTargetPosition, int yTargetPosition) {

            // --- Student Solution ---
           


        }

        // Supplied Methods: 
        public void SetGrid(Grid2D newGrid) {
            grid = newGrid;
        }

        // Add for testing
        // Override is needed since MonoBehaviour already has a ToString method we will be hiding. 
        // See Polymorphism later in the course for details. 
        public override string ToString() {
            return "Stuff Here";
        }
    }

}

