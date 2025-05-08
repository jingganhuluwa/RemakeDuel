// 文件：LogicFrameTimer.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/05/07 21:13

using System;

namespace TinyFramework;

public class LogicFrameTimer
{
    public long Id;
    public bool IsFinished{ get; private set; }
    public VInt CompleteTime{ get; private set; }
    public int LoopCount{ get; private set; }
    public int Loop{ get; private set; }
    public Action OnTimerComplete;

    public VInt AccTime { get; private set; } //当前累加时间
    

    /// <summary>
    /// 初始化计时器
    /// </summary>
    /// <param name="completeTime">完成时间</param>
    /// <param name="callback">回调</param>
    /// <param name="loop">循环次数</param>
    /// <param name="initAccTime">初始累计时间</param>
    public LogicFrameTimer(VInt completeTime, Action callback, int loop = 1,int initAccTime=0)
    {
        Id=IDGenerate.Generate();
        CompleteTime = completeTime;
        OnTimerComplete = callback;
        Loop = loop;
        AccTime = initAccTime;
        
        IsFinished=false;
        LoopCount = 0;
    }
    
    public void OnLogicFrameUpdate()
    {
        AccTime +=  LogicFrameConfig.LogicFrameIntervalMS;

        if (IsFinished&&AccTime < CompleteTime )
        {
            return;
        }

        OnTimerComplete?.Invoke();
        AccTime -= CompleteTime;
        LoopCount++;
        if (LoopCount>=Loop)
        {
            IsFinished = true;
        }
    }
}