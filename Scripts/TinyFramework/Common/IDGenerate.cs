// 文件：IDGenerate.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/03/25 23:45

using System;

namespace TinyFramework;

public static class IDGenerate
{
    private static readonly string _lock = "lock";
    private static long _lastTimestamp = 0;
    private static long _sequence = 0;
    private const long _maxSequence = 9999; // 序列号的最大值

    public static long Generate()
    {
        
        //代码由AI生成
        lock (_lock)
        {
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (currentTimestamp == _lastTimestamp)
            {
                _sequence = (_sequence + 1) % _maxSequence;
                if (_sequence == 0)
                {
                    // 如果序列号溢出，等待下一毫秒
                    while (currentTimestamp == _lastTimestamp)
                    {
                        currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    }
                }
            }
            else
            {
                _sequence = 0;
            }

            _lastTimestamp = currentTimestamp;

            // 组合生成ID：时间戳部分 + 序列号部分
            long id = currentTimestamp * (long)_maxSequence + _sequence;
            return id;
        }
    }
}