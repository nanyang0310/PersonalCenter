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
    [CreateAssetMenu(menuName = "NY/UI Page Asset")]
    public class UIPageAsset : ScriptableObject
    {
        public List<UIPageAssetItem> uIPageAssetItems;
    }

    [System.Serializable]
    public class UIPageAssetItem
    {
        public GameObject m_page;
    }
}

