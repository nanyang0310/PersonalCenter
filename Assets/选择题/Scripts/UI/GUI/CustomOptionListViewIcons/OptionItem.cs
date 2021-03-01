using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class OptionItem 
{
    [XmlAttribute("Title")]
    public string m_title;
    [XmlAttribute("OptionDatas")]
    public string m_optionDatas;
    [XmlAttribute("Answer")]
    public string m_answer;
    [XmlAttribute("Type")]
    public string m_type;
}

[XmlRoot("OptionItemDatas")]
public class OptionItemData
{
    [XmlElement("OptionItemData")]
    public OptionItemList m_OptionItemData;
}

[System.Serializable]
public class OptionItemList
{
    [XmlElement("OptionItemLisList")]
    public List<OptionItem> m_OptionItemList;
}
