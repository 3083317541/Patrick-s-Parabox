using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Õ¯∏Ò…Ë÷√")]
    public float gridSize = 5f;

    public Vector2Int GetGridIndex(Vector2 gridPos)
    {
        int x = Mathf.FloorToInt(gridPos.x / gridSize);
        int y = Mathf.FloorToInt(gridPos.y / gridSize);

        return new Vector2Int(x, y);
    }

    public Vector2 GetGridPos(Vector2Int gridIndex)
    {
        float x = gridIndex.x * gridSize + gridSize / 2f;
        float y = gridIndex.y * gridSize + gridSize / 2f;

        return new Vector2(x, y);
    }
}
