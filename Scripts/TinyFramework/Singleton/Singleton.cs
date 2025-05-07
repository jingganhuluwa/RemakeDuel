// 文件：Singleton.cs
// 作者：急冻雪柜
// 描述：单例基类
// 日期：2024/09/19 16:13

using System;
using Godot;

namespace TinyFramework;

public class Singleton<T> where T: IDisposable,new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }

    public virtual void Init()
    {
        GD.PrintRich($"[color=CYAN] {typeof(T).Name} 启动 [/color]");
    }
    
    public void Dispose()
    {
        _instance = default;
        UnInit();
    }
    
    public virtual void UnInit()
    {
        GD.PrintRich($"[color=ORANGE] {typeof(T).Name}  关闭 [/color]");
    }
}