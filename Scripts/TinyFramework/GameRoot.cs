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
    }

    private void InitManager()
    {
        SettingManager.Instance.Init();
        
        ResManager.Create().Init();
        PoolManager.Create().Init();
        //音量管理器
        AudioManager.Create().Init();

        MonoManager.Create().Init();


        UIManager.Create(UIManager.ResPath).Init();
    }
    
    

}