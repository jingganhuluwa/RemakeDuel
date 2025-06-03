// 文件：UIManager.cs
// 作者：急冻雪柜
// 描述：UI管理器
// 日期：2024/09/19 19:51

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

namespace TinyFramework;

public enum UILayer
{
    Bottom,
    Middle,
    Top
}

public partial class UIManager:SingletonNode<UIManager>
{
    [Export]
    private Control _bottom;
    [Export]
    private Control _middle;
    [Export]
    private Control _top;
    [Export]
    private Label _tips;
    


    private readonly Dictionary<string, Panel> _panelDict = new Dictionary<string, Panel>();
    private readonly Queue<string> _tipsQueue = new Queue<string>();
    private bool isTipShow = false;
    
    public  static readonly  string ResPath = Path.Combine(PathDefine.UIPath,"UIManager.tscn");

    public override void Init()
    {
        base.Init();
        _tips.Hide();
        
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!isTipShow)
        {
            ShowTips();
        }
    }

    public void ShowUI<T>(UILayer uiLayer=UILayer.Bottom,Action callback=null) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panelDict.TryGetValue(panelName,out Panel ui))
        {
            (ui as BasePanel).OnShow();
            callback?.Invoke();
            return;
        }

        string fullName = Path.Combine(PathDefine.UIPath, panelName + ".tscn");

        PackedScene packedScene = ResourceLoader.Load<PackedScene>(fullName);
        if (packedScene is null)
        {
            return;
        }

        ui = packedScene.Instantiate<Panel>();
        ui.Name = panelName;
        switch (uiLayer)
        {
            case UILayer.Bottom:
                _bottom.AddChild(ui);
                break;
            case UILayer.Middle:
                _middle.AddChild(ui);
                break;
            case UILayer.Top:
                _top.AddChild(ui);
                break;
        }
        (ui as BasePanel)?.OnShow();
        callback?.Invoke();
        _panelDict.Add(panelName,ui);
    }
    
    public void HideUI<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panelDict.TryGetValue(panelName,out Panel ui))
        {
            (ui as BasePanel)?.OnHide();
            return;
        }
    }

    public void CloseUI(Panel ui)
    {
        if (_panelDict.Values.Contains(ui))
        {
            _panelDict.Remove(ui.Name);
            (ui as BasePanel)?.OnHide();
            ui.QueueFree();
        }
    }
    
    public void AddTips(string content)
    {
        _tipsQueue.Enqueue(content);
    }

    private void ShowTips()
    {
        if (_tipsQueue.Count>0)
        {
            string content = _tipsQueue.Dequeue();
            isTipShow = true;
            _tips.Show();
            _tips.Text = content;
            Tween tween = CreateTween();
            Vector2 position = _tips.Position;
            position.Y -= 200;
            Color color = _tips.SelfModulate;
            color.A = 1;
            tween.TweenProperty(_tips, "position",position,2);
            tween.SetParallel();
            tween.TweenProperty(_tips, "self_modulate",color,1);
            tween.SetParallel(false);
            color.A = 0;
            tween.TweenProperty(_tips, "self_modulate",color,1);
            tween.TweenCallback(Callable.From(() =>
            {
                isTipShow = false;
                position.Y += 200;
                _tips.Position = position;
                _tips.SelfModulate=color;
                _tips.Hide();
            }));
            tween.Play();
        }
    }
}