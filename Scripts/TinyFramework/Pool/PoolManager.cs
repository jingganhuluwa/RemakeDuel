// 文件：PoolManager.cs
// 作者：急冻雪柜
// 描述：todo 待完成复制时有Bug
// 日期：2025/03/21 2:51

using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace TinyFramework;

public partial class PoolManager : SingletonNode<PoolManager>
{
    /// <summary>
    /// Node池子容器字典
    /// </summary>
    private readonly Dictionary<string, NodePoolData> _nodePoolDict = new();

    /// <summary>
    /// 普通类 对象容器
    /// </summary>
    public Dictionary<string, ObjectPoolData> _objectPoolDict = new Dictionary<string, ObjectPoolData>();

    #region Node对象相关操作

    /// <summary>
    /// 获取Node
    /// </summary>
    public T GetNodeObject<T>(Node prefab) where T : Node
    {
        Node node = GetNodeObject(prefab);
        if (node != null)
        {
            return node as T;
        }

        return null;
    }

    public Node GetNodeObject(Node prefab)
    {
        Node node = null;
        //尝试去池子拿
        if (_nodePoolDict.TryGetValue(prefab.Name, out NodePoolData poolData))
        {
            node = poolData.GetObject();
        }

        //没有的话创建
        if (node == null)
        {
            node = prefab.Duplicate();
            //GD.Print(node.Name);
            AddChild(node);
            //物体名+InstanceId,使用_分隔
            node.Name = $"{prefab.Name}_{node.GetInstanceId()}";
        }

        return node;
    }

    public T GetNodeObject<T>(string path) where T : Node
    {
        Node node = GetNodeObject(path);
        if (node != null)
        {
            return node as T;
        }

        return null;
    }

    public Node GetNodeObject(string path)
    {
        string prefabName = Path.GetFileNameWithoutExtension(path);
        GD.Print(prefabName);
        Node node = null;
        //尝试去池子拿
        if (_nodePoolDict.TryGetValue(prefabName, out NodePoolData poolData))
        {
            node = poolData.GetObject();
        }

        //没有的话创建
        if (node == null)
        {
            node = ResManager.Instance.Load<Node>(path, this);
            //GD.Print(node.Name);
            //物体名+InstanceId,使用_分隔
            node.Name = $"{prefabName}_{node.GetInstanceId()}";
        }

        return node;
    }


    public void PushNodeObject(Node prefab)
    {
        //处理prefab名字
        string prefabName = prefab.Name.ToString();
        //参照GetObject中,使用_分隔
        int lastIndex = prefabName.LastIndexOf('_');
        if (lastIndex > 0)
        {
            prefabName = prefabName.Substring(0, lastIndex);
        }


        //容器字典中存在
        if (_nodePoolDict.TryGetValue(prefabName, out NodePoolData poolData))
        {
            poolData.PushObject(prefab);
            return;
        }

        //字典中没有
        _nodePoolDict.Add(prefabName, new NodePoolData(prefab, this));
    }

    #endregion

    #region 普通对象相关操作

    /// <summary>
    /// 获取普通对象
    /// </summary>
    public T GetObject<T>() where T : class, new()
    {
        T obj;
        if (CheckObjectCache<T>())
        {
            string name = typeof(T).FullName;
            obj = (T) _objectPoolDict[name].GetObj();
            return obj;
        }
        else
        {
            return new T();
        }
    }

    /// <summary>
    /// 普通对象放进对象池
    /// </summary>
    /// <param name="obj"></param>
    public void PushObject(object obj)
    {
        string name = obj.GetType().FullName;
        // 现在有没有这一层
        if (_objectPoolDict.ContainsKey(name))
        {
            _objectPoolDict[name].PushObj(obj);
        }
        else
        {
            _objectPoolDict.Add(name, new ObjectPoolData(obj));
        }
    }

    private bool CheckObjectCache<T>()
    {
        string name = typeof(T).FullName;
        return _objectPoolDict.ContainsKey(name) && _objectPoolDict[name].poolQueue.Count > 0;
    }

    #endregion


    #region 删除操作

    public void ClearAll(bool isClearNode = true, bool isClearObject = true)
    {
        if (isClearNode)
        {
            foreach (Node child in GetChildren())
            {
                child.QueueFree();
            }

            _nodePoolDict.Clear();
        }

        if (isClearObject)
        {
            _objectPoolDict.Clear();
        }
    }

    public void ClearNode(string prefabName)
    {
        if (_nodePoolDict.TryGetValue(prefabName, out NodePoolData poolData))
        {
            poolData.ClearAll();
            _nodePoolDict.Remove(prefabName);
        }
    }

    public void ClearNode(Node prefab)
    {
        //处理prefab名字
        string prefabName = prefab.Name.ToString();
        //参照GetObject中,使用_分隔
        int lastIndex = prefabName.LastIndexOf('_');
        if (lastIndex > 0)
        {
            prefabName = prefabName.Substring(0, lastIndex);
        }

        ClearNode(prefabName);
    }

    public void ClearObject<T>()
    {
        _nodePoolDict.Remove(typeof(T).FullName);
    }

    public void ClearObject(Type type)
    {
        _nodePoolDict.Remove(type.FullName);
    }

    #endregion
}