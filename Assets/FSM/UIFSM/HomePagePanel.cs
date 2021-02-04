/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePagePanel : BasePanel
{
    private const string m_click="ClickBtn";
   private string m_page1= typeof(PageOne).Name;

    public Button m_btn;

    private void Awake()
    {
        m_finiteStateMachine.EntryPoint(typeof(HomePagePanel).Name);

        Init();
    }

    protected override void Init()
    {
        base.Init();
        m_finiteStateMachine.GetState(m_stateName).On(m_click).Enter(m_page1);
    }

    private void Start()
    {
        m_btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        m_finiteStateMachine.Trigger(m_click);
    }

    public override void OnEnter(string prevState)
    {
        base.OnEnter(prevState);
        this.gameObject.SetActive(true);
    }
}
