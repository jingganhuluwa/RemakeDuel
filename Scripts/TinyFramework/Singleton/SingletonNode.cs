// 文件：SingletonNode.cs
// 作者：急冻雪柜
// 描述：Node节点单例基类
// 日期：2024/09/19 16:16

using Godot;

namespace TinyFramework;

public partial class SingletonNode<T> : Node where T : Node, new()
{
    public static T Instance { get; private set; }
    

    public override void _EnterTree()
    {
        base._EnterTree();
        if (Instance == null)
        {
            Instance = this as T;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        UnInit();
    }


    public virtual void Init()
    {
        GD.PrintRich($"[color=CYAN] {typeof(T).Name} 启动 [/color]");
    }
    
    public virtual void UnInit()
    {
        GD.PrintRich($"[color=ORANGE] {typeof(T).Name} 关闭 [/color]");
    }

    /// <summary>
    /// 新建并作为GameRoot的子节点
    /// </summary>
    protected static T Create(string path = "")
    {
        if (!string.IsNullOrEmpty(path))
        {
            return ResManager.Instance.Load<T>(path,GameRoot.Instance);
        }
        Instance = new T();
        Instance.Name = typeof(T).Name;
        GameRoot.Instance.AddChild(Instance);
        return Instance;
    }
}