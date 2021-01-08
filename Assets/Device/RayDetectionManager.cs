/*
/// 功能：  射线检测的管理器脚本
///     用于处理射线检测到指定的layer,根据鼠标的不同输入状态，分发不同事件
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayDetectionManager : Singleton<RayDetectionManager>
{
    protected RayDetectionManager() { }

    //鼠标悬浮时的状态
    public enum HoverState
    {
        NONE,//悬浮时，处于未指定的layer上
        HOVER, //悬浮时,处于指定的layer上
    }

    /// <summary>
    /// 射线检测的layer
    /// </summary>
    public LayerMask m_layerMask = 1;

    /// <summary>
    /// 射线的最大距离
    /// </summary>
    public float m_maxDistance = 100;

    /// <summary>
    /// Camera to ray.
    /// </summary>
    public Camera m_rayCamera;

    public HoverState m_currHoverState;
    private GameObject m_currHoveredGO;
    private Vector3 m_onMouseDownPos;
    private bool m_isDrag = false;
    private bool m_isDetectionSucceedStart = false;
    private bool m_isDetectionSucceedEnd = false;

    [SerializeField] [Header("拖拽基础值")]
    private float m_dragNormalDis = 1.5f;

    private void Update()
    {
        // 如果光标在UI上，则直接返回
        if (IsPointerOnUI("UI"))
        {
            //m_IsPointerOnUIAction?.Invoke();
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        if (!isPosInViewport(m_rayCamera, Input.mousePosition))
        {
            //m_NoPosInViewportAction?.Invoke();
            return;
        }

        //发射射线
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = m_rayCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out hitInfo, m_maxDistance, m_layerMask))
        {
            //悬浮
            if (m_currHoveredGO != hitInfo.collider.gameObject)
            {
                OnPointerExit(m_currHoveredGO);
                m_currHoverState = HoverState.NONE;
            }

            if (m_currHoverState == HoverState.NONE)
            {
                m_currHoveredGO = hitInfo.collider.gameObject;
                OnPointerEnter(m_currHoveredGO);
            }
            m_currHoverState = HoverState.HOVER;
            Debug.Log("悬浮");
        }
        else
        {
            //划出
            if (m_currHoverState == HoverState.HOVER)
            {
                OnPointerExit(m_currHoveredGO);
            }
            m_currHoverState = HoverState.NONE;
            m_currHoveredGO = null;
        }


        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            m_onMouseDownPos = Input.mousePosition;
            if (m_currHoverState == HoverState.HOVER)
            {
                m_isDetectionSucceedStart = true;
                Debug.Log("鼠标按下,在检测的对象上,开始检测成功");
            }
            else if (m_currHoverState == HoverState.NONE)
            {
                m_isDetectionSucceedStart = false;
                Debug.Log("没有检查到触发layer,开始检测失败,点空气");
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (m_isDetectionSucceedStart)
                {
                    Debug.Log("左键，有对象");
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (m_isDetectionSucceedStart)
                {
                    Debug.Log("右键，有对象");
                }
            }
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if (m_currHoverState == HoverState.HOVER)
            {
                Debug.Log("持续按下中，有对象");
            }
            else if (m_currHoverState == HoverState.NONE)
            {
                Debug.Log("持续按下中，没有对象");
            }

            if (Input.GetMouseButton(0))
            {
                if (m_isDetectionSucceedStart)
                {
                    Debug.Log("左键，按下点有对象");
                }
            }
            else if (Input.GetMouseButton(1))
            {
                if (m_isDetectionSucceedStart)
                {
                    Debug.Log("右键，按下点有对象");
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            if (m_currHoverState == HoverState.HOVER)
            {
                m_isDetectionSucceedEnd = true;
                Debug.Log("鼠标抬起，能检测到对象");
            }
            else if (m_currHoverState == HoverState.NONE)
            {
                m_isDetectionSucceedEnd = false;
                Debug.Log("鼠标抬起，未能检测到对象");
            }

            Vector3 newPos = Input.mousePosition;
            if (Vector3.Distance(newPos, m_onMouseDownPos) > m_dragNormalDis)
            {
                m_isDrag = true;

                if (m_isDetectionSucceedStart && m_isDetectionSucceedEnd)
                {
                    Debug.Log("按下点有对象，抬起点有对象，可执行拖拽相关操作");
                }
            }
            else
            {
                m_isDrag = false;

                if (m_isDetectionSucceedStart && m_isDetectionSucceedEnd)
                {
                    Debug.Log("按下点有对象，抬起点有对象，可执行鼠标抬起相关操作");
                }
            }

            m_isDetectionSucceedStart = false;
            m_isDetectionSucceedEnd=false;
        }
    }

    protected virtual void Update1()
    {
        // 如果光标在UI上，则直接返回
        if (IsPointerOnUI("UI"))
        {
            //m_IsPointerOnUIAction?.Invoke();
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        if (!isPosInViewport(m_rayCamera, Input.mousePosition))
        {
            //m_NoPosInViewportAction?.Invoke();
            return;
        }

        RaycastHit hitInfo = new RaycastHit();
        Ray ray = m_rayCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out hitInfo, m_maxDistance, m_layerMask))
        {
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                Debug.Log("未长按？？");
                // 先恢复上次选择物体
                if (m_currHoveredGO != hitInfo.collider.gameObject)
                {
                    //OnPointerExit(m_currHoveredGO);
                    m_currHoverState = HoverState.NONE;
                }

                if (m_currHoverState == HoverState.NONE)
                {
                    //OnPointerEnter(hitInfo);
                    m_currHoveredGO = hitInfo.collider.gameObject;
                }

                m_currHoverState = HoverState.HOVER;
            }
            else
            {
                Debug.Log("长按？？");
            }
        }
        else
        {
            if (m_currHoverState == HoverState.HOVER)
            {
                //OnPointerExit(m_currHoveredGO);
            }
            m_currHoverState = HoverState.NONE;
            m_currHoveredGO = null;

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Debug.Log("点空气？？");
            }
        }

        if (m_currHoverState == HoverState.HOVER)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("选中，按下左键");
                //m_mouseDownPos = Input.mousePosition;

                //m_clickCount++;
                //if (!m_isDetectingClick)
                //{
                //    StartCoroutine(DetectClick(hitInfo, 0));
                //}
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("选中，抬起左键");
                //OnPointerUp(hitInfo, 0);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("选中，按下右键");
                //m_mouseDownPos = Input.mousePosition;
                //OnPointerDown(hitInfo, 1);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("选中，抬起右键");
                //OnPointerUp(hitInfo, 1);
            }
            else
            {
                Debug.Log("选中，拖拽？？？");
                //OnPointerHover(hitInfo);
            }
        }
    }

    /// <summary>
    /// 检测是否处于LayerName层
    /// </summary>
    /// <param name="LayerName"></param>
    /// <returns></returns>
    protected virtual bool IsPointerOnUI(string LayerName)
    {
        bool isPointerOnUI = false;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            for (int i = 0; i < results.Count; ++i)
            {
                if (results[i].gameObject.layer == LayerMask.NameToLayer(LayerName))
                {
                    isPointerOnUI = true;
                    break;
                }
            }
        }

        return isPointerOnUI;
    }

    /// <summary>
    /// 检测鼠标是否在指定的摄像机视口
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="screenPosition"></param>
    /// <returns></returns>
    protected bool isPosInViewport(Camera camera, Vector3 screenPosition)
    {
        bool isIn = camera.pixelRect.Contains(screenPosition);
        return isIn;
    }

    public virtual void OnPointerExit(GameObject exitGameObject)
    {
        if (!exitGameObject)
        {
            return;
        }

#if UNITY_EDITOR
        Debug.LogFormat("Pointer exit: {0}", exitGameObject.name);
#endif

        //if (m_OnPointerExitAction != null)
        //{
        //    m_OnPointerExitAction();
        //}
    }

    public virtual void OnPointerEnter(GameObject go)
    {
#if UNITY_EDITOR
        Debug.LogFormat("Pointer enter: {0}", go.name);
#endif

        //if (m_OnPointerEnterAction != null)
        //{
        //    m_OnPointerEnterAction(hitInfo);
        //}
    }
}
