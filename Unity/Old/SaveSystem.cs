using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// 用于提供存档的方法
/// </summary>
public class SaveSystem : MonoBehaviour
{
    /// <summary>
    /// 包含所有需要存储的数据的类
    /// </summary>
    [Serializable]
    public struct SaveData
    {
        public float a;
        public float b;
        public float c;
        public int d;
        public string e;
    }
    /// <summary>
    /// 包含所有需要存储的数据
    /// </summary>
    public static SaveData saveData;

    [ContextMenu("Save")]
    /// <summary>
    /// 存自动档
    /// </summary>
    public static void Save()
    {
        Save(0);
    }
    /// <summary>
    /// 存档
    /// </summary>
    /// <param name="dataNum">存档序号（0代表自动档）</param>
    public static void Save(int dataNum)
    {
        PackData();
        BinaryWriter bw = new BinaryWriter(File.Open(Application.dataPath + "/Saves/Save" + dataNum + ".sav", FileMode.Create));
        bw.Write(StructToBytes(saveData, Marshal.SizeOf(saveData)));
        bw.Close();
    }
    [ContextMenu("Load")]
    /// <summary>
    /// 读自动档
    /// </summary>
    public static void Load()
    {
        Load(0);
    }
    /// <summary>
    /// 读档
    /// </summary>
    /// <param name="dataNum">存档序号（0代表自动档）</param>
    public static void Load(int dataNum)
    {
        if (!Directory.Exists(Application.dataPath + "/Saves/Save" + dataNum + ".sav")) return;
        BinaryReader br= new BinaryReader(File.Open(Application.dataPath + "/Saves/Save" + dataNum + ".sav", FileMode.Open));
        Byte[] buffer=new Byte[Marshal.SizeOf(saveData)];
        br.Read(buffer, 0, Marshal.SizeOf(saveData));
        saveData = (SaveData)ByteToStruct(buffer, typeof(SaveData));
        br.Close();
        ApplyData();
    }
    /// <summary>
    /// 将结构体转换为Byte类型
    /// </summary>
    public static byte[] StructToBytes(object structObj, int size)
    {
        byte[] bytes = new byte[size];
        IntPtr structPtr = Marshal.AllocHGlobal(size);
        //将结构体拷到分配好的内存空间
        Marshal.StructureToPtr(structObj, structPtr, false);
        //从内存空间拷贝到byte 数组
        Marshal.Copy(structPtr, bytes, 0, size);
        //释放内存空间
        Marshal.FreeHGlobal(structPtr);
        return bytes;
    }
    ///<summary>
    ///将Byte转换为结构体类型
    ///</summary>
    public static object ByteToStruct(byte[] bytes, Type type)
    {
        int size = Marshal.SizeOf(type);
        if (size > bytes.Length)
        {
            return null;
        }
        //分配结构体内存空间
        IntPtr structPtr = Marshal.AllocHGlobal(size);
        //将byte数组拷贝到分配好的内存空间
        Marshal.Copy(bytes, 0, structPtr, size);
        //将内存空间转换为目标结构体
        object obj = Marshal.PtrToStructure(structPtr, type);
        //释放内存空间
        Marshal.FreeHGlobal(structPtr);
        return obj;
    }
    /// <summary>
    /// 记录游戏数据并打包至SaveData
    /// </summary>
    private static void PackData()
    {
        //TODO:
    }
    /// <summary>
    /// 将读取的数据应用到游戏
    /// </summary>
    private static void ApplyData()
    {
        //TODO:
    }
    /// <summary>
    /// 返回当前存档数
    /// </summary>
    /// <returns></returns>
    public static int SaveDataNum()
    {
        int d = 0;
        //检测文件是否存在
        while (Directory.Exists(Application.dataPath + "/Saves/Save" + d + ".sav"))
        {
            d++;
        }
        return d;
    }
}
