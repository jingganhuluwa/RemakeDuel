// 文件：LogicFrameManager.cs
// 作者：急冻雪柜
// 描述：逻辑帧管理器
// 日期：2025/05/07 19:15

using System;
using System.Collections.Generic;

namespace TinyFramework;

public static class LogicFrameConfig
{
    public static long LogicFrameId; //逻辑帧Id

    public static readonly double LogicFrameInterval = 0.1; //逻辑帧间隔,1秒10帧

    public static readonly int LogicFrameIntervalMS = 100; //逻辑帧间隔(毫秒级)
}

public class LogicFrameManager : Singleton<LogicFrameManager>, IDisposable
{
    private double _accLogicRuntime; //累计逻辑运行时间

    private double _nextLogicFrameTime; //下一个逻辑帧时间

    public static double DeltaTime { get; private set; } //逻辑时间增量

    //临时列表
    private readonly List<Action> _tmpList = new List<Action>();

    //逻辑帧监听列表
    private readonly List<Action> _logicFrameListenerList = new List<Action>();


    public override void Init()
    {
        base.Init();
        MonoManager.Instance.AddUpdateListener(Update);
    }

    public override void UnInit()
    {
        base.UnInit();
        MonoManager.Instance.RemoveUpdateListener(Update);
        
        _tmpList.Clear();
        _logicFrameListenerList.Clear();
    }

    public void AddLogicFrameListener(Action action)
    {
        _tmpList.Add(action);
    }

    public void RemoveLogicFrameListener(Action action)
    {
        _tmpList.Remove(action);
        _logicFrameListenerList.Remove(action);
    }

    public void Update(double delta)
    {
        //累计逻辑运行时间
        _accLogicRuntime += delta;

        //逻辑帧更新
        while (_accLogicRuntime > _nextLogicFrameTime)
        {
            LogicFrameUpdate();
            _nextLogicFrameTime += LogicFrameConfig.LogicFrameInterval;
            LogicFrameConfig.LogicFrameId++;
        }

        //计算逻辑时间增量
        DeltaTime = (_accLogicRuntime + LogicFrameConfig.LogicFrameInterval - _nextLogicFrameTime) /
                    LogicFrameConfig.LogicFrameInterval;
    }

    private void LogicFrameUpdate()
    {
        //逻辑帧监听列表更新
        ListenerUpdate();


        //逻辑帧计时器更新
        LogicFrameTimerManager.Instance.OnLogicFrameUpdate();
    }

    /// <summary>
    /// 逻辑帧监听更新
    /// </summary>
    private void ListenerUpdate()
    {
        {
            foreach (Action action in _tmpList)
            {
                _logicFrameListenerList.Add(action);
            }

            _tmpList.Clear();

            foreach (Action action in _logicFrameListenerList)
            {
                action?.Invoke();
            }
        }
    }
}