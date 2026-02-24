using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    [Header("房间池配置")]
    public List<RoomPool> roomPools = new List<RoomPool>();
    
    [Header("生成设置")]
    public int roomCount = 10;        // 生成房间数量
    public float roomSpacing = 15f;   // 房间间距
    
    [Header("房间预制体")]
    public GameObject roomPrefab;     // 基础房间预制体
    
    private List<Vector2Int> generatedRooms = new List<Vector2Int>();
    private System.Random rng;
    
    void Start()
    {
        rng = SeedManager.Instance.GetRNG();
        GenerateDungeon();
    }
    
    // 生成地牢
    public void GenerateDungeon()
    {
        generatedRooms.Clear();
        
        // 1. 生成起点房间（固定位置）
        Vector2Int startPos = Vector2Int.zero;
        PlaceRoom(startPos, Room.RoomType.Normal);
        
        // 2. 生成其他房间
        Vector2Int currentPosition = startPos;
        for (int i = 1; i < roomCount; i++)
        {
            // 随机选择一个方向
            Vector2Int direction = GetRandomDirection();
            Vector2Int newPosition = currentPosition + direction;
            
            // 检查位置是否已存在房间
            if (!generatedRooms.Contains(newPosition))
            {
                // 根据进度选择房间类型
                Room.RoomType roomType = GetRoomTypeByProgress(i, roomCount);
                PlaceRoom(newPosition, roomType);
                currentPosition = newPosition;
            }
        }
        
        // 3. 在终点放置Boss房
        PlaceRoom(currentPosition, Room.RoomType.Boss);
        
        Debug.Log($"[地牢] 生成完成，共{generatedRooms.Count}个房间");
    }
    
    // 放置房间
    void PlaceRoom(Vector2Int gridPos, Room.RoomType type)
    {
        generatedRooms.Add(gridPos);
        
        // 找到对应的房间池
        RoomPool pool = GetRoomPoolByType(type);
        Room selectedRoom = pool.GetRandomRoom(rng);
        
        // 实例化房间
        if (selectedRoom != null && selectedRoom.roomPrefab != null)
        {
            Vector3 position = new Vector3(gridPos.x * roomSpacing, 0, gridPos.y * roomSpacing);
            Instantiate(selectedRoom.roomPrefab, position, Quaternion.identity);
            Debug.Log($"[房间] 放置 {selectedRoom.roomName} 在 {gridPos}");
        }
        else
        {
            // 如果没有预制体，用基础预制体代替
            Vector3 position = new Vector3(gridPos.x * roomSpacing, 0, gridPos.y * roomSpacing);
            Instantiate(roomPrefab, position, Quaternion.identity);
            Debug.Log($"[房间] 放置基础房间 在 {gridPos}");
        }
    }
    
    // 获取随机方向
    Vector2Int GetRandomDirection()
    {
        int dir = rng.Next(4);
        return dir switch
        {
            0 => Vector2Int.up,
            1 => Vector2Int.down,
            2 => Vector2Int.left,
            3 => Vector2Int.right,
            _ => Vector2Int.up
        };
    }
    
    // 根据进度选择房间类型
    Room.RoomType GetRoomTypeByProgress(int current, int total)
    {
        float progress = (float)current / total;
        float roll = (float)rng.NextDouble();
        
        // 最后一个是Boss房
        if (current == total - 1)
            return Room.RoomType.Boss;
        
        // 根据进度和随机值决定类型
        if (progress < 0.3f)
        {
            // 前期： mostly 普通房
            return roll < 0.8f ? Room.RoomType.Normal : Room.RoomType.Elite;
        }
        else if (progress < 0.6f)
        {
            // 中期： 加入商店和宝箱
            if (roll < 0.6f) return Room.RoomType.Normal;
            if (roll < 0.8f) return Room.RoomType.Elite;
            if (roll < 0.9f) return Room.RoomType.Shop;
            return Room.RoomType.Treasure;
        }
        else
        {
            // 后期： 更多精英房
            if (roll < 0.5f) return Room.RoomType.Normal;
            if (roll < 0.8f) return Room.RoomType.Elite;
            if (roll < 0.9f) return Room.RoomType.Shop;
            return Room.RoomType.Treasure;
        }
    }
    
    // 根据类型获取房间池
    RoomPool GetRoomPoolByType(Room.RoomType type)
    {
        foreach (var pool in roomPools)
        {
            if (pool.rooms.Count > 0 && pool.rooms[0].roomType == type)
            {
                return pool;
            }
        }
        // 兜底返回第一个池子
        return roomPools.Count > 0 ? roomPools[0] : null;
    }
}