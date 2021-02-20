/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class TestCube : MonoBehaviour
{
    List<string> list1 = new List<string>();
    List<string> list2 = new List<string>();

    public Action m_action;

    private void Start()
    {
        //Rigidbody rigidbody = this.transform.AddAndGetComponent<Rigidbody>();
        //rigidbody.SetActive(false);

        //list1.Add("555");
        //list2.Add("7777", "555");
        //list1.AddRange(list2);
        //list1.EliminateRepeat();
        //List<string> list3 = list1.GetRepeat(list2);
        //list1.CopyToEliminateRepeat(list2);
        //foreach (var item in list1)
        //{
        //    Debug.Log(item);
        //}

        //MessageCenter.Register("Debug", x,y => TestDebug(y));
        MessageCenter.Register("Debug", (object x, object y) => TestDebug(x, y));
        MessageCenter.Register("Debug", (x, y) => TestDebugError(x, y));
        MessageCenter.Send("Debug", "我是你爸爸", "我是你爸爸2");
    }

    public void TestDebug(object s, object s2)
    {
        Debug.Log(s.ToString() + s2.ToString());
    }

    public void TestDebugError(object s, object s2)
    {
        Debug.LogError(s.ToString() + s2.ToString());
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
