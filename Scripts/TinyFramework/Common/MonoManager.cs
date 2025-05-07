// 文件：MonoManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/05/06 3:11

using System;

namespace TinyFramework;

public partial class MonoManager:SingletonNode<MonoManager>
{
    private Action<double> _updateEvent;
    private Action<double> _fixedUpdateEvent;
    
    /// <summary>
    /// 添加Update监听
    /// </summary>
    /// <param name="action"></param>
    public void AddUpdateListener(Action<double> action)
    {
        _updateEvent += action;
    }
    /// <summary>
    /// 移除Update监听
    /// </summary>
    /// <param name="action"></param>
    public void RemoveUpdateListener(Action<double> action)
    {
        _updateEvent -= action;
    }
    
    /// <summary>
    /// 添加FixedUpdate监听
    /// </summary>
    /// <param name="action"></param>
    public void AddFixedUpdateListener(Action<double> action)
    {
        _fixedUpdateEvent += action;
    }
    /// <summary>
    /// 移除FixedUpdate监听
    /// </summary>
    /// <param name="action"></param>
    public void RemoveFixedUpdateListener(Action<double> action)
    {
        _fixedUpdateEvent -= action;
    }

    public override void _Process(double delta)
    {
        _updateEvent?.Invoke(delta);
    }


    public override void _PhysicsProcess(double delta)
    {
        _fixedUpdateEvent?.Invoke(delta);
    }
}