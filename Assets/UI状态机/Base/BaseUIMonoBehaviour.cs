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
    [RequireComponent(typeof(CanvasGroup))]
    public class BaseUIMonoBehaviour : MonoBehaviour, IState
    {
        protected static UIFiniteStateMachine m_belongFSM;
        protected static FiniteStateMachineMessageCenter m_messageCenter;

        protected virtual void Awake()
        {
            if (m_belongFSM == null)
            {
                m_belongFSM = StateMachineManager.Instance.m_uiFSM;
            }
            if (m_messageCenter == null)
            {
                m_messageCenter = new FiniteStateMachineMessageCenter();
            }
        }

        protected virtual void InitState()
        {
            string stateName = GetType().Name;
        }

        string GetPageName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }


        public virtual void OnEnter(string stateName)
        {
#if UNITY_EDITOR
            Debug.Log("OnEnter " + stateName);
#endif
            this.GetComponent<CanvasGroup>().interactable = true;
            this.gameObject.SetActive(true);
        }

        public virtual void OnExit(StateType stateType)
        {
#if UNITY_EDITOR
            Debug.Log("OnExit ");
#endif
            switch (stateType)
            {
                case StateType.DEFAULT:
                    this.gameObject.SetActive(false);
                    break;
                case StateType.POP:
                    break;
                default:
                    break;
            }
            this.GetComponent<CanvasGroup>().interactable = false;
        }

        public virtual void OnUpdate(string stateName)
        {
            throw new System.NotImplementedException();
        }
    }
}

