using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidgets;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class CustomOptionListViewIconsComponent : ListViewItem, IViewData<CustomOptionListViewItemDescription>
{
    [Header("选择题类型")]
    public ChoiceQuestionType m_choiceQuestionType;

    public string[] m_optionSerialNum;

    [SerializeField]
    protected string titleName;

    [SerializeField]
    protected string data;

    [SerializeField]
    protected string optionNameA;

    [SerializeField]
    protected string optionNameB;

    [SerializeField]
    protected string optionNameC;

    [SerializeField]
    protected string optionNameD;

    [SerializeField]
    protected string optionNameE;

    [SerializeField]
    protected string optionNameAnswer;


    /// <summary>
    /// This image will be shown on click.
    /// </summary>
    [SerializeField]
    public Sprite[] OptionSprites = new Sprite[2];

    /// <summary>
    /// This image will be shown on isAnswer.
    /// </summary>
    [SerializeField]
    public Sprite[] AnswerSprites = new Sprite[2];

    /// <summary>
    /// This image will be shown on click.
    /// </summary>
    [SerializeField]
    public Image[] OptionImages;

    /// <summary>
    /// CustomListView.
    /// </summary>
    public CustomOptionListViewIcons ParentList;

    [SerializeField]
    public Image AnswerImage;

    /// <summary>
    /// Text.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI AnswerTip;

    /// <summary>
    /// Foreground graphics for coloring.
    /// </summary>
    public override Graphic[] GraphicsForeground
    {
        get
        {
            return new Graphic[] { TextTMPro, OptionNameATMPro, OptionNameBTMPro, OptionNameCTMPro, OptionNameDTMPro, };
        }
    }

    /// <summary>
    /// Text.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI TextTMPro;

    /// <summary>
    /// OptionNameAText
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI OptionNameATMPro;

    /// <summary>
    /// OptionNameBText.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI OptionNameBTMPro;

    /// <summary>
    /// OptionNameCText.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI OptionNameCTMPro;

    /// <summary>
    /// OptionNameDText.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI OptionNameDTMPro;

    /// <summary>
    /// OptionNameEText.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI OptionNameETMPro;

    /// <summary>
    /// Item.
    /// </summary>
    [NonSerialized]
    public CustomOptionListViewItemDescription Item;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < OptionImages.Length; i++)
        {
            if (OptionImages[i].GetComponent<Button>())
            {
                int j = i;
                OptionImages[i].GetComponent<Button>().onClick.AddListener(() => OnOptionButtonClick(j));
            }
        }
    }
    /// <summary>
    /// Sets component data with specified item.
    /// </summary>
    /// <param name="item">Item.</param>
    public virtual void SetData(CustomOptionListViewItemDescription item)
    {
        Item = item;
        data = Item.Data;
        name = item.Name;
        titleName = item.TitleName;
        optionNameA = item.OptionNameA;
        optionNameB = item.OptionNameB;
        optionNameC = item.OptionNameC;
        optionNameD = item.OptionNameD;
        optionNameE = item.OptionNameE;
        optionNameAnswer = item.OptionNameAnswer;

        List<string> answerArray = optionNameAnswer.Split(',').ToList<string>();
        answerArray.Remove(" ");
        answerArray.Remove("");
        if (answerArray.Count > 1)
        {
            m_choiceQuestionType = ChoiceQuestionType.Multiple;
            Item.IsMultiple = true;
        }

        if (Item == null)
        {
            TextTMPro.text = string.Empty;
            OptionNameATMPro.text = string.Empty;
            OptionNameBTMPro.text = string.Empty;
            OptionNameCTMPro.text = string.Empty;
            OptionNameDTMPro.text = string.Empty;
            OptionNameETMPro.text = string.Empty;
        }
        else
        {
            String text = Item.LocalizedName ?? Item.TitleName;
            TextTMPro.text = text.Replace("\\n", "\n");

            String textA = Item.LocalizedName ?? Item.OptionNameA;
            OptionNameATMPro.text = textA.Replace("\\n", "\n");

            String textB = Item.LocalizedName ?? Item.OptionNameB;
            OptionNameBTMPro.text = textB.Replace("\\n", "\n");

            String textC = Item.LocalizedName ?? Item.OptionNameC;
            OptionNameCTMPro.text = textC.Replace("\\n", "\n");

            String textD = Item.LocalizedName ?? Item.OptionNameD;
            OptionNameDTMPro.text = textD.Replace("\\n", "\n");

            String textE = Item.LocalizedName ?? Item.OptionNameE;
            OptionNameETMPro.text = textE.Replace("\\n", "\n");

        }

        SetOptionCD();
        SetOptionIconStateSprite();
    }

    //设置选项状态的图标
    private void SetOptionIconStateSprite()
    {
        //根据选中的，控制UI的状态
        for (int i = 0; i < OptionImages.Length; i++)
        {
            if (Item.SelectBtnIndex.Contains(i))
            {
                OptionImages[i].sprite = OptionSprites[1];
            }
            else
            {
                OptionImages[i].sprite = OptionSprites[0];
            }
        }
    }

    //设置选项C、D的值
    private void SetOptionCD()
    {
        if (Item.OptionNameC == null || Item.OptionNameC == "" || Item.OptionNameC == " ")
        {
            this.transform.Find("SpaceC").gameObject.SetActive(false);
            this.transform.Find("OptionIconC").gameObject.SetActive(false);
            this.transform.Find("OptionNameC").gameObject.SetActive(false);
        }
        else
        {
            this.transform.Find("SpaceC").gameObject.SetActive(true);
            this.transform.Find("OptionIconC").gameObject.SetActive(true);
            this.transform.Find("OptionNameC").gameObject.SetActive(true);
        }

        if (Item.OptionNameD == null || Item.OptionNameD == "" || Item.OptionNameD == " ")
        {
            this.transform.Find("SpaceD").gameObject.SetActive(false);
            this.transform.Find("OptionIconD").gameObject.SetActive(false);
            this.transform.Find("OptionNameD").gameObject.SetActive(false);
        }
        else
        {
            this.transform.Find("SpaceD").gameObject.SetActive(true);
            this.transform.Find("OptionIconD").gameObject.SetActive(true);
            this.transform.Find("OptionNameD").gameObject.SetActive(true);
        }

        if (Item.OptionNameE == null || Item.OptionNameE == "" || Item.OptionNameE == " ")
        {
            this.transform.Find("SpaceE").gameObject.SetActive(false);
            this.transform.Find("OptionIconE").gameObject.SetActive(false);
            this.transform.Find("OptionNameE").gameObject.SetActive(false);
        }
        else
        {
            this.transform.Find("SpaceE").gameObject.SetActive(true);
            this.transform.Find("OptionIconE").gameObject.SetActive(true);
            this.transform.Find("OptionNameE").gameObject.SetActive(true);
        }
    }

    /// 设置是否显示正确答案
    public void SetAnswerTip()
    {
        if (Item.IsMultiple)
        {
            m_choiceQuestionType = ChoiceQuestionType.Multiple;
        }
        else
        {
            m_choiceQuestionType = ChoiceQuestionType.Single; ;
        }
        if (Item.ShowAnswerTip)
        {
            if (Item.SelectBtnIndex.Count == 0)
            {
                AnswerImage.enabled = true;
                AnswerImage.sprite = AnswerSprites[0];
                AnswerTip.text = "未作答";
                AnswerTip.enabled = true;
            }
            else
            {
                if (Item.Correct)
                {
                    AnswerImage.enabled = true;
                    AnswerImage.sprite = AnswerSprites[1];
                    AnswerTip.text = "回答正确";
                    AnswerTip.enabled = true;
                }
                else
                {
                    AnswerImage.enabled = true;
                    AnswerImage.sprite = AnswerSprites[0];
                    AnswerTip.text = "回答错误,正确答案为：" + Item.OptionNameAnswer;
                    AnswerTip.enabled = true;
                }
            }
        }
        else
        {
            AnswerImage.enabled = false;
            AnswerTip.enabled = false;
        }
    }

    protected LayoutGroup layoutGroup;

    /// <summary>
    /// Current layout.
    /// </summary>
    public LayoutGroup LayoutGroup
    {
        get
        {
            if (layoutGroup == null)
            {
                layoutGroup = GetComponent<LayoutGroup>();
            }
            return layoutGroup;
        }
    }


    /// <summary>
    /// Set graphics colors.
    /// </summary>
    /// <param name="foregroundColor">Foreground color.</param>
    /// <param name="backgroundColor">Background color.</param>
    /// <param name="fadeDuration">Fade duration.</param>
    public override void GraphicsColoring(Color foregroundColor, Color backgroundColor, float fadeDuration = 0.0f)
    {
        base.GraphicsColoring(foregroundColor, backgroundColor, fadeDuration);

        if (!ParentList)
        {
            return;
        }
    }

    public void OnOptionButtonClick(int index)
    {
        bool isSelect = false;
        switch (m_choiceQuestionType)
        {
            case ChoiceQuestionType.Single:
                //单选
                for (int i = 0; i < OptionImages.Length; i++)
                {
                    if (i == index)
                    {
                        if (OptionImages[i].sprite == OptionSprites[0])
                        {
                            isSelect = true;
                            OptionImages[i].sprite = OptionSprites[1];
                            if (m_optionSerialNum[i] == optionNameAnswer)
                            {
                                Item.Correct = true;
                            }
                            else
                            {
                                Item.Correct = false;
                            }
                        }
                        else
                        {
                            OptionImages[i].sprite = OptionSprites[0];
                            isSelect = false;
                        }
                    }
                    else
                    {
                        OptionImages[i].sprite = OptionSprites[0];
                    }
                }
                if (isSelect)
                {
                    if (Item.SelectBtnIndex.Count > 0)
                    {
                        Item.SelectBtnIndex.Clear();
                    }
                    Item.SelectBtnIndex.Add(index);
                }
                else
                {
                    Item.SelectBtnIndex.Remove(index);
                }
                break;
            case ChoiceQuestionType.Multiple:  //多选 
                //1.添加或移除选项
                if (OptionImages[index].sprite == OptionSprites[0])
                {
                    if (Item.SelectBtnIndex.Contains(index))
                    {
                        Debug.LogErrorFormat("{0}包含了：{1}", Item.TitleName, index);
                    }
                    OptionImages[index].sprite = OptionSprites[1];
                    Item.SelectBtnIndex.Add(index);
                }
                else
                {
                    OptionImages[index].sprite = OptionSprites[0];
                    Item.SelectBtnIndex.Remove(index);
                }
                //2.重新排序
                Item.SelectBtnIndex.Sort();

                //3.组拼答案
                string answer = "";
                for (int i = 0; i < Item.SelectBtnIndex.Count; i++)
                {
                    if (i == Item.SelectBtnIndex.Count - 1)
                    {
                        answer += m_optionSerialNum[Item.SelectBtnIndex[i]];
                    }
                    else
                    {
                        answer += m_optionSerialNum[Item.SelectBtnIndex[i]] + ",";
                    }
                }
                //4.验证答案
                if (answer == optionNameAnswer)
                {
                    Item.Correct = true;
                }
                else
                {
                    Item.Correct = false;
                }
                break;
            default:
                break;
        }
    }
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
      
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
       
    }
}

/// <summary>
/// 选择题类型 单选 多选
/// </summary>
public enum ChoiceQuestionType
{
    Single,
    Multiple,
}