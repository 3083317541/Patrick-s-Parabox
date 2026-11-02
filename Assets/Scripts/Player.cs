using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("ÒÆ¶¯ÉèÖÃ")]
    public float processTime = 0.8f;
    public float gridSize = 5f;

    [Header("Íæ¼Ò×´Ì¬")]
    public Vector2Int playerIndex;
    public Vector2 playerPosition;

    public bool Mobility = true;
    
    


    void Start()
    {
        Mobility = true;

        playerIndex = new Vector2Int(0, 0);

        playerPosition = IndexToPosition(playerIndex);
        transform.position = playerPosition;
    }

    void Update()
    {
        Move();
        Moving();
    }




    //ÒÆ¶¯ÅÐ¶Ï
    public void Move()
    {
        if (!Mobility) return;

        Vector2Int dir = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.A)) dir = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.D)) dir = Vector2Int.right;
        else if (Input.GetKeyDown(KeyCode.W)) dir = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.S)) dir = Vector2Int.down;

        if (dir == Vector2Int.zero) return;

        Mobility = false;
        playerIndex += dir;
        playerPosition = IndexToPosition(playerIndex);

    }

    //ÒÆ¶¯Âß¼­
    private void Moving()
    {
        float speed = (gridSize / processTime) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed);

        if ( Vector2.Distance(transform.position, playerPosition) <= 0.03f)
        {
            transform.position = playerPosition;
            Mobility = true;
        }
    }
    
    //Íø¸ñ×ø±ê×ª»»
    public Vector2 IndexToPosition(Vector2Int index)
    {
        float x = index.x * gridSize + gridSize / 2f;
        float y = index.y * gridSize + gridSize / 2f;

        return new Vector2(x, y);
    }
}
