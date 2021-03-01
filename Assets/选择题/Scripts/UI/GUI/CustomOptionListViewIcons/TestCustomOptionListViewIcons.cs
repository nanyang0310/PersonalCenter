using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCustomOptionListViewIcons : MonoBehaviour
{
    public CustomOptionListViewIcons m_CustomOptionListViewIcons;

    private void Start()
    {
        m_CustomOptionListViewIcons.DataSource.Clear();
        m_CustomOptionListViewIcons.DataSource.BeginUpdate();

        CustomOptionListViewItemDescription customOptionListViewItemDescription_1 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_1.Name = "1";
        customOptionListViewItemDescription_1.TitleName = "1、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_1.OptionNameA = "A.高于";
        customOptionListViewItemDescription_1.OptionNameB = "B.高于";
        customOptionListViewItemDescription_1.OptionNameC = "C.高于";
        customOptionListViewItemDescription_1.OptionNameD = "D.高于";
        customOptionListViewItemDescription_1.OptionNameAnswer = "A,B";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_1);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_2 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_2.Name = "2";
        customOptionListViewItemDescription_2.TitleName = "2、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_2.OptionNameA = "A.高于";
        customOptionListViewItemDescription_2.OptionNameB = "B.高于";
        customOptionListViewItemDescription_2.OptionNameC = "";
        customOptionListViewItemDescription_2.OptionNameD = "";
        customOptionListViewItemDescription_2.OptionNameAnswer = "A";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_2);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_3 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_3.Name = "3";
        customOptionListViewItemDescription_3.TitleName = "3、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_3.OptionNameA = "A.高于";
        customOptionListViewItemDescription_3.OptionNameB = "B.高于";
        customOptionListViewItemDescription_3.OptionNameC = "C.高于";
        customOptionListViewItemDescription_3.OptionNameD = "D.高于";
        customOptionListViewItemDescription_3.OptionNameAnswer = "A";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_3);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_4 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_4.Name = "4";
        customOptionListViewItemDescription_4.TitleName = "4、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_4.OptionNameA = "A.高于";
        customOptionListViewItemDescription_4.OptionNameB = "B.高于";
        customOptionListViewItemDescription_4.OptionNameC = "";
        customOptionListViewItemDescription_4.OptionNameD = "";
        customOptionListViewItemDescription_4.OptionNameAnswer = "A";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_4);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_5 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_5.Name = "15";
        customOptionListViewItemDescription_5.TitleName = "5、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_5.OptionNameA = "A.高于";
        customOptionListViewItemDescription_5.OptionNameB = "B.高于";
        customOptionListViewItemDescription_5.OptionNameC = "C.高于";
        customOptionListViewItemDescription_5.OptionNameD = "D.高于";
        customOptionListViewItemDescription_5.OptionNameAnswer = "A,B,C,D";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_5);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_6 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_6.Name = "6";
        customOptionListViewItemDescription_6.TitleName = "6、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_6.OptionNameA = "A.高于";
        customOptionListViewItemDescription_6.OptionNameB = "B.高于";
        customOptionListViewItemDescription_6.OptionNameC = "";
        customOptionListViewItemDescription_6.OptionNameD = "";
        customOptionListViewItemDescription_6.OptionNameAnswer = "A";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_6);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_7 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_7.Name = "7";
        customOptionListViewItemDescription_7.TitleName = "7、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_7.OptionNameA = "A.高于";
        customOptionListViewItemDescription_7.OptionNameB = "B.高于";
        customOptionListViewItemDescription_7.OptionNameC = "C.高于";
        customOptionListViewItemDescription_7.OptionNameD = "D.高于";
        customOptionListViewItemDescription_7.OptionNameAnswer = "A,C,D";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_7);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_8 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_8.Name = "8";
        customOptionListViewItemDescription_8.TitleName = "8、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_8.OptionNameA = "A.高于";
        customOptionListViewItemDescription_8.OptionNameB = "B.高于";
        customOptionListViewItemDescription_8.OptionNameC = "";
        customOptionListViewItemDescription_8.OptionNameD = "";
        customOptionListViewItemDescription_8.OptionNameAnswer = "A,B";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_8);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_9 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_9.Name = "9";
        customOptionListViewItemDescription_9.TitleName = "9、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_9.OptionNameA = "A.高于";
        customOptionListViewItemDescription_9.OptionNameB = "B.高于";
        customOptionListViewItemDescription_9.OptionNameC = "";
        customOptionListViewItemDescription_9.OptionNameD = "";
        customOptionListViewItemDescription_9.OptionNameAnswer = "A";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_9);

        CustomOptionListViewItemDescription customOptionListViewItemDescription_10 = new CustomOptionListViewItemDescription();
        customOptionListViewItemDescription_10.Name = "10";
        customOptionListViewItemDescription_10.TitleName = "10、线路首段电压一定( )末端电压";
        customOptionListViewItemDescription_10.OptionNameA = "A.高于";
        customOptionListViewItemDescription_10.OptionNameB = "A.高于";
        customOptionListViewItemDescription_10.OptionNameC = "C....";
        customOptionListViewItemDescription_10.OptionNameD = "D....";
        customOptionListViewItemDescription_10.OptionNameAnswer = "A,B,C,D";
        m_CustomOptionListViewIcons.DataSource.Add(customOptionListViewItemDescription_10);

        m_CustomOptionListViewIcons.DataSource.EndUpdate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i < m_CustomOptionListViewIcons.DataSource.Count; i++)
            {
                m_CustomOptionListViewIcons.DataSource[i].ShowAnswerTip = true;
            }

            CustomOptionListViewIconsComponent[] customOptionListViewIconsComponents = m_CustomOptionListViewIcons.transform.GetComponentsInChildren<CustomOptionListViewIconsComponent>();
            for (int i = 0; i < customOptionListViewIconsComponents.Length; i++)
            {
                customOptionListViewIconsComponents[i].SetAnswerTip();
            }

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < m_CustomOptionListViewIcons.DataSource.Count; i++)
            {
                m_CustomOptionListViewIcons.DataSource[i].ShowAnswerTip = false;
            }
        }
    }

    public void OnCustomOptionListViewSliderValueChange(float value)
    {
        CustomOptionListViewIconsComponent[] customOptionListViewIconsComponents = m_CustomOptionListViewIcons.transform.GetComponentsInChildren<CustomOptionListViewIconsComponent>();
        for (int i = 0; i < customOptionListViewIconsComponents.Length; i++)
        {
            customOptionListViewIconsComponents[i].SetAnswerTip();
        }
    }
}
