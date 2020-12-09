/*
/// 功能： 
/// 
/// 时间：
/// 
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateXML : MonoBehaviour
{
    public CreatConfigItems m_creatConfigItems;

    private void Start()
    {
        Create();
    }

    void Create()
    {
        InitUIXML();
    }

    public void InitUIXML()
    {
        ConfigItemData configItemData = new ConfigItemData();
        configItemData.m_configItemData.m_ConfigItemList = m_creatConfigItems.m_ConfigItemsList;

        XMLManager.CreatSerializerXML<ConfigItemData>("PanelUIBase.xml", configItemData);
    }
}
