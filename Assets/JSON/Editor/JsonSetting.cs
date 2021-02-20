using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using NY;
using System;
using System.IO;
using Newtonsoft.Json;

public class JsonSetting : EditorWindow
{
    public static JsonSetting m_instance;

    private Vector2 m_pos;
    public string m_excelPath;
    public string m_jsonListPath;

    [MenuItem("Custom/Json Setting")]
    public static void Apply()
    {
        JsonSetting window = (JsonSetting)EditorWindow.GetWindow(typeof(JsonSetting), false, "Json Setting");
        window.Show();
        EditorWindow.GetWindow(typeof(JsonSetting));
    }
    void OnGUI()
    {
        GUILayout.BeginVertical();
        m_pos = GUILayout.BeginScrollView(m_pos);

        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("popup"));
        style.fontSize = 12;
        style.fixedHeight = 18;
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        m_excelPath= EditorGUILayout.TextField("ExcelPath", m_excelPath);
        m_jsonListPath = EditorGUILayout.TextField("JsonListPath", m_jsonListPath);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("创建<Task>JSON"))
        {
            CreatTaskJsonExample();
        }
        if (GUILayout.Button("创建<TaskStep>JSON"))
        {
            CreatTaskStepJsonExample();
        }
        if (GUILayout.Button("创建<TaskHotspotGroup>JSON"))
        {
            CreatTaskHotspotGroupJsonExample();
        }
        if (GUILayout.Button("创建<Tool>JSON"))
        {
            CreatToolJsonExample();
        }
        if (GUILayout.Button("解析JSON"))
        {
            AnalysisTaskJson();
        }

        GUILayout.EndScrollView();

        GUILayout.EndVertical();
    }

    private void CreatTaskJsonExample()
    {
        Debug.Log("SaveJson");
        //List<T> -> Json ( 例 : List<Enemy> )  
        List<Task> tasks = ReadExcel.GameReadTaskExcel(m_excelPath);
        //string str = JsonUtility.ToJson(tasks); // 输出 : { }
        string str = JsonUtility.ToJson(new Serialization<Task>(tasks));
        File.WriteAllText(Application.streamingAssetsPath + m_jsonListPath, str);
        Debug.Log(str);
    }

    private void CreatTaskStepJsonExample()
    {
        Debug.Log("SaveJson");
        //List<T> -> Json ( 例 : List<Enemy> )  
        List<TaskStep> tasks = ReadExcel.GameReadExcel(m_excelPath);
        //string str = JsonUtility.ToJson(tasks); // 输出 : { }
        string str = JsonUtility.ToJson(new Serialization<TaskStep>(tasks));
        File.WriteAllText(Application.streamingAssetsPath + m_jsonListPath, str);
        Debug.Log(str);
    }

    private void CreatTaskHotspotGroupJsonExample()
    {
        Debug.Log("SaveJson");
        //List<T> -> Json ( 例 : List<Enemy> )  
        List<TaskHotspotGroup> tasks = ReadExcel.ReadTaskHotspotGroup(m_excelPath);
        //string str = JsonUtility.ToJson(tasks); // 输出 : { }
        string str = JsonUtility.ToJson(new Serialization<TaskHotspotGroup>(tasks));
        File.WriteAllText(Application.streamingAssetsPath + m_jsonListPath, str);
        Debug.Log(str);
    }

    private void CreatToolJsonExample()
    {
        Debug.Log("SaveJson");
        //List<T> -> Json ( 例 : List<Enemy> )  
        List<ToolInfo> tasks = ReadExcel.ReadToolExcel(m_excelPath);
        //string str = JsonUtility.ToJson(tasks); // 输出 : { }
        string str = JsonUtility.ToJson(new Serialization<ToolInfo>(tasks));
        File.WriteAllText(Application.streamingAssetsPath + m_jsonListPath, str);
        Debug.Log(str);
    }

    private void AnalysisTaskJson()
    {
        var content = File.ReadAllText(Application.streamingAssetsPath + m_jsonListPath);
        TaskRoot taskRoot = JsonConvert.DeserializeObject<TaskRoot>(content);
        foreach (var item in taskRoot.tasks)
        {
            Debug.Log(item.m_taskCode);
        }
    }
}

// Serialization.cs
/*
 * 
 * Unity5.3从开始追加的JsonUtility，但是对于List 和Dictionary不能被直接序列化存储.
 * 在unity的官方网站,ISerializationCallbackReceiver继承的方法被提出,用的时候把此脚本放到项目里.
 * 
*/
[Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

// Dictionary<TKey, TValue>
[Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;

    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }
    }
}


