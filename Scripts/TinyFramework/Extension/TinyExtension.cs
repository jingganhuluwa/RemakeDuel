// 文件：TinyExtension.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/05/06 3:06

using System;
using System.Collections;
using System.Reflection;

namespace TinyFramework;

public static class TinyExtension
{
    #region 通用
    /// <summary>
    /// 获取特性
    /// </summary>
    public static T GetAttribute<T>(this object obj) where T : Attribute
    {
        return obj.GetType().GetCustomAttribute<T>();
    }
    /// <summary>
    /// 获取特性
    /// </summary>
    /// <param name="type">特性所在的类型</param>
    /// <returns></returns>
    public static T GetAttribute<T>(this object obj, Type type) where T : Attribute
    {
        return type.GetCustomAttribute<T>();
    }

    /// <summary>
    /// 数组相等对比
    /// </summary>
    public static bool ArraryEquals(this object[] objs, object[] other)
    {
        if (other == null || objs.GetType() != other.GetType())
        {
            return false;
        }
        if (objs.Length == other.Length)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if (!objs[i].Equals(other[i]))
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    #endregion
    
    
    #region Mono

    /// <summary>
    /// 添加Update监听
    /// </summary>
    public static void OnUpdate(this object obj, Action<double> action)
    {
        MonoManager.Instance.AddUpdateListener(action);
    }
    /// <summary>
    /// 移除Update监听
    /// </summary>
    public static void RemoveUpdate(this object obj, Action<double> action)
    {
        MonoManager.Instance.RemoveUpdateListener(action);
    }
    

    /// <summary>
    /// 添加FixedUpdate监听
    /// </summary>
    public static void OnFixedUpdate(this object obj, Action<double> action)
    {
        MonoManager.Instance.AddFixedUpdateListener(action);
    }
    /// <summary>
    /// 移除Update监听
    /// </summary>
    public static void RemoveFixedUpdate(this object obj, Action<double> action)
    {
        MonoManager.Instance.RemoveFixedUpdateListener(action);
    }
    

    #endregion
    
}