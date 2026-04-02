using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTexturePlacer : MonoBehaviour
{
    public Tilemap tilemap; // 目标 Tilemap
    public TileBase tile; // 要放置的 Tile

    public void FillTilemapWithTexture()
    {
        if (tilemap == null || tile == null)
        {
            Debug.LogError("Tilemap 或 Tile 未设置。");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(position))
                {
                    tilemap.SetTile(position, tile);
                }
            }
        }
    }
}