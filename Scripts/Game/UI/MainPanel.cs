// 文件：MainPanel.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/06/02 23:25


using System;
using Godot;
using TinyFramework;

public partial class MainPanel : BasePanel
{
    [Export] private Button _startBtn;
    [Export] private Button _settingBtn;

    [Export] private Button _xiaohuSelectButton;
    [Export] private Button _qiqiSelectButton;
    [Export] private Button _sumeiSelectButton;
    [Export] private Button _yiruSelectButton;

     private static Sprite2D _characterSprite;
     private static int _selectedCharacter = 0;


    public override void OnShow()
    {
        base.OnShow();
        _settingBtn.Pressed += OnSettingBtnPressed;
        _startBtn.Pressed += OnStartBtnPressed;
        
        _xiaohuSelectButton.Pressed +=()=> SelectButton(0);
        _qiqiSelectButton.Pressed +=()=> SelectButton(1);
        _sumeiSelectButton.Pressed +=()=> SelectButton(2);
        _yiruSelectButton.Pressed +=()=> SelectButton(3);
        
        _characterSprite=GetNode<Sprite2D>("Character");
        
        AudioManager.Instance.PlayBGM("Xian024余情幽梦.mp3");
        Refresh();
    }

    public static void Refresh()
    {
        switch (_selectedCharacter)
        {
            case 0:
                _characterSprite.Texture = ResourceLoader.Load<Texture2D>("res://Resources/Images/Character/Xiaohu.png");
                break;
            case 1:
                _characterSprite.Texture = ResourceLoader.Load<Texture2D>("res://Resources/Images/Character/Qiqi.png");
                break;
            case 2:
                _characterSprite.Texture = ResourceLoader.Load<Texture2D>("res://Resources/Images/Character/Sumei.png");
                break;
            case 3:
                _characterSprite.Texture = ResourceLoader.Load<Texture2D>("res://Resources/Images/Character/Yiru.png");
                break;
        }
    }

    public override void OnHide()
    {
        base.OnHide();
        _settingBtn.Pressed -= OnSettingBtnPressed;
        _startBtn.Pressed -= OnStartBtnPressed;
    }

    private void OnStartBtnPressed()
    {
    }

    private void OnSettingBtnPressed()
    {
        UIManager.Instance.ShowUI<SettingPanel>();
    }


    private Action<int> SelectButton = (index) =>
    {
        _selectedCharacter = index;
        Refresh();
    };
}