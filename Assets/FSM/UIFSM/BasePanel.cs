/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour, IUIState
{
    //所属的状态机
    protected FiniteStateMachine m_finiteStateMachine = new FiniteStateMachine();
    //状态机名称
    protected string m_stateName;
    //状态之间的过渡（或桥梁）,用于状态间的切换
    protected Dictionary<string, string> m_transitionDic = new Dictionary<string, string>();

    //初始化的时候，需要将自身注册进来
    protected virtual void Init()
    {
        m_stateName = GetType().Name;
        m_finiteStateMachine.Register(m_stateName, this);

        //foreach (KeyValuePair<string, string> pair in m_transitionDic)
        //{
        //    m_finiteStateMachine.GetState(m_stateName).On(pair.Key).Enter(pair.Value);
        //}
    }

    public virtual void Update()
    {
        OnUpdate();
    }

    public virtual void OnEnter(string prevState)
    {
        
    }

    public virtual void OnExit(string nextState)
    {
     
    }

    public virtual void OnUpdate()
    {
        m_finiteStateMachine.Update();
    }

    public virtual void Hide()
    {
       
    }

    public virtual void Show()
    {
        
    }
}
