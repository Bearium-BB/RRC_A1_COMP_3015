using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A1;

namespace A1 {

    [RequireComponent(typeof(SpriteRenderer))] // Will force this Scripts GameObjec to have a SpriteRenderer
    public class GridTile : MonoBehaviour {
        public Sprite floor;
        public Sprite wall;


        private SpriteRenderer _spriteRenderer;

        private bool isWall = false;

        private bool isWinPoint = false;

        // Add Sprites Later
        /*
        public Sprite openSprite;
        public Sprite wallSprite;
        */

        public bool IsAWall() { return isWall; } // Accessor for wall state. 
        public bool IsAWinPoint() { return isWinPoint; } // Accessor for wall state. 

        public void Awake() {
            // This should never fail because we have the [Requires.. ] 
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void InitializeSelf(bool isWall, bool isWinPoint = false) {
            // Sets the art to match the state. Later we might expand this. 
            this.isWall = isWall;// Note the use of this keyword, which refers to the current object. 
            this.isWinPoint = isWinPoint;
            // Otherwise, the isWall parameter would hide the class isWall parameter, and it would not get
            // updated when you try to set it in a very mysterious and hard to debug way. 
            UpdateArt();
        }

        public void UpdateArt() {
            if (isWall) {
                _spriteRenderer.sprite = wall;
                gameObject.AddComponent<BoxCollider2D>();
            }
            else if (isWinPoint)
            {
                _spriteRenderer.color = new Color(1f, 180f / 255f, 150f);
            } 
            else {
                //_spriteRenderer.color = new Color(0f, 59f / 255f, 63f / 255f);
                _spriteRenderer.sprite = floor;
            }
        }
    }
}

