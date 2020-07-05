using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;
    
    public Tile wallTile;
    public Tile destructibleTile;

    public GameObject explosion;
    public GameObject ammo;
    public GameObject range;
    public PlayerController player;


    public void Explode(Vector2 worldPos)
    {
        int bombRange = player.getBombRange();
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        
        ExplodeCell(originCell);

        
        
        player.AddBomb();
        
        for (int i = 0; i <= bombRange; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(i, 0, 0)))
            {
                break;
            }
        }
        for (int i = 0; i <= bombRange; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(-i, 0, 0)))
            {
                break;
            }
        }
        for (int i = 0; i <= bombRange; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(0, i, 0)))
            {
                break;
            }
        }
        for (int i = 0; i <= bombRange; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(0, -i, 0)))
            {
                break;
            }
        }
    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == wallTile)
        {
            return false;
        }

        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
            if (Random.Range(0f, 1f) <= 0.3f)
            {
                Instantiate(ammo, tilemap.GetCellCenterWorld(cell), quaternion.identity);
            }
            else if (Random.Range(0f, 1f) <= 0.3f)
            {
                Instantiate(range, tilemap.GetCellCenterWorld(cell), quaternion.identity);
            }
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosion, pos, quaternion.identity);
        return true;
    }
}
