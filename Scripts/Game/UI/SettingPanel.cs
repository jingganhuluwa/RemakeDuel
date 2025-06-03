// 文件：SettingPanel.cs
// 作者：急冻雪柜
// 描述：设置面板
// 日期：2025/05/09 16:05

using Godot;

namespace TinyFramework;

public partial class SettingPanel:BasePanel
{
    [Export]
    private Slider _audioSlider;
    
    [Export]
    private CheckButton _audioButton;
    
    [Export]
    private Slider _bgmSlider;
    
    [Export]
    private CheckButton _bgmButton;
    
    [Export]
    private Button _saveButton;
    
    private SettingData _setting => SettingManager.Instance.Setting;

    public override void OnShow()
    {
        base.OnShow();
        
        // 连接slider的值变化事件
        _audioSlider.ValueChanged += OnAudioVolumeChanged;
        _bgmSlider.ValueChanged += OnBGMVolumeChanged;
        
        // 连接checkbutton的切换事件
        _audioButton.Toggled += OnAudioToggled;
        _bgmButton.Toggled += OnBGMToggled;
        
        _saveButton.Pressed += OnSaveButtonPressed;
        
        // 初始化slider的值（可以从设置中读取）
        _audioSlider.Value = _setting.AudioVolume;
        _bgmSlider.Value = _setting.BGMVolume;  // 默认最大音量
        
        // 初始化checkbutton的状态
        _audioButton.ButtonPressed = !_setting.IsAudioMute;
        _bgmButton.ButtonPressed = !_setting.IsBGMMute;
    }

    private void OnSaveButtonPressed()
    {
        SaveManager.SaveFile(_setting);
        OnHide();
    }

    public override void OnHide()
    {
        base.OnHide();
        
        // 断开slider的值变化事件
        _audioSlider.ValueChanged -= OnAudioVolumeChanged;
        _bgmSlider.ValueChanged -= OnBGMVolumeChanged;
        
        // 断开checkbutton的切换事件
        _audioButton.Toggled -= OnAudioToggled;
        _bgmButton.Toggled -= OnBGMToggled;
        
        _saveButton.Pressed -= OnSaveButtonPressed;
        
    }
   

    private void OnAudioVolumeChanged(double value)
    {
        _setting.AudioVolume = (int)value;
        // 如果音效被禁用，不更新音量
        if (!_audioButton.ButtonPressed)
            return;
            
        AudioManager.Instance.SetAudioVolume((float)value);
    }

    private void OnBGMVolumeChanged(double value)
    {
        _setting.BGMVolume = (int)value;
        // 如果背景音乐被禁用，不更新音量
        if (!_bgmButton.ButtonPressed)
            return;
            
        AudioManager.Instance.SetBGMVolume((float)value);
    }

    private void OnAudioToggled(bool buttonPressed)
    {
        AudioManager.Instance.SetAudioVolume(buttonPressed ? (float)_audioSlider.Value : 0);
    }

    private void OnBGMToggled(bool buttonPressed)
    {
        AudioManager.Instance.SetBGMVolume(buttonPressed ? (float)_bgmSlider.Value : 0);
    }
}