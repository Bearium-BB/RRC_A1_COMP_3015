using UnityEngine;
using A1;
using System.Threading;
using System.ComponentModel;
using System.Collections;

namespace A1 {


    public class Player : MonoBehaviour {

        // Object References
        private Grid2D grid;

        private int currentPosX;
        private int currentPosY;
        private float timePassFromLastInput;
        // --- Input  --- 

        public void Update() {
            ListenForInput();

            timePassFromLastInput += Time.deltaTime;

            if (timePassFromLastInput >= 0.3f)
            {
                ProcessInput();
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

        // You've converted the input into a target position, make sure that position is valid on the Grid. 
        private bool ValidateMove(int xTargetPosition, int yTargetPosition) 
        {
            bool isValid = grid.IsAWallTile(xTargetPosition, yTargetPosition);

            if (!isValid)
            {
                StartCoroutine(InvaleMoveIndicator());
            }
            else 
            {
                StartCoroutine(valeMoveIndicator());
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

        IEnumerator InvaleMoveIndicator()
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
        IEnumerator valeMoveIndicator()
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

            yield return new WaitForSeconds(0.25f);

            transform.localScale = new Vector3(1, 1, 1);

            yield return null;

        }
    }
}

