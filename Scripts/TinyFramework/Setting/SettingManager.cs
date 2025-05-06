// 文件：SettingManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/03/25 19:29

namespace TinyFramework;

public class SettingManager:Singleton<SettingManager>
{
    public SettingData Setting { get; private set; } 


    public override void Init()
    {
        base.Init();
        LoadSetting();
    }

    public void LoadSetting()
    {
        Setting  = SaveManager.LoadFile<SettingData>();
    }

    public void SaveSetting()
    {
        SaveManager.SaveFile(Setting);
    }
}