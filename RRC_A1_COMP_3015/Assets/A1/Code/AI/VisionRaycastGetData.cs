using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VisionRaycastGetData : MonoBehaviour
{
    public UnityEvent<RaycastHit2D> onHit;
    public float offSet = 0;

    private List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, new Vector2Int(1,1), new Vector2Int(-1, 1), new Vector2Int(1, -1) , new Vector2Int(-1, -1) };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var direction in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(direction.x * offSet, direction.y * offSet), direction,5);
            Debug.DrawRay(transform.position + new Vector3(direction.x * offSet, direction.y * offSet), new Vector3(direction.x * 5, direction.y * 5), Color.green);

            if(hit.collider != null)
            {
                onHit.Invoke(hit);
            }
        }
    }
}
