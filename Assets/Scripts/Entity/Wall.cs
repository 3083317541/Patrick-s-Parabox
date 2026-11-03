using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header("Íø¸ñÒýÓÃ")]
    public UnityEngine.Grid grid;
    [Header("Ç½±Ú×´Ì¬")]
    public Vector3Int wallIndex;
    public Vector3 wallPosition;

    void Start()
    {
        wallPosition = grid.GetCellCenterWorld(wallIndex);

        transform.position = wallPosition;
    }
}
