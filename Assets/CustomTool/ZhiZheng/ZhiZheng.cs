using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ZhiZheng : MonoBehaviour
{
    public GameObject m_model;
    [Header("model的localEulerAngles参数")]
    public float m_localEulerAnglesMinValue;  //当目标值为最小值时，对应的obj的相对旋转角度
    public float m_localEulerAnglesMaxValue;  

    [Header("目标值的参数")]
    public float m_targetMinValue;
    public float m_targetMaxValue;

    [Header("旋转轴向")]
    public NormalDir m_normalDir;
    public RotateMode m_rotateMode;
    public float m_timer = 0.5f;
    public Tweener m_tweener;

    private float currTargetValue; //目标值
    public float CurrTargetValue
    {
        get
        {
            return currTargetValue;
        }

        set
        {
            currTargetValue = SetCurrTargetValue(value);
        }
    }

    [SerializeField]
    protected float m_ratio; //比例值

    private void Start()
    {
        if (m_model==null)
        {
            m_model = this.gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CurrTargetValue = Random.Range(m_targetMinValue, m_targetMaxValue);
        }
    }

    public virtual float SetCurrTargetValue(float value)
    {
        if (value <= m_targetMinValue)
        {
            value = m_targetMinValue;
            m_ratio = 0;
        }
        else if (value >= m_targetMaxValue)
        {
            value = m_targetMaxValue;
            m_ratio = 1;
        }
        else
        {
            m_ratio = (value - m_targetMinValue) / (m_targetMaxValue - m_targetMinValue);
        }


        if (m_ratio > 1)
        {
            Debug.LogError("当前值超出最大范围");
        }

        if (m_localEulerAnglesMinValue < 0 || m_localEulerAnglesMaxValue < 0)
        {
            m_localEulerAnglesMinValue += 360;
            m_localEulerAnglesMaxValue += 360;
        }

        float localEulerAngles = (m_localEulerAnglesMaxValue - m_localEulerAnglesMinValue) * m_ratio + m_localEulerAnglesMinValue;

        Vector3 target = Vector3.zero;
        switch (m_normalDir)
        {
            case NormalDir.X:
                target = new Vector3(localEulerAngles, m_model.transform.localEulerAngles.y, m_model.transform.localEulerAngles.z);
                break;
            case NormalDir.Y:
                target = new Vector3(m_model.transform.localEulerAngles.x, localEulerAngles, m_model.transform.localEulerAngles.z);

                break;
            case NormalDir.Z:
                target = new Vector3(m_model.transform.localEulerAngles.x, m_model.transform.localEulerAngles.y, localEulerAngles);
                break;
            default:
                break;
        }
        if (m_tweener != null)
        {
            m_tweener.Kill();
        }
        m_tweener = m_model.transform.DOLocalRotate(target, m_timer, m_rotateMode);
        return value;
    }
}

public enum NormalDir
{
    X,
    Y,
    Z,
}
