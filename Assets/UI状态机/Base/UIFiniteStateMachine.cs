/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NY
{
    public class UIFiniteStateMachine
    {
        public UIFiniteStateMachine(string name)
        {
            m_fsmName = name;
        }
        public string m_fsmName;
        private IState m_currState;
        public Dictionary<string, IState> m_allStateDic = new Dictionary<string, IState>();
        public Stack<IState> m_stateStack = new Stack<IState>();

        public void EntryPoint(string startName)
        {
            Trriger(startName);
        }

        //注册
        public void RegisterState(string stateName, IState state)
        {
            if (m_allStateDic.ContainsKey(stateName))
            {
                Debug.LogError("stateName: " + stateName + " 之前存储过");
            }
            m_allStateDic[stateName] = state;
        }

        public void UnReisterState(string stateName)
        {
            if (m_allStateDic.ContainsKey(stateName))
            {
                m_allStateDic.Remove(stateName);
            }
            m_stateStack.Pop();
        }

        public void Trriger(string stateName, StateType stateType = StateType.DEFAULT)
        {
            if (m_currState != null)
            {
                m_currState.OnExit(stateType);
            }
            IState state;
            if (m_allStateDic.TryGetValue(stateName, out state))
            {
                m_currState = state;
            }
            else
            {
                Debug.LogError("stateName " + stateName + " 未注册!");
                return;
            }
            m_stateStack.Push(state);
            m_currState.OnEnter(stateName);
        }

        public void GotoPreState(StateType stateType = StateType.DEFAULT)
        {
            if (m_currState != null)
            {
                m_currState.OnExit(stateType);
            }
            m_stateStack.Pop();        
            IState state = m_stateStack.Peek();
            string stateName = GetStateName(state);
            m_currState = state;
            m_currState.OnEnter(stateName);
        }

        private string GetStateName(IState state)
        {
            foreach (var item in m_allStateDic)
            {
                if (item.Value == state)
                {
                    return item.Key;
                }
            }
            return null;
        }
    }
}
