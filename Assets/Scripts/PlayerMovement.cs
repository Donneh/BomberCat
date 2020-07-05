using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;
    
    public float moveSpeed = 5f;
    public Transform movePoint;
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }
    
    public void Move(Vector2 direction)
    {
        Vector3Int originMovePoint = tilemap.WorldToCell(movePoint.position);
        
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(direction.x) == 1f)
            {
                Vector3Int desiredPosition = tilemap.WorldToCell(originMovePoint) + new Vector3Int((int) direction.x, 0, 0);
                Tile tile = tilemap.GetTile<Tile>(desiredPosition);
                if(tile == null) 
                {
                    movePoint.position = tilemap.GetCellCenterWorld(desiredPosition);
                }
            }
        
            if (Mathf.Abs(direction.y) == 1f)
            {
                Vector3Int desiredPosition = tilemap.WorldToCell(originMovePoint) + new Vector3Int(0, (int) direction.y, 0);
                Tile tile = tilemap.GetTile<Tile>(desiredPosition);
                if (tile == null)
                {
                    movePoint.position = tilemap.GetCellCenterWorld(desiredPosition);
                }
            }
        }
    }
}
