//using longliang.GetStreamingAssetsPath;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

/// <summary>
/// 认知学习--认知测试
/// </summary>
public class CognitiveTestPageUI : MonoBehaviour
{
    public Button m_returnBtn;

    public CustomOptionListViewIcons m_customOptionListViewIcons;
    public CustomTileViewTiKa m_daTiCustomTileViewTiKa;
    private static string m_optionPath = "\\Option\\" + "OptionItem.xml";
    private OptionItemData m_optionUIItemData;
    private List<int> m_optionIndexArray = new List<int>();

    public Button m_submitBtn;
    public RectTransform m_getScoreRect;
    private bool m_isSubmit = false;

    private int hour;
    private int minute;
    private int second;
    public TextMeshProUGUI m_hourLabel;
    public TextMeshProUGUI m_minuteLabel;
    public TextMeshProUGUI m_secondLabel;
    public TextMeshProUGUI m_totalScoreLabel;

    private int m_score;

    private void Awake()
    {
        m_submitBtn.onClick.AddListener(OnSubmitBtnClick);
    }

    private void Start()
    {
        StartCoroutine(GetUIXML_Coroutine(m_optionPath));
    }

    public int[] GetRandoms(int sum, int min, int max)
    {
        int[] arr = new int[sum];
        int j = 0;
        //表示键和值对的集合。
        Hashtable hashtable = new Hashtable();
        System.Random rm = new System.Random();
        while (hashtable.Count < sum)
        {
            //返回一个min到max之间的随机数
            int nValue = rm.Next(min, max);
            // 是否包含特定值
            if (!hashtable.ContainsValue(nValue))
            {
                //把键和值添加到hashtable
                hashtable.Add(nValue, nValue);
                arr[j] = nValue;

                j++;
            }
        }
        return arr;
    }

