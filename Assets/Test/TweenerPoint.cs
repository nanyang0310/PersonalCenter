/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenerPoint : MonoBehaviour
{
    [SerializeField, TooltipAttribute("不允许修改")]
    private string m_modelName;

    public string ModelName
    {
        get { return m_modelName; }
        set { m_modelName = value; }
    }

    [SerializeField]
    private int m_pointIndex;

    public int PointIndex
    {
        get { return m_pointIndex; }
        set { m_pointIndex = value; }
    }
    [SerializeField]
    private float m_timer;

    public float Timer
    {
        get { return m_timer; }
        set { m_timer = value; }
    }

    private string m_defaultModelName;
    private int m_defaultPointIndex;
    private float m_defaultTimer;
    private Vector3 m_defaultPos;
    private Quaternion m_defaultRotion;
    private bool m_isPlay;
    private GameObject m_nextPoint;

    private GameObject m_next;



    public void DefaultPoint()
    {
        m_defaultModelName = m_modelName;
        m_defaultPointIndex = m_pointIndex;
        m_defaultTimer = m_timer;
        m_defaultPos = this.transform.localPosition;
        m_defaultRotion = this.transform.localRotation;
    }

    public void DefaultPoint(string modelName, int pointIndex, float timer, Vector3 pos, Quaternion rotion)
    {
        m_defaultModelName = modelName;
        m_defaultPointIndex = pointIndex;
        m_defaultTimer = timer;
        m_defaultPos = pos;
        m_defaultRotion = rotion;
    }

    [ContextMenu("SavePoint（保存）")]
    private void SavePoint()
    {
        if (m_pointIndex <= 0)
        {
            return;
        }
        if (m_modelName != m_defaultModelName)
        {
            Debug.LogError("不允许修改名称和动画下标值");
            //return;
        }

    }

    [ContextMenu("ResetPoint(还原)")]
    private void ResetPoint()
    {
        m_modelName = m_defaultModelName;
        m_pointIndex = m_defaultPointIndex;
        m_timer = m_defaultTimer;
        this.transform.localPosition = m_defaultPos;
        this.transform.localRotation = m_defaultRotion;

        m_isPlay = false;
        CancelInvoke("InvokePlay");
    }

    [ContextMenu("AddPoint(添加)")]
    private void AddPoint()
    {
        
    }

    [ContextMenu("DeletePoint(移除)")]
    private void DeletePoint()
    {

    }

    [ContextMenu("PlayPoint(播放)")]
    private void PlayPoint()
    {

    }

    public void InvokePlay()
    {
        if (m_isPlay)
        {
            if (m_next != null)
            {
                if (Vector3.Distance(this.transform.localPosition, m_next.transform.localPosition) < 0.01f)
                {
                    CancelInvoke("InvokePlay");
                    m_isPlay = false;
                }
                else
                {
                    Vector3 dir = m_next.transform.localPosition - this.transform.localPosition;
                    this.transform.Translate(dir.normalized * 0.01f);
                }
            }
        }
    }


    public string StringVector3(Vector3 vec)
    {
        string s = string.Format("{0},{1},{2}", vec.x, vec.y, vec.z);
        return s;
    }

    public string StringQuaternion(Quaternion qua)
    {
        string s = string.Format("{0},{1},{2},{3}", qua.x, qua.y, qua.z, qua.w);
        return s;
    }
}



