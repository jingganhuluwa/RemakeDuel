// 文件：ObjectPoolData.cs
// 作者：急冻雪柜
// 描述：普通类 对象 对象池数据
// 日期：2025/05/06 2:44

using System.Collections.Generic;

namespace TinyFramework;

public class ObjectPoolData
{
    public ObjectPoolData(object obj)
    {
        PushObj(obj);
    }
    // 对象容器
    public Queue<object> poolQueue = new Queue<object>();
    /// <summary>
    /// 将对象放进对象池
    /// </summary>
    public void PushObj(object obj)
    {
        poolQueue.Enqueue(obj);
    }

    /// <summary>
    /// 从对象池中获取对象
    /// </summary>
    /// <returns></returns>
    public object GetObj()
    {
        return poolQueue.Dequeue();
    }
}