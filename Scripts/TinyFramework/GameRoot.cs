// 文件：GameRoot.cs
// 作者：急冻雪柜
// 描述：游戏入口
// 日期：2025/03/23 4:05

using TinyFramework;

public partial class GameRoot:SingletonNode<GameRoot>
{
    public override void _Ready()
    {
        base._Ready();
        Init();
        
        InitManager();
        UIManager.Instance.ShowUI<WelcomePanel>();
        AudioManager.Instance.PlayBGM("Xian013水龙吟.mp3");
    }

    private void InitManager()
    {
        //设置管理器
        SettingManager.Instance.Init();
        //资源管理器
        ResManager.Create().Init();
        //对象池管理器
        PoolManager.Create().Init();
        //音量管理器
        AudioManager.Create().Init();
        
        MonoManager.Create().Init();
        //逻辑帧管理器
        LogicFrameManager.Instance.Init();
        
        //UI管理器
        UIManager.Create(UIManager.ResPath).Init();
    }
    
    

}