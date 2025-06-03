using Godot;
using System;
using TinyFramework;

public partial class WelcomePanel : BasePanel
{
    [Export]
    private Button _anyButton;


    public override void OnShow()
    {
        base.OnShow();
        _anyButton.Pressed += OnAnyButtonPressed;
    }
    
    public override void OnHide()
    {
        base.OnHide();
        _anyButton.Pressed -= OnAnyButtonPressed;
    }

    private void OnAnyButtonPressed()
    {
        UIManager.Instance.ShowUI<MainPanel>();
        OnClose();
    }
}
