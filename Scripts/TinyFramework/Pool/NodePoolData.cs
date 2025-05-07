// 文件：NodePoolData.cs
// 作者：急冻雪柜
// 描述：Node对象池数据
// 日期：2025/03/21 23:18

using System.Collections.Generic;
using Godot;

namespace TinyFramework;

public class NodePoolData
{
    //父节点
    public Node FatherNode { get;private set; }
    //对象池队列
    private readonly Queue<Node> _poolqueue = new();


    public NodePoolData(Node node,Node poolRoot)
    {
        //创建父节点,并设置到对象池根节点
        FatherNode = new Node();
        poolRoot.AddChild(FatherNode);
        //设置父节点名称
        FatherNode.Name = node.Name;
        PushObject(node);
    }

    public void PushObject(Node node)
    {
        //放进队列
        _poolqueue.Enqueue(node);
        //设置父物体
        if (node.GetParent()==null)
        {
            FatherNode.AddChild(node);
           
        }
        else
        {
            node.Reparent(FatherNode);
        }
        
        //隐藏
        if (node is Node2D node2D)
        {
            node2D.Visible=false;
        }
        if (node is Node3D node3D)
        {
            node3D.Visible=false;
        }
        
    }

    public Node GetObject()
    {
        if (_poolqueue.Count == 0)
        {
            return null;
        }
        Node node = _poolqueue.Dequeue();
        //显示
        if (node is Node2D node2D)
        {
            node2D.Visible=true;
        }
        if (node is Node3D node3D)
        {
            node3D.Visible=true;
        }
        //设定父物体为根节点
        node.Reparent(node.GetTree().Root);
        return node;
    }

    public void ClearAll()
    {
        foreach (Node node in _poolqueue)
        {
            node.QueueFree();
        }
        
        FatherNode.QueueFree();
        _poolqueue.Clear();
    }
}