// 文件：LogicFrameTimerManager.cs
// 作者：急冻雪柜
// 描述：逻辑帧定时器管理器
// 日期：2025/05/07 19:29

using System;
using System.Collections.Generic;

namespace TinyFramework;

public class LogicFrameTimerManager : Singleton<LogicFrameTimerManager>, IDisposable
{
    private readonly List<LogicFrameTimer> _logicFrameTimerList = new List<LogicFrameTimer>();
    private readonly List<LogicFrameTimer> _tmpList = new List<LogicFrameTimer>();


    public static LogicFrameTimer CreateTimer(VInt completeTime, Action callback, int loop = 1, int initAccTime = 0)
    {
        LogicFrameTimer timer = new LogicFrameTimer(completeTime, callback, loop, initAccTime);
        Instance._tmpList.Add(timer);
        return timer;
    }

    public override void Init()
    {
        base.Init();
        Clear();
    }

    public override void UnInit()
    {
        base.UnInit();
        Clear();
    }

    public void OnLogicFrameUpdate()
    {
        foreach (LogicFrameTimer logicFrameTimer in _tmpList)
        {
            _logicFrameTimerList.Add(logicFrameTimer);
        }

        _tmpList.Clear();
        foreach (LogicFrameTimer logicFrameTimer in _logicFrameTimerList)
        {
            logicFrameTimer.OnLogicFrameUpdate();
        }
        
        // foreach (LogicFrameTimer logicFrameTimer in _logicFrameTimerList.ToList())
        // {
        //     //如果定时器任务已经完成,删除
        //     _logicFrameTimerList.Remove(logicFrameTimer);
        // }
    }


    public void Clear()
    {
        _logicFrameTimerList.Clear();
        _tmpList.Clear();
    }
}