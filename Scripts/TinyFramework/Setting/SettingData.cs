// 文件：SettingData.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/03/25 19:29

using MessagePack;

namespace TinyFramework;

[MessagePackObject]
public class SettingData:SaveData
{
    [Key(3)]
    public int BGMVolume = 100;
    [Key(4)]
    public int AudioVolume = 100;
    [Key(5)]
    public bool IsBGMMute = false;
    [Key(6)]
    public bool IsAudioMute = false;
    
    
}