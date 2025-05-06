// 文件：SaveData.cs
// 作者：急冻雪柜
// 描述：存档的数据
// 日期：2025/03/24 2:03

using System;
using MessagePack;

namespace TinyFramework;

[MessagePackObject]
public class SaveData
{
    [Key(0)] public long Id { get; set; } = IDGenerate.Generate();
    [Key(1)] public DateTime CreateTime { get; set; } = DateTime.Now;
    [Key(2)] public DateTime LastSaveTime { get; set; } = DateTime.Now;

    public void UpdateTime(DateTime lastSaveTime = default)
    {
        if (lastSaveTime == default)
        {
            lastSaveTime = DateTime.Now;
        }

        LastSaveTime = lastSaveTime;
    }
}