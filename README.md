# MyRandomRoomGeneratorDemo_u3d
一个非常简易的基于《以撒的结合》种子算法实现的随机房间生成系统

## 🎮 功能特性

- **种子系统**：支持数字/字符串种子，相同种子可复现相同地图
- **房间池管理**：3种房间类型（普通/商店/Boss），权重可控
- **程序化生成**：基于随机游走的线性地牢生成算法
- **难度曲线**：根据进度动态调整房间类型概率

## 🛠️ 技术栈

- Unity 2022.3 LTS
- C#
- 程序化内容生成(PCG)

## 📁 项目结构
Assets/
├── Scripts/ # 核心脚本
│ ├── DungeonGenerator.cs
│ ├── RoomPool.cs
│ ├── SeedManager.cs
│ └── Room.cs
├── Prefabs/ # 房间预制体
└── Scenes/ # 演示场景

## 🚀 如何使用

1. 打开Unity Hub，添加本项目
2. 打开 `Assets/Scenes/MainScene.unity`
3. 点击Play运行
4. 在Hierarchy中选中 `SeedManager` 可修改种子

## 📝 核心代码说明

| 脚本 | 功能 |
|------|------|
| DungeonGenerator | 地牢生成主逻辑，控制房间布局 |
| RoomPool | 房间池管理，基于权重随机选择 |
| SeedManager | 种子系统，保证地图可复现 |

## 🎯 学习收获
- 理解UGC地图编辑器的核心逻辑
- 理解程序化生成算法基础
- 实践Unity脚本与Prefab工作流
