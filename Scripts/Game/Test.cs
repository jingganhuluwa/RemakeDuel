// 文件：Test.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/03/15 16:07

using System;
using System.IO;
using Godot;
using MessagePack;

namespace TinyFramework;

public partial class Test : Node
{
    [Export] private PackedScene _testPrefab;

    private Node2D _testNode;

    public override void _Ready()
    {
        GD.PrintRich("[color=green]Test启动[/color]");
        _testNode = _testPrefab.Instantiate<Node2D>();
        AddChild(_testNode);
        GD.Print(_testPrefab.GetPath());
    }

    public override void _Process(double delta)
    {
        // if (Input.IsActionJustPressed("ui_up"))
        // {
        //     Node node = PoolManager.Instance.GetObject(_testPrefab.GetPath());
        // }
        //
        // if (Input.IsActionJustPressed("ui_up"))
        // {
        //     PoolManager.Instance.ClearAll();
        // }
        // if (Input.IsActionJustPressed("ui_up"))
        // {
        //     PoolManager.Instance.ClearOne(Path.GetFileNameWithoutExtension(_testPrefab.GetPath()));
        // } 
        // if (Input.IsActionJustPressed("ui_up"))
        // {
        //     TestSave testSave = new TestSave(){Id = 100,Name = "张三",LastSaveTime = DateTime.Now};
        //     SaveManager.SaveFile(testSave);
        // } 
        // if (Input.IsActionJustPressed("ui_down"))
        // {
        //     TestSave save = SaveManager.LoadFile<TestSave>();
        //     GD.Print($"id:{save.Id} LastSaveTime:{save.LastSaveTime} Name:{save.Name}");
        // }
        if (Input.IsActionJustPressed("ui_up"))
        {
            SettingManager.Instance.LoadSetting();
            SettingData setting = SettingManager.Instance.Setting;
            GD.Print($"id:{setting.Id}  CreateTime:{setting.CreateTime} LastSaveTime:{setting.LastSaveTime} \n AudioVolume:{setting.AudioVolume} BGMVolume:{setting.BGMVolume}");
        }

        if (Input.IsActionJustPressed("ui_down"))
        {
            SettingData setting = SettingManager.Instance.Setting;
            setting.AudioVolume=GD.RandRange(0,100);
            setting.BGMVolume=GD.RandRange(0,100);
            SettingManager.Instance.SaveSetting();
            
        }
    }
}

[MessagePackObject]
public class TestSave : SaveData
{
    [Key(3)] public string Name;
}