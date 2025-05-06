// 文件：SaveManager.cs
// 作者：急冻雪柜
// 描述：
// 日期：2025/03/24 2:02

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Godot;
using MessagePack;
using FileAccess = System.IO.FileAccess;

namespace TinyFramework;

public static class SaveManager
{
    private static Dictionary<string, SaveData> _saveDataDict = new Dictionary<string, SaveData>();


    #region 缓存

    /// <summary>
    /// 存档缓存保存
    /// </summary>
    private static void AddCache<T>(T saveData, string fileName = null) where T : SaveData
    {
        if (fileName == null)
        {
            fileName = typeof(T).Name;
        }

        GD.Print($"SaveManager: {fileName} 缓存保存");
        if (_saveDataDict.TryGetValue(fileName, out SaveData saveDataCache))
        {
            saveDataCache = saveData;
            return;
        }

        _saveDataDict.Add(fileName, saveData);
    }

    /// <summary>
    /// 存档缓存加载
    /// </summary>
    private static T GetCache<T>( string fileName = null) where T : SaveData
    {
        if (fileName == null)
        {
            fileName = typeof(T).Name;
        }

        GD.Print($"SaveManager: {fileName} 缓存加载");
        if (_saveDataDict.TryGetValue(fileName, out SaveData saveDataCache))
        {
            return saveDataCache as T;
        }

        return null;
    }

    #endregion

    #region 存档读档

    /// <summary>
    /// 存档文件
    /// </summary>
    public static void SaveFile<T>(T saveData, string fileName = null) where T : SaveData
    {
        if (fileName == null)
        {
            fileName = typeof(T).Name;
        }
        
        //更新时间
        saveData.UpdateTime();


        if (!Directory.Exists(PathDefine.SavePath))
        {
            Directory.CreateDirectory(PathDefine.SavePath);
        }

        string path = Path.Combine(PathDefine.SavePath, fileName);

        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            //MessagePack二进制方式存档
            MessagePackSerializer.Serialize(fs, saveData);
        }
    }

    /// <summary>
    /// 加载文件
    /// </summary>
    public static T LoadFile<T>(string fileName = null) where T : SaveData, new()
    {
        fileName = fileName ?? typeof(T).Name;
        //加载缓存
        T cacheData = GetCache<T>(fileName);
        if (cacheData != null)
        {
            return cacheData;
        }

        //拼接路径
        string path = Path.Combine(PathDefine.SavePath, fileName);
        if (!File.Exists(path))
        {
            GD.Print($"SaveManager: {path} 找不到,新建并保存");
            T data = new T();
            SaveFile(data, fileName);
            return data;
        }

        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        //MessagePack二进制方式读档
        T saveData = MessagePackSerializer.Deserialize<T>(fs);
        if (saveData == null)
        {
            saveData = new T();
        }

        //存档缓存保存
        AddCache(saveData, fileName);
        return saveData;
    }

    #endregion
}