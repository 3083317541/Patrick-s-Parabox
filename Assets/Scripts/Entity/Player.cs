using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("网格引用")]
    public UnityEngine.Grid grid;
    [Header("墙壁引用")]
    public List<Wall> walls;
    [Header("箱子引用")]
    public List<Box> boxes;

    [Header("移动设置")]
    public float processTime = 0.8f;

    [Header("玩家状态")]
    public Vector3Int playerIndex;
    public Vector3 playerPosition;

    public bool playerMovable = true;
    public bool isMoving = false;
    
    


    void Start()
    {
        playerMovable = true;
        isMoving = false;

        playerPosition = grid.GetCellCenterWorld(playerIndex);

        transform.position = playerPosition;
    }

    void Update()
    {
        Move();
        Moving();
    }




    //移动判断
    public void Move()
    {
        playerMovable = true;

        //按键（方向）判断
        if (isMoving) return;

        Vector3Int dir = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.A)) dir = Vector3Int.left;
        else if (Input.GetKeyDown(KeyCode.D)) dir = Vector3Int.right;
        else if (Input.GetKeyDown(KeyCode.W)) dir = Vector3Int.up;
        else if (Input.GetKeyDown(KeyCode.S)) dir = Vector3Int.down;

        if (dir == Vector3Int.zero) return;

        //可移动性判断
        foreach (Wall wall in walls)
        {
            if (playerIndex + dir == wall.wallIndex)
            {
                playerMovable = false;
                return;
            }
        }

        foreach (Box box in boxes)
        {
            if (playerIndex + dir == box.boxIndex)
            {
                if (!box.boxMovable)
                {
                    playerMovable = false;
                    return;
                }
            }
        }

        playerIndex += dir;
        playerPosition = grid.GetCellCenterWorld(playerIndex);


    }

    //移动逻辑
    private void Moving()
    {
        if (!playerMovable) return;

        isMoving = true;
        float speed = (grid.cellSize.x / processTime) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed);

        if ( Vector2.Distance(transform.position, playerPosition) <= 0.03f)
        {
            transform.position = playerPosition;
            isMoving = false;
        }
    }
}
