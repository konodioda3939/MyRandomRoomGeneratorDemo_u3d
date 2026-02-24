using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Room
{
    public string roomName;        // 房间名称
    public RoomType roomType;      // 房间类型
    public float weight;           // 权重值
    public GameObject roomPrefab;  // 房间预制体
    
    public enum RoomType
    {
        Normal,    // 普通房
        Elite,     // 精英房
        Shop,      // 商店
        Boss,      // Boss房
        Treasure   // 宝箱房
    }
}