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
using System.Threading.Tasks;

namespace A1 {


    public class Player : MonoBehaviour{

        // Object References
        private Grid2D grid;

        private int currentPosX;
        private int currentPosY;
        private float timePassFromLastInput;

        public GameObject aStarLogNodeInWordGameObject;
        // --- Input  --- 

        public void Update() {
            ListenForInput();

            timePassFromLastInput += Time.deltaTime;

            if (timePassFromLastInput >= 0.3f)
            {
                ProcessInput();
            }
            Physics.SyncTransforms();
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
                    SetPosition(movementVector.x, movementVector.y);
                }
                timePassFromLastInput = 0;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                movementVector = new Vector3Int(currentPosX, currentPosY - 1);
                if (ValidateMove(movementVector.x, movementVector.y))
                {
                    SetPosition(movementVector.x, movementVector.y);
                }
                timePassFromLastInput = 0;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                movementVector = new Vector3Int(currentPosX - 1, currentPosY);
                if (ValidateMove(movementVector.x, movementVector.y))
                {
                    SetPosition(movementVector.x, movementVector.y);
                }
                timePassFromLastInput = 0;

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                movementVector = new Vector3Int(currentPosX + 1, currentPosY);
                if (ValidateMove(movementVector.x, movementVector.y))
                {
                    SetPosition(movementVector.x, movementVector.y);
                }
                timePassFromLastInput = 0;
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
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            }
            yield return new WaitForSeconds(0.25f);

            if (spriteRenderer != null)
            {
                transform.localScale = new Vector3(1, 1, 1);

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

        public void SeeNPC(RaycastHit2D hit)
        {
            Vector3 opposingDirectionForNPC = -(transform.position - hit.collider.transform.position).normalized;
            if (hit.collider.tag == "NPC")
            {
                Debug.Log(opposingDirectionForNPC);
            }
        }
    }
}

