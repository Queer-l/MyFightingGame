using UnityEngine;

public class GrassPlacer : MonoBehaviour
{
    public GameObject grassPrefab; // 草地预制体
    public Vector2 areaSize = new Vector2(10, 10); // 区域大小
    public float grassSpacing = 1.0f; // 草地间距

    void Start()
    {
        PlaceGrass();
    }

    void PlaceGrass()
    {
        Vector2 offset = new Vector2(0.5f,0.5f); // 计算起始位置

        if (grassPrefab == null)
        {
            Debug.LogError("Grass prefab is not assigned.");
            return;
        }

        for (float x = 0; x < areaSize.x ; x += grassSpacing)
        {
            for (float y = 0; y < areaSize.y ; y += grassSpacing)
            {
                Vector2 position = new Vector2(x, y) + (Vector2)transform.position + offset; // 计算草地位置
                Instantiate(grassPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}