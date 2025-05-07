// 文件：ResManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/03/22 3:22

using System;
using System.Collections.Generic;
using Godot;

namespace TinyFramework;

public partial class ResManager : SingletonNode<ResManager>
{
    
    // 需要缓存的类型
    private static Dictionary<Type, bool> _wantCacheDic=new();

    public override void Init()
    {
        base.Init();
        //todo 
        //_wantCacheDic = SettingManager.Instance.Setting.WantCacheDic;
    }


    /// <summary>
    /// 检查一个类型是否需要缓存
    /// </summary>
    private static bool CheckCacheDic(Type type)
    {
        return _wantCacheDic.ContainsKey(type);
    }
    
    /// <summary>
    /// 获取预制体,PackedScene
    /// </summary>
    public PackedScene LoadPackScene(string path)
    {
        //路径为空
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        var packedScene = ResourceLoader.Load<PackedScene>(path);
        //找不到
        if (packedScene == null)
        {
            throw new Exception("Can't load resource: " + path);
        }

        return packedScene;
    }
    
    /// <summary>
    /// 实例化预制体
    /// </summary>
    public T Load<T>(string path, Node parent=null) where T : Node
    {
        return Load<T>(LoadPackScene(path), parent);
    }

    public T Load<T>(PackedScene prefab, Node parent=null) where T : Node
    {
        T res = prefab.Instantiate<T>();
        if (parent == null)
        {
            AddChild(res);
            return res;
        }
        
        parent.AddChild(res);

        return res;
    }
    
    /// <summary>
    /// 获取实例-普通Class
    /// 如果类型需要缓存，会从对象池中获取
    /// </summary>
    public static T Load<T>() where T : class, new()
    {
        // 需要缓存
        if (CheckCacheDic(typeof(T)))
        {
            return PoolManager.Instance.GetObject<T>();
        }
        else
        {
            return new T();
        }
    }
    
    public T LoadRes<T>(string path) where T:Resource
    {
        //路径为空
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }
        T res = ResourceLoader.Load<T>(path);
        return res;
    }

    
}