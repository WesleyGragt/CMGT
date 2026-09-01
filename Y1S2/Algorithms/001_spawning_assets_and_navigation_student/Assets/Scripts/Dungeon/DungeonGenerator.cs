using System;
using System.Collections.Generic;
using Mono.Cecil;
using NaughtyAttributes;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    // TODO EXERCISE 4: Add a reference to the navmesh surface here
    
    [SerializeField]
    private RectInt dungeonBounds;
    
    [SerializeField]
    private List<RectInt> rooms = new List<RectInt>();
    
    [SerializeField]
    private RectInt door;

    [SerializeField]
    private GameObject wall;

    [SerializeField]
    private GameObject floor;
    
    private void Start()
    {
         GenerateDungeon();
         SpawnDungeonAssets();

        // TODO EXERCISE 4: See the BakeNavMesh() method!
        // Call the BakeNavMesh() function here so that the NavMesh is generated after the dungeon is finished
        BakeNavMesh();
    }

    [Button]
    public void GenerateDungeon()
    {
        // Clear the rooms list and our single door...
        rooms.Clear();
        door = RectInt.zero;

        // Split the main room once...
        (RectInt roomA, RectInt roomB) = SplitVertically(dungeonBounds);
        rooms.Add(roomA);
        rooms.Add(roomB);
        
        // Calculate where our door should be ...
        RectInt intersection = AlgorithmsUtils.Intersect(roomA, roomB);
        int randomY = UnityEngine.Random.Range(intersection.y + 1, intersection.y + intersection.height - 1);
        
        door = new RectInt(intersection.x, randomY, intersection.width, intersection.width);

        // Clear all batched drawing calls and redraw our rooms and door...
        DebugDrawingBatcher.GetInstance().ClearAllBatchedCalls();

        DebugDrawingBatcher.GetInstance().BatchCall(() =>
        {
            // Draw the rooms
            foreach (var room in rooms)
            {
                AlgorithmsUtils.DebugRectInt(roomA, Color.red);
                RectInt innerRoomA = new RectInt(roomA.x + 1, roomA.y + 1, roomA.width - 2, roomA.height - 2);
                AlgorithmsUtils.DebugRectInt(innerRoomA, Color.red);

                AlgorithmsUtils.DebugRectInt(roomB, Color.red);
                RectInt innerRoomB = new RectInt(roomB.x + 1, roomB.y + 1, roomB.width - 2, roomB.height - 2);
                AlgorithmsUtils.DebugRectInt(innerRoomB, Color.red);

            }

            // Draw the door
            AlgorithmsUtils.DebugRectInt(door, Color.cyan);
        });
    }
    
    private (RectInt, RectInt) SplitVertically (RectInt pRect)
    {
        RectInt roomA = pRect;
        RectInt roomB = pRect;

        roomA.width = (roomA.width / 2) + UnityEngine.Random.Range(-2, 2);
        roomB.width -= (roomA.width - 1);

        roomB.x += roomA.width - 1;

        return (roomA, roomB);
    }

    [Button]
    public void SpawnDungeonAssets()
    {
        // TODO EXERCISE 1:
        //  - Spawn walls around each room in your generated dungeon
        //  - Fix the visual alignment
        //  - Fix the overlapping cubes
        //  - Fix the missing door...
        //  - Fix the messy hierarchy

        int roomNumb = 0;

        foreach (var room in rooms)
        {
            int roomWidth = room.width;
            int roomHeight = room.height;
            int roomSize = roomWidth * roomHeight;

            int roomX = room.x;
            int roomY = room.y;


            GameObject parentGameObject = new GameObject("Room" + roomNumb);

            int curTile = 0;
            
            int tileY = 0;
            int tileX = 0;
            
            while (curTile < roomSize)
            {
                if (door.x == roomX + tileX && door.y == roomY + tileY)
                {
                    var floorBlock = Instantiate(floor, new Vector3(roomX + tileX + .5f, .5f, roomY + tileY + .5f), Quaternion.identity, parentGameObject.transform);
                    floorBlock.name = $"{roomX + tileX}, 0, {roomY + tileY}";
                } else
                {
                    if (tileX == 0 || tileX == roomWidth - 1 || tileY == 0 || tileY == roomHeight - 1)
                    {
                        var wallBlock = Instantiate(wall, new Vector3(roomX + tileX + .5f, .5f, roomY + tileY + .5f), Quaternion.identity, parentGameObject.transform);
                        wallBlock.name = $"{roomX + tileX}, 0, {roomY + tileY}";
                    } else
                    {
                        var floorBlock = Instantiate(floor, new Vector3(roomX + tileX + .5f, .5f, roomY + tileY + .5f), Quaternion.identity, parentGameObject.transform);
                        floorBlock.name = $"{roomX + tileX}, 0, {roomY + tileY}";
                    }
                }

                curTile++;
                tileX = curTile % roomWidth;
                if (tileX == 0) tileY++;
            }
         
            roomNumb++;
        }
        Debug.Log("TODO EXERCISE 1: Create walls");

        // TODO EXERCISE 3:
        //  - Spawn a floor and test your mouse clicks again from exercise 2
        Debug.Log("TODO EXERCISE 3: Create floor");
    }

    [Button]
    private void BakeNavMesh()
    {
        Debug.Log("TODO EXERCISE 4: Implement the BakeNavMesh() function here:");
    }
}
