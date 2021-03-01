/*
/// 功能： 状态机之间的消息中心
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NY
{
    public class FiniteStateMachineMessageCenter
    {
        public Dictionary<string, Action<object>> m_allMessageDic = new Dictionary<string, Action<object>>();

        public void Execute(string name,object obj)
        {
            m_allMessageDic[name].Invoke(obj);
        }

        public void Register(string name, Action<object> action)
        {
            if (!m_allMessageDic.ContainsKey(name))
            {
                m_allMessageDic.Add(name, default);
            }
            m_allMessageDic[name] += action;
        }

        public void UnRegister(string name)
        {
            m_allMessageDic.Remove(name);
        }

        public void UnRegister(string name, Action<object> action)
        {
            if (m_allMessageDic.ContainsKey(name))
            {
                m_allMessageDic[name] -= action;
            }
        }
    }
}
