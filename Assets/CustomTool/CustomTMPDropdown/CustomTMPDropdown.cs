using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CustomTMPDropdown : TMP_Dropdown
{
    [Header("【选择是否正确】")]
    /// <summary>
    /// 选择是否正确
    /// </summary>
    public bool m_isCoorect = false;

    [Header("【正确答案的下标】")]
    [SerializeField] private int m_answerIndex = -1;

    [Header("【正确答案】")]
    [SerializeField] private string m_answer;

    //public CustomTMPDropdownUnityEvent  ValueChangeEndEvent;

    protected override void Awake()
    {
        base.Awake();
        verificationAnswer(value);
    }

    public void OnCustomTMPDropdownValueChange(int index)
    {
        verificationAnswer(index);
    }

    private void verificationAnswer(int index)
    {
        if (m_answerIndex < 0)
        {
            if (options[index].text == m_answer)
            {
                m_isCoorect = true;
            }
            else
            {
                m_isCoorect = false;
            }
        }
        else
        {
            if (index == m_answerIndex)
            {
                m_isCoorect = true;
            }
            else
            {
                m_isCoorect = false;
            }
        }
        Debug.Log("m_isCoorect:" + m_isCoorect);
    }
}

//public class CustomTMPDropdownUnityEvent : UnityEvent<CustomTMPDropdown>
//{

//}

