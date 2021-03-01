/*
/// 功能： 
/// 时间：
/// 版本：
*/

using NY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NY
{
    public class PageThree : BaseUIMonoBehaviour
    {
        public Button m_btn;

        private void Start()
        {
            m_belongFSM.RegisterState(UIPageConfig.Page3ToPage2, m_belongFSM.m_allStateDic[typeof(PageTwo).FullName]);
            m_btn.onClick.AddListener(delegate { m_belongFSM.Trriger(UIPageConfig.Page3ToPage2); });
        }
    }
}
