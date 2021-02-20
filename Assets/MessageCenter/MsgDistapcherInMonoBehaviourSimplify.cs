/*
/// 功能： 
/// 时间：
/// 版本：
*/

using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgDistapcherInMonoBehaviourSimplify : MonoBehaviourSimplify
{
    private void Awake()
    {
        RegisterMsg("Do", DoSomething);
        RegisterMsg("Do2", DoSomething);
    }
    private IEnumerator Start()
    {
        MessageCenter.Send("Do", "hello");

        yield return new WaitForSeconds(1.0f);

        MessageCenter.Send("Do2", "hello1");
    }

    void DoSomething(object data)
    {
        // do something
        Debug.LogFormat("Received Do msg:{0}", data);
    }

    protected override void OnBeforeDestroy()
    {

    }
}

