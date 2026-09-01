using System;
using System.Collections.Generic;
using System.Text;
using NaughtyAttributes;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Events;
[DefaultExecutionOrder(4)]
public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    private  UnityEvent onDungeonGenerated;
    
    [SerializeField]
    private NavMeshSurface navMeshSurface;
    
    [SerializeField]
    private RectInt dungeonBounds;
    
    [SerializeField]
    private List<RectInt> rooms = new List<RectInt>();
    
    [SerializeField]
    private RectInt door;

    [SerializeField]
    private GameObject roomsParent = null;
    
    [SerializeField]
    private bool generateOnStart = false;
    private void Start()
    {
        if(generateOnStart) {
            GenerateDungeon(); 
        }
    }

    [Button]
    public void GenerateDungeon()
    {
        rooms.Clear();
        door = RectInt.zero;
        DebugDrawingBatcher.GetInstance().ClearAllBatchedCalls();
        
        if (roomsParent != null)
        {
            Destroy(roomsParent);
        }
        
        
        (RectInt roomA, RectInt roomB) = SplitVertically(dungeonBounds);
        rooms.Add(roomA);
        rooms.Add(roomB);
        
        DebugDrawingBatcher.GetInstance().BatchCall( () =>
        {
            foreach (var room in rooms)
            {
                AlgorithmsUtils.DebugRectInt(room, Color.red);
                RectInt innerRoom = new RectInt(room.x + 1, room.y + 1, room.width - 2, room.height - 2);
                AlgorithmsUtils.DebugRectInt(innerRoom, Color.red);
            }
        });
        
        RectInt intersection = AlgorithmsUtils.Intersect(roomA, roomB);
        int randomY = UnityEngine.Random.Range(intersection.y + 1, intersection.y + intersection.height - 1);
        
        door = new RectInt(intersection.x, randomY, intersection.width, intersection.width);
        
        DebugDrawingBatcher.GetInstance().BatchCall( () =>
        {
            AlgorithmsUtils.DebugRectInt(door, Color.cyan);
        });
        
        onDungeonGenerated.Invoke();
    }

    private (RectInt, RectInt) SplitVertically(RectInt pRect)
    {
        RectInt roomA = pRect;
        RectInt roomB = pRect;

        roomA.width = (roomA.width / 2) + UnityEngine.Random.Range(-2, 2);
        roomB.width -= (roomA.width - 1);

        roomB.x += roomA.width - 1;

        return (roomA, roomB);
    }
    
    [Button]
    public void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
    
    public RectInt GetDungeonBounds()
    {
        return dungeonBounds;
    }
    
    public List<RectInt> GetRooms()
    {
        return rooms;
    }

    public RectInt GetDoor()
    {
        return door;
    }
    
    
}
