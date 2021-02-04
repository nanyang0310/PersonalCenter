/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMesh : MonoBehaviour
{
    public Text m_hoverName;

    private void OnEnable()
    {
        RayDetectionManager.Instance.m_OnPointerEnterAction += OnPointerEnterCallBack;
        RayDetectionManager.Instance.m_OnPointerDragingAction += OnPointerDragingCallBack;
    }

    private void OnDisable()
    {
        RayDetectionManager.Instance.m_OnPointerEnterAction -= OnPointerEnterCallBack;
        RayDetectionManager.Instance.m_OnPointerDragingAction -= OnPointerDragingCallBack;
    }

    private void OnPointerEnterCallBack(GameObject go)
    {
        m_hoverName.text = go.name;
    }

    private void OnPointerDragingCallBack(GameObject go,Vector3 pos,int button)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(pos);
        go.transform.position = newPos;
    }
}
