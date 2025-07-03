using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    public bool isOccupied = false;
    public bool isSpawnable = true;

    public TileType tileType = TileType.Neutral;

    public enum TileType
    {
        Neutral,
        Player,
        Enemy
    }

    public Material neutralMat;
    public Material playerMat;
    public Material enemyMat;

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            // 자식에서 찾기 (자식에 MeshRenderer가 있는 경우)
            rend = GetComponentInChildren<Renderer>();
        }

        if (rend == null)
        {
            Debug.LogError("Renderer not found on Tile object.");
        }
    }

    public void SetTileType(TileType newType)
    {
        tileType = newType;

        if (rend == null)
        {
            Debug.LogWarning("Cannot set material: Renderer is null.");
            return;
        }

        switch (tileType)
        {
            case TileType.Neutral:
                rend.material = neutralMat;
                break;
            case TileType.Player:
                rend.material = playerMat;
                break;
            case TileType.Enemy:
                rend.material = enemyMat;
                break;
        }
    }

    /* void OnMouseDown()
     {
             Debug.Log("dddd");
         // 클릭 시 상태 순환: Neural → Player → Enemy → Neural
             switch (tileType)
             {
                 case TileType.Neutral:
                     SetTileType(TileType.Player);
                     break;
                 case TileType.Player:
                     SetTileType(TileType.Enemy);
                     break;
                 case TileType.Enemy:
                     SetTileType(TileType.Neutral);
                     break;
             }
     }*/
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Tile] Triggered by {other.name}");

        CycleTileType();
    }

    public void CycleTileType()
    {
        switch (tileType)
        {
            case TileType.Neutral:
                SetTileType(TileType.Player);
                break;
            case TileType.Player:
                SetTileType(TileType.Enemy);
                break;
            case TileType.Enemy:
                SetTileType(TileType.Neutral);
                break;
        }
    }


}
