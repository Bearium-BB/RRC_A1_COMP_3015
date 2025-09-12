using A1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace A1 {


    public class Player : MonoBehaviour{

        // Object References
        private Grid2D grid;

        private int currentPosX;
        private int currentPosY;
        private float timePassFromLastInput;

        public GameObject aStarLogNodeInWordGameObject;

        public Transform transformNPC;

        public UnityEvent<MoverAStartModels> onSeeNPC;
        public UnityEvent<MoverAStartModels> notSeeNPC;

        bool seeNPC;
        // --- Input  --- 

        public void Update() {
            ListenForInput();

            timePassFromLastInput += Time.deltaTime;

            if (timePassFromLastInput >= 0.3f)
            {
                ProcessInput();
            }

            if (HowCloseIsNPC() <= 5f)
            {
                seeNPC = true;
            }
            else
            {
                seeNPC = false;
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
            Vector2Int movementVector = Vector2Int.zero;
            if (Input.GetKeyDown(KeyCode.W))
            {
                movementVector = new Vector2Int(currentPosX, currentPosY + 1);

                Mover(movementVector);

                timePassFromLastInput = 0;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                movementVector = new Vector2Int(currentPosX, currentPosY - 1);

                Mover(movementVector);

                timePassFromLastInput = 0;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                movementVector = new Vector2Int(currentPosX - 1, currentPosY);

                Mover(movementVector);

                timePassFromLastInput = 0;

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                movementVector = new Vector2Int(currentPosX + 1, currentPosY);

                Mover(movementVector);

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

        public void Mover(Vector2Int movementVector)
        {
            if (ValidateMove(movementVector.x, movementVector.y))
            {
                SetPosition(movementVector.x, movementVector.y);
            }
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

        public Grid2D GetGrid()
        {
            return grid;
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


        public void SeeNPCAction()
        {

            List<Vector2Int> movingWall = new List<Vector2Int> { new Vector2Int((int)transformNPC.position.x, (int)transformNPC.position.y) };
            List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (Vector2Int direction in directions)
            {
                movingWall.Add(movingWall[0] + direction);
  
            }
            MoverAStartModels moverAStartModels = new MoverAStartModels(this, new Vector2Int(currentPosX, currentPosY), grid.GetWinPointPos(), movingWall);

            onSeeNPC.Invoke(moverAStartModels);
            
        }

        public void GoToWinPoint()
        {
            MoverAStartModels moverAStartModels = new MoverAStartModels(this, new Vector2Int(currentPosX, currentPosY), grid.GetWinPointPos());
            notSeeNPC.Invoke(moverAStartModels);
        }

        public void WhatToDO()
        {
            if (!seeNPC)
            {
                GoToWinPoint();
            }
            else 
            {
                SeeNPCAction();
            }
        }

        public float HowCloseIsNPC()
        {
            return Vector2.Distance(transformNPC.position, transform.position);
        }


    }
}

