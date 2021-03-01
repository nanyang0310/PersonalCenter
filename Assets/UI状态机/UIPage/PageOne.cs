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
    public class PageOne : BaseUIMonoBehaviour
    {
        public Button m_btn;

        protected void InitPage()
        {
            m_belongFSM.RegisterState(UIPageConfig.Page1ToPage2, m_belongFSM.m_allStateDic[typeof(PageTwo).FullName]);
        }

        private void Start()
        {
            InitPage();
            m_btn.onClick.AddListener(OnClickBtn);
        }

        private void OnClickBtn()
        {
            m_belongFSM.Trriger(UIPageConfig.Page1ToPage2);
            m_messageCenter.Execute(UIPageConfig.SendMessageToPage2, this);
        }
    }
}


