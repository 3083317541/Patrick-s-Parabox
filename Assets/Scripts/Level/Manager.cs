using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public UnityEngine.Grid grid;

    public Player player;

    public Vector3Int direction;

    public Dictionary<Vector3Int, Entity> entityMap;
    public List<Entity> movableEntities = new List<Entity>();




    void Start()
    {
        movableEntities.Clear();
        direction = Vector3Int.zero;
        
        InitializeMap();
    }

    void Update()
    {
        direction = InputToDir();
        if (direction == Vector3Int.zero)
            return;

        if (CheckMove(player, direction))
        {
            UpdateMap(movableEntities, direction);
            movableEntities.Clear();
        }
        else
            movableEntities.Clear();

    }




    Vector3Int InputToDir()
    {
        if (Input.GetKeyDown(KeyCode.W))
            return Vector3Int.up;

        if (Input.GetKeyDown(KeyCode.S))
            return Vector3Int.down;

        if (Input.GetKeyDown(KeyCode.A))
            return Vector3Int.left;

        if (Input.GetKeyDown(KeyCode.D))
            return Vector3Int.right;

        return Vector3Int.zero;
    }

    bool CheckMove(Entity entity, Vector3Int dir)
    {
        if (entity == null) 
            return true;


        if (!entity.Movable)
            return false;


            movableEntities.Add(entity);
            return CheckMove(GetEntityAt(entity.Index + dir), dir);
    }

    void ExcuteMove(List<Entity> entities, Vector3Int dir)
    {
        foreach (Entity entity in entities)
            entity.Move(dir);

    }

    Entity GetEntityAt(Vector3Int index)
    {
        if(entityMap.TryGetValue(index, out Entity entity))
        {
            return entity;
        }

        return null;
    }

    void InitializeMap()
    {
        entityMap = new Dictionary<Vector3Int, Entity>();

        Entity[] entities = FindObjectsOfType<Entity>();

        foreach (Entity entity in entities)
        {
            entity.grid = grid;
            entity.Initialize();

            if (entity is Player || entity is Box)
                entity.Movable = true;
            else
                entity.Movable = false;

            entityMap.Add(entity.Index, entity);
        }
    }

    void UpdateMap(List<Entity> entities, Vector3Int dir)
    {
        foreach(Entity entity in entities)
            entityMap.Remove(entity.Index);

        ExcuteMove(entities, dir);

        foreach(Entity entity in entities)
        {
            entityMap.Add(entity.Index, entity);
        }
    }













}
