using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("网格引用")]
    public UnityEngine.Grid grid;

    [Header("玩家引用")]
    public Player player;

    [Header("墙壁引用")]
    public List<Wall> walls;


    [Header("位置设置")]
    public Vector3Int boxIndex;
    public Vector3 boxPosition;
    public Vector3Int targetIndex;

    public bool boxMovable;
    public bool isMoving;




    void Start()
    {
        boxMovable = true;
        isMoving = false;

        boxPosition = grid.GetCellCenterWorld(boxIndex);
        transform.position = boxPosition;

    }

    void Update()
    {
        Move();
        Moving();
    }




    //移动判断
    public void Move()
    {
        boxMovable = true;

        if (isMoving) return;

            Vector3Int dir = Vector3Int.zero;

        //与玩家关系判断
        if (boxIndex == player.playerIndex + Vector3Int.left)
            dir = Vector3Int.left;
        else if (boxIndex == player.playerIndex + Vector3Int.right)
            dir = Vector3Int.right;
        else if (boxIndex == player.playerIndex + Vector3Int.up)
            dir = Vector3Int.up;
        else if (boxIndex == player.playerIndex + Vector3Int.down)
            dir = Vector3Int.down;

        if (dir == Vector3Int.zero) return;


        foreach(Wall wall in walls)
        {
            if(boxIndex + dir == wall.wallIndex)
            {
                boxMovable = false;
                return;
            }
        }
        

        targetIndex = boxIndex + dir;
        
    }

    //移动逻辑
    public void Moving()
    {
        if (!boxMovable) return;

        if (boxIndex == player.playerIndex)
            isMoving = true;

        if (!isMoving) return;

        boxIndex = targetIndex;
        boxPosition = grid.GetCellCenterWorld(boxIndex);

        float speed = (grid.cellSize.x / player.processTime) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, boxPosition, speed);

        if (Vector2.Distance(transform.position, boxPosition) <= 0.03f)
        {
            transform.position = boxPosition;
            isMoving = false;
        }

    }



}