    public IEnumerator GetUIXML_Coroutine(string fileName)
    {
        //GetUrlClass getUrlClass = new GetUrlClass();
#if UNITY_EDITOR
        fileName = Application.streamingAssetsPath + fileName;
#else
        fileName = getUrlClass.GetUrlPath() + fileName; 
        Debug.Log("Web:"+fileName);
#endif
        WWW w = new WWW(fileName);
        if (!System.IO.File.Exists(fileName))
        {
            Debug.LogError("not find file");
        }
        yield return w;
        string xml = XMLManager.get_uft8(w.text);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);
        if (xmlDoc != null)
        {
            string allXML = Regex.Replace(xmlDoc.InnerXml, "\"true\"", "\"true\"", RegexOptions.IgnoreCase);
            allXML = Regex.Replace(allXML, "\"false\"", "\"false\"", RegexOptions.IgnoreCase);
            m_optionUIItemData = (XmlUtil.Deserialize(typeof(OptionItemData), allXML) as OptionItemData);
        }
        GetPortionOptionItemIndex();
        InitCustomOptionListViewIcons();
        InitDaTiCustomListViewIcons();
    }

    public void GetPortionOptionItemIndex()
    {
        m_optionIndexArray.Clear();


        for (int i = 0; i < m_optionUIItemData.m_OptionItemData.m_OptionItemList.Count; i++)
        {
            m_optionIndexArray.Add(i);
        }
    }

    public void InitCustomOptionListViewIcons()
    {
        m_customOptionListViewIcons.DataSource.Clear();
        m_customOptionListViewIcons.DataSource.BeginUpdate();
        for (int i = 0; i < m_optionIndexArray.Count; i++)
        {
            OptionItem optionItem = m_optionUIItemData.m_OptionItemData.m_OptionItemList[m_optionIndexArray[i]];         
            CustomOptionListViewItemDescription customOptionListViewItemDescription = new CustomOptionListViewItemDescription();
            customOptionListViewItemDescription.Name = i.ToString();
            customOptionListViewItemDescription.TitleName = (i + 1) + "、" + optionItem.m_title.Replace(" ", "");
            List<string> optionDatas = optionItem.m_optionDatas.Split(']').ToList();
            optionDatas.Remove("");
            customOptionListViewItemDescription.OptionNameA = optionDatas[0].Replace("[", "").Replace(" ", "");
            customOptionListViewItemDescription.OptionNameB = optionDatas[1].Replace("[", "").Replace(" ", "");
            if (optionDatas.Count == 5)
            {
                customOptionListViewItemDescription.OptionNameC = optionDatas[2].Replace("[", "").Replace(" ", "");
                customOptionListViewItemDescription.OptionNameD = optionDatas[3].Replace("[", "").Replace(" ", "");
                customOptionListViewItemDescription.OptionNameE = optionDatas[4].Replace("[", "").Replace(" ", "");
            }
            else if (optionDatas.Count == 4)
            {
                customOptionListViewItemDescription.OptionNameC = optionDatas[2].Replace("[", "").Replace(" ", "");
                customOptionListViewItemDescription.OptionNameD = optionDatas[3].Replace("[", "").Replace(" ", "");
                customOptionListViewItemDescription.OptionNameE = " ";
            }
            else if (optionDatas.Count == 3)
            {
                customOptionListViewItemDescription.OptionNameC = optionDatas[2].Replace("[", "").Replace(" ", "");
                customOptionListViewItemDescription.OptionNameD = " ";
                customOptionListViewItemDescription.OptionNameE = " ";
            }
            else
            {
                customOptionListViewItemDescription.OptionNameC = " ";
                customOptionListViewItemDescription.OptionNameD = " ";
                customOptionListViewItemDescription.OptionNameE = " ";
            }

            customOptionListViewItemDescription.OptionNameAnswer = optionItem.m_answer;
            m_customOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription);
        }
        m_customOptionListViewIcons.DataSource.EndUpdate();
    }

    public void InitDaTiCustomListViewIcons()
    {
        m_daTiCustomTileViewTiKa.DataSource.Clear();
        m_daTiCustomTileViewTiKa.DataSource.BeginUpdate();
        for (int i = 0; i < m_optionIndexArray.Count; i++)
        {
            CustomTileViewItemDescription customTileViewItemDescription = new CustomTileViewItemDescription();
            customTileViewItemDescription.Name = (i + 1).ToString();
            //customTileViewItemDescription.Icon = Resources.Load("UI/default", typeof(Sprite)) as Sprite;
            customTileViewItemDescription.Value = 0;
            m_daTiCustomTileViewTiKa.DataSource.Add(customTileViewItemDescription);
        }
        m_daTiCustomTileViewTiKa.DataSource.EndUpdate();
    }

    public void OnSubmitBtnClick()
    {
        m_getScoreRect.gameObject.SetActive(true);
        if (!m_isSubmit)
        {
            for (int i = 0; i < m_customOptionListViewIcons.DataSource.Count; i++)
            {
                m_customOptionListViewIcons.DataSource[i].ShowAnswerTip = true;

                if (m_customOptionListViewIcons.DataSource[i].SelectBtnIndex.Count == 0)
                {
                    m_daTiCustomTileViewTiKa.DataSource[i].Value = 1;
                }
                else
                {
                    m_daTiCustomTileViewTiKa.DataSource[i].Value = 2;
                }
                m_daTiCustomTileViewTiKa.UpdateItems();

                if (m_customOptionListViewIcons.DataSource[i].Correct)
                {
                    m_score++;
                }
            }

            CustomOptionListViewIconsComponent[] customOptionListViewIconsComponents = m_customOptionListViewIcons.transform.GetComponentsInChildren<CustomOptionListViewIconsComponent>();
            for (int i = 0; i < customOptionListViewIconsComponents.Length; i++)
            {
                for (int j = 0; j < customOptionListViewIconsComponents[i].OptionImages.Length; j++)
                {
                    customOptionListViewIconsComponents[i].OptionImages[j].GetComponent<Button>().interactable = false;
                }
                customOptionListViewIconsComponents[i].SetAnswerTip();
            }

        }
        
        m_totalScoreLabel.text = Mathf.RoundToInt(m_score).ToString();
        m_isSubmit = true;
        //UserInfo.Instance.m_scoreDic[ScoreType.CognitiveStudy_Exam] = m_score;
    }                                      

    public void OnCustomOptionListViewSliderValueChange(float value)
    {
        CustomOptionListViewIconsComponent[] customOptionListViewIconsComponents = m_customOptionListViewIcons.transform.GetComponentsInChildren<CustomOptionListViewIconsComponent>();
        for (int i = 0; i < customOptionListViewIconsComponents.Length; i++)
        {
            customOptionListViewIconsComponents[i].SetAnswerTip();
        }
    }

    //private void Update()
    //{
    //    hour = (int)(CommonManager.Instance.m_timer / 3600);
    //    minute = (int)((CommonManager.Instance.m_timer - hour * 3600) / 60);
    //    second = (int)(CommonManager.Instance.m_timer - hour * 3600 - minute * 60);
    //    m_hourLabel.text = hour.ToString();
    //    m_minuteLabel.text = minute.ToString();
    //    m_secondLabel.text = second.ToString();
    //}
}
