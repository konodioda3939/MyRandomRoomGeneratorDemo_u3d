using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RoomPool
{
    public string poolName;           // 池子名称
    public List<Room> rooms = new List<Room>();  // 房间列表
    
    // 根据权重随机选择一个房间
    public Room GetRandomRoom(System.Random rng)
    {
        if (rooms.Count == 0) return null;
        
        // 计算总权重
        float totalWeight = 0;
        foreach (var room in rooms)
        {
            totalWeight += room.weight;
        }
        
        // 在总权重范围内随机
        float randomValue = (float)rng.NextDouble() * totalWeight;
        float currentWeight = 0;
        
        // 遍历找到对应的房间
        foreach (var room in rooms)
        {
            currentWeight += room.weight;
            if (randomValue <= currentWeight)
            {
                return room;
            }
        }
        
        return rooms[rooms.Count - 1]; // 兜底返回最后一个
    }
}