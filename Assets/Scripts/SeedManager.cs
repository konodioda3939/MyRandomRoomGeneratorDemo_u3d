using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeedManager : MonoBehaviour
{
    public static SeedManager Instance;
    
    [Header("种子设置")]
    public int seed = 12345;          // 种子值
    public string seedString;         // 种子字符串（可输入）
    
    private System.Random rng;
    
    void Awake()
    {
        Instance = this;
        InitializeSeed();
    }
    
    // 初始化随机数生成器
    public void InitializeSeed()
    {
        if (!string.IsNullOrEmpty(seedString))
        {
            // 将字符串转换为数字种子
            seed = seedString.GetHashCode();
        }
        rng = new System.Random(seed);
        Debug.Log($"[种子] 初始化种子: {seed}");
    }
    
    public System.Random GetRNG()
    {
        return rng;
    }
    
    // 生成新种子（随机）
    public void GenerateNewSeed()
    {
        seed = Random.Range(10000, 99999);
        seedString = seed.ToString();
        InitializeSeed();
    }
}