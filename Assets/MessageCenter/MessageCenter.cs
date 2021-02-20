/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MessageCenter
{
    private static Dictionary<string, Action<object,object>> m_messageCenterDic = new Dictionary<string, Action<object, object>>();

    public static void Register(string name, Action<object, object> action)
    {
        if (!m_messageCenterDic.ContainsKey(name))
        {
            //m_messageCenterDic.Add(name, default);
            //m_messageCenterDic.Add(name, action);
            m_messageCenterDic.Add(name, (x, y) => { });
        }
        m_messageCenterDic[name] += action;
    }

    public static void UnRegister(string name)
    {
        m_messageCenterDic.Remove(name);
    }

    public static void Send(string name, object obj1, object obj2)
    {
        m_messageCenterDic[name](obj1, obj2);
    }
}
