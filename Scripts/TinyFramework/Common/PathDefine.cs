// 文件：PathDefine.cs
// 作者：急冻雪柜
// 描述：路径定义
// 日期：2024/09/19 16:29

using Godot;

namespace TinyFramework;

public static class PathDefine
{
    public const string ResPath ="res://Resources/";
    public const string AudioPath =ResPath+"Sound/Audio/"; 
    public const string BGMPath =ResPath+"Sound/BGM/"; 
    public const string ScenePath =ResPath+"Scene/"; 
    public const string PrefabsPath =ResPath+"Prefabs/"; 
    public const string UIPath =ResPath+"UI/"; 
    public static string SavePath => ProjectSettings.GlobalizePath("user://");
    public const string ConfigPath= ResPath+ "Config/";
    
    public static string ExcelFilePath =>ProjectSettings.GlobalizePath("res://addons/ConfigGenerate/Excel");
    public static string ExcelFieldClassPath => ProjectSettings.GlobalizePath("res://Scripts/Game/Generate/class/");
    public static string GenerateConfigPath => ProjectSettings.GlobalizePath("res://Resources/Config/");
    
}