using Godot;
using System;
using TinyFramework;

public partial class TestPrefab : Node2D
{
    private float counter;
    
    private bool isFirst=true;
    public override void _Ready()
    {
        GD.Print("我产生了");
        //GD.Print(Name);
    }

    public override void _Process(double delta)
    {
        if (!Visible)
        {
            return;
        }

        if (!isFirst && Visible && counter == 0)
        {
            GD.Print($"从对象池中取出:{Name}");
        }
        counter +=(float) delta;
        if (counter>2)
        {
            Dead();
        }
        
    }

    public void Dead()
    {
        counter = 0;
        isFirst=false;
        GD.Print("我回收了");
        //GD.Print(Name);
        PoolManager.Instance.PushNodeObject(this);
    }

    
}
