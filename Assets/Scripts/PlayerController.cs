using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private InputMaster controls;
    
    public Tilemap tilemap;
    
    public GameObject bomb;
    
    public int maxBombs = 1;
    public int currentBombs = 1;
    private int explosionRange = 1;
    
    public Transform movePoint;
    
    void Awake()
    {
        controls = new InputMaster();
        controls.Player.PlaceBomb.performed += ctx => PlaceBomb();
        controls.Player.Movement.performed += 
            ctx => 
                this.gameObject.GetComponent<PlayerMovement>().Move(ctx.ReadValue<Vector2>());
    }
    
    private void Start()
    {
        movePoint.parent = null;
    }
    
    void PlaceBomb()
    {
        if (currentBombs > 0)
        {
            Vector3Int cell = tilemap.WorldToCell(transform.position);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

            Instantiate(bomb, cellCenterPos, Quaternion.identity);
            currentBombs -= 1;
        }
    }
    
    public void AddBomb()
    {
            currentBombs += 1;
    }

    public void gainBomb()
    {
        maxBombs += 1;
        currentBombs += 1;
    }

    public int getBombRange()
    {
        return explosionRange;
    }

    public void increaseBombRange()
    {
        explosionRange += 1;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Explosion"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("BombUp"))
        {
            this.gainBomb();
        }

        if (other.CompareTag("RangeUp"))
        {
            this.increaseBombRange();
        }
            
        Destroy(other.gameObject);
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    
    private void OnDisable()
    {
        controls.Disable();
    }
}
