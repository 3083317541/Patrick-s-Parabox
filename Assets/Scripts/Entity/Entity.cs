using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public UnityEngine.Grid grid;

    public Vector3Int Index;
    public Vector3 Position;
    public bool Movable;

    public void Initialize()
    {
        Index = grid.WorldToCell(transform.position);

        Position = grid.GetCellCenterWorld(Index);

        transform.position = Position;

    }

    public void Move(Vector3Int direction)
    {
        Index += direction;

        Position = grid.GetCellCenterWorld(Index);

        transform.position = Position;
    }
}
