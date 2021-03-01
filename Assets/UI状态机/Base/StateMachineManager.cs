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
    public class StateMachineManager : Singleton<StateMachineManager>
    {
        protected StateMachineManager() { }

        private Dictionary<string, UIFiniteStateMachine> m_fsmDic = new Dictionary<string, UIFiniteStateMachine>();
        public BaseUIMonoBehaviour[] m_states;
        public UIFiniteStateMachine m_uiFSM;
        private string m_uiFSMName = "UIFSM";

        public override void Awake()
        {
            base.Awake();
            m_uiFSM = new UIFiniteStateMachine(m_uiFSMName);
            m_fsmDic.Add(m_uiFSMName, m_uiFSM);

            InitUIPageAsset();
            m_uiFSM.EntryPoint(typeof(PageOne).FullName);
        }

        private void InitUIPageAsset()
        {
            foreach (var item in m_states)
            {
                m_uiFSM.m_allStateDic.Add(item.GetType().FullName, item);
            }
        }
    }
}


