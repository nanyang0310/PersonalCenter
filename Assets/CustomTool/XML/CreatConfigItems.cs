using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[CreateAssetMenu(menuName= "MyConfig/Create ConfigItems")]
public class CreatConfigItems : ScriptableObject
{
    public List<ConfigItem> m_ConfigItemsList;
}

[XmlRoot("ConfigItemDatas")]
public class ConfigItemData
{
    [XmlElement("ConfigItemData")]
    public ConfigItemList m_configItemData = new ConfigItemList();
}

[System.Serializable]
public class ConfigItemList
{
    [XmlElement("ConfigItemLisList")]
    public List<ConfigItem> m_ConfigItemList;
}

[System.Serializable]
public class ConfigItem
{
    [XmlAttribute("Name")]
    public string m_name;
    [XmlAttribute("Value")]
    public string m_value;
    [XmlAttribute("Comment")]
    public string m_comment;
}
