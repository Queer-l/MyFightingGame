using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapAutoFill : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private TileBase fillTile;

    [Header("Fill Settings")]
    [Tooltip("Inclusive min X/Y, exclusive max X/Y in cell coordinates.")]
    [SerializeField] private Vector2Int minCell = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int maxCell = new Vector2Int(10, 10);

    [SerializeField] private bool fillOnStart = true;

    private void Start()
    {
        if (fillOnStart)
        {
            Fill();
        }
    }

    [ContextMenu("Fill")]
    public void Fill()
    {
        if (targetTilemap == null || fillTile == null)
        {
            Debug.LogWarning("TilemapAutoFill: targetTilemap or fillTile is not set.");
            return;
        }

        for (int y = minCell.y; y < maxCell.y; y++)
        {
            for (int x = minCell.x; x < maxCell.x; x++)
            {
                targetTilemap.SetTile(new Vector3Int(x, y, 0), fillTile);
            }
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        if (targetTilemap == null)
        {
            Debug.LogWarning("TilemapAutoFill: targetTilemap is not set.");
            return;
        }

        targetTilemap.ClearAllTiles();
    }
}
