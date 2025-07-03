using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 6;
    public int height = 4;
    public GameObject tilePrefab;

    private float tileSpacingX = 1f;
    private float tileSpacingZ = 1f;

    public float tileSpacingPadding = 0.1f;


    private void Start()
    {
        UpdateTileSpacing();
        GenerateGrid();
    }

    void UpdateTileSpacing()
    {
        if (tilePrefab != null)
        {
            Renderer rend = tilePrefab.GetComponent<Renderer>();
            if (rend != null)
            {
                tileSpacingX = rend.bounds.size.x;
                tileSpacingZ = rend.bounds.size.z;
            }
            else
            {
                Debug.LogWarning("Renderer not found on tilePrefab.");
            }
        }
    }

    void GenerateGrid()
{
    for (int x = 0; x < width; x++)
    {
        for (int z = 0; z < height; z++)
        {
            Vector3 pos = new Vector3(
                x * (tileSpacingX + tileSpacingPadding), 
                0, 
                z * (tileSpacingZ + tileSpacingPadding)
            );

            GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            tileObj.name = $"Tile_{x}_{z}";

            Tile tile = tileObj.GetComponent<Tile>();
            tile.gridPos = new Vector2Int(x, z);
            tile.SetTileType(Tile.TileType.Neutral);
        }
    }
}
}
