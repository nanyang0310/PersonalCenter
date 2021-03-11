/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NY;
using DG.Tweening;
using UnityEngine.UI;

public partial class TestCube : MonoBehaviour
{
    List<string> list1 = new List<string>();
    List<string> list2 = new List<string>();
    List<Task> tasks = new List<Task>();

    public Action m_action;

    private void Start()
    {
        //List<Task> temp = tasks.FindAll(p => p.m_id == 1);

        //MessageCenter.Register("Debug", x,y => TestDebug(y));
        //MessageCenter.Register("Debug", (object x, object y) => TestDebug(x, y));  //注意 如果采取匿名委托的方式，注销的时候无效，
        MessageCenter.Register("Debug", TestDebug);
        MessageCenter.Register("Debug", (x) => TestDebugError(x));
        MessageCenter.Send("Debug", "注销前的测试1");
        MessageCenter.UnRegister("Debug", TestDebug);
        MessageCenter.Send("Debug", "注销后的测试1");
    }

    public void TestDebug(object s)
    {
        Debug.Log(s.ToString());
    }

    public void TestDebugError(object s)
    {
        Debug.LogError(s.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEditor.EditorApplication.isPlaying = true;
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
