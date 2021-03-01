/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NY
{
    public class PageTwo : BaseUIMonoBehaviour
    {
        public Button m_preBtn;
        public Button m_page1Btn;
        public Button m_popBtn;

        protected override void Awake()
        {
            base.Awake();
            m_messageCenter.Register(UIPageConfig.SendMessageToPage2, TestDebug);
        }

        private void Start()
        {     
            m_belongFSM.RegisterState(UIPageConfig.Page2ToPage3, m_belongFSM.m_allStateDic[typeof(PageThree).FullName]);
            m_belongFSM.RegisterState(UIPageConfig.Page2ToPage1, m_belongFSM.m_allStateDic[typeof(PageOne).FullName]);

            m_popBtn.onClick.AddListener(delegate { m_belongFSM.Trriger(UIPageConfig.Page2ToPage3, StateType.POP); });
            m_preBtn.onClick.AddListener(delegate { m_belongFSM.GotoPreState(); });
            m_page1Btn.onClick.AddListener(delegate { m_belongFSM.Trriger(UIPageConfig.Page2ToPage1); });
        }

        public void TestDebug(object obj)
        {
            Debug.Log("obj " + obj.GetType().FullName);
        }
    }
}


