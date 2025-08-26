using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A1;

namespace A1 {

    [RequireComponent(typeof(SpriteRenderer))] // Will force this Scripts GameObjec to have a SpriteRenderer
    public class GridTile : MonoBehaviour {
        private SpriteRenderer _spriteRenderer;

        private bool isWall = false;

        // Add Sprites Later
        /*
        public Sprite openSprite;
        public Sprite wallSprite;
        */

        public bool IsAWall() { return isWall; } // Accessor for wall state. 

        public void Awake() {
            // This should never fail because we have the [Requires.. ] 
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void InitializeSelf(bool isWall) {
            // Sets the art to match the state. Later we might expand this. 
            this.isWall = isWall;// Note the use of this keyword, which refers to the current object. 

            // Otherwise, the isWall parameter would hide the class isWall parameter, and it would not get
            // updated when you try to set it in a very mysterious and hard to debug way. 
            UpdateArt();
        }

        public void UpdateArt() {
            if (isWall) {
                _spriteRenderer.color = Color.black; // Note the use of Static class Color to store colors. 
            } else {
                _spriteRenderer.color = Color.white;
            }
        }
    }
}

