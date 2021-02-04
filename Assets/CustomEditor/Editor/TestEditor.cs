/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditor : EditorWindow
{
    [MenuItem("Custom/TestEditor")]
    public static void Set()
    {
        TestEditor window = (TestEditor)EditorWindow.GetWindow(typeof(TestEditor), false, "Test Editor");
        window.Show();
        EditorWindow.GetWindow(typeof(TestEditor));
    }

    public GUISkin mySkin;
    public Texture micon;

    private void OnEnable()
    {
        micon = LoadTexture("wechat.jpg");
    }

    private void OnGUI()
    {
        string path = "Assets/CustomEditor/";
        mySkin = (GUISkin)AssetDatabase.LoadAssetAtPath(path + "NYGUISkin.guiskin", typeof(GUISkin));
        GUI.skin = mySkin;

        GUILayout.Label(GUIContent.none, "DefaultButton", GUILayout.ExpandWidth(true));
    }

    Texture LoadTexture(string name)
    {
        string path = "Assets/Demigiant/Editor/";
        return (Texture)AssetDatabase.LoadAssetAtPath(path + name, typeof(Texture));
    }
}
