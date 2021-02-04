using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace NY
{
    public class OperationManager : Singleton<OperationManager>
    {
        protected OperationManager() { }

        /// <summary>
        /// Layer of ray.
        /// </summary>
        public LayerMask m_layerMask = 1;

        /// <summary>
        /// Max distance of ray.
        /// </summary>
        public float m_maxDistance = 100;

        /// <summary>
        /// Camera to ray.
        /// </summary>
        public Camera m_rayCamera;

    public GameObject m_hoveredGO;
    public enum HoverState
    {
        NONE,
        HOVER,
    }
    public HoverState m_hoverState = HoverState.NONE;
    
    // 鼠标单击、双击判断
    protected float m_doubleClickInterval = 0.3f;
    protected int m_clickCount = 0;
    protected bool m_isDetectingClick = false;

    // 通过鼠标按下和弹起的位置判断鼠标是否处于拖动状态
    public Vector3 m_mouseDownPos;

        // 部件名称提示UI
        //public Text m_nameText;

        public Action<RaycastHit> m_OnPointerEnterAction;
        public Action<RaycastHit> m_OnPointerHoverAction;
        public Action m_OnPointerExitAction;
        public Action<RaycastHit, int> m_OnPointerDownAction;
        public Action<RaycastHit, int> m_OnPointerUpAction;
        public Action<RaycastHit, int> m_OnPointerDoubleClickAction;
        public Action<float> m_OnMouseScrollWheelAction; //鼠标滚轮滚动的监听
        public Action<RaycastHit, int> m_OnPointerDragAction;
        public Action<RaycastHit, int> m_OnPointerEndDragAction;
        public Action m_IsPointerOnUIAction;
        public Action m_NoPosInViewportAction;

        public float m_offsetX = 5f;
        public float m_offsetY = -5f;
        public Color m_outLineColor1;
        public Color m_outLineColor2;

        public GameObject m_partNameTip;
        protected virtual void Start()
        {
            if (!m_rayCamera)
            {
                m_rayCamera = Camera.main;
            }
        }

        protected virtual void Update()
        {
            // 注意：会检测到带有Collider的3D GameObject
            //// 如果光标在UI上，则直接返回
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (IsPointerOnUI("UI"))
            {
                m_IsPointerOnUIAction?.Invoke();
                return;
            }

            Vector3 mousePos = Input.mousePosition;
            if (!isPosInViewport(m_rayCamera, Input.mousePosition))
            {
                m_NoPosInViewportAction?.Invoke();
                return;
            }

            RaycastHit hitInfo = new RaycastHit();
            Ray ray = m_rayCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hitInfo, m_maxDistance, m_layerMask))
            {
                if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                {
                    // 先恢复上次选择物体
                    if (m_hoveredGO != hitInfo.collider.gameObject)
                    {
                        OnPointerExit(m_hoveredGO);
                        m_hoverState = HoverState.NONE;
                    }

                    if (m_hoverState == HoverState.NONE)
                    {
                        OnPointerEnter(hitInfo);
                        m_hoveredGO = hitInfo.collider.gameObject;
                    }

                    m_hoverState = HoverState.HOVER;
                }
                else
                {
                    //m_hoverState = HoverState.Drag;
                    //OrderMenuManager.Instance.InitAllOrderMenuViewHide();
                }
            }
            else
            {
                if (m_hoverState == HoverState.HOVER)
                {
                    OnPointerExit(m_hoveredGO);
                }
                m_hoverState = HoverState.NONE;
                m_hoveredGO = null;

                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    m_mouseDownPos = Input.mousePosition;
                }
            }

            if (m_hoverState == HoverState.HOVER)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_mouseDownPos = Input.mousePosition;
                    if (IsPointerOnUI("UI"))
                    {
                        return;
                    }

                    m_clickCount++;
                    if (!m_isDetectingClick)
                    {
                        StartCoroutine(DetectClick(hitInfo, 0));
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        return;
                    }
                    OnPointerUp(hitInfo, 0);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    m_mouseDownPos = Input.mousePosition;
                    OnPointerDown(hitInfo, 1);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                    {
                        return;
                    }
                    OnPointerUp(hitInfo, 1);
                }
                else
                {
                    OnPointerHover(hitInfo);
                }
            }

            //if (m_hoverState == HoverState.Drag)
            //{
            //    if (Input.GetMouseButton(0))
            //    {
            //        OnPointerDrag(hitInfo, 0);
            //    }
            //    else if (Input.GetMouseButton(1))
            //    {
            //        OnPointerDrag(hitInfo, 1);
            //    }
            //}
        }

        public virtual bool IsPointerOnUI(string UILayerName)
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
                    if (results[i].gameObject.layer == LayerMask.NameToLayer(UILayerName))
                    {
                        isPointerOnUI = true;
                        break;
                    }
                }
            }

            return isPointerOnUI;
        }

        protected IEnumerator DetectClick(RaycastHit hitInfo, int button)
        {
            m_isDetectingClick = true;
            yield return new WaitForSeconds(m_doubleClickInterval);

            if (m_clickCount >= 2)
            {
                OnPointerDoubleClick(hitInfo, button);
            }
            else if (m_clickCount == 1)
            {
                OnPointerDown(hitInfo, button);
            }

            m_clickCount = 0;
            m_isDetectingClick = false;
        }

        /// <summary>
        /// Detect if Mouse is in specified camera-viewport
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public bool isPosInViewport(Camera camera, Vector3 screenPosition)
        {
            bool isIn = camera.pixelRect.Contains(screenPosition);
            return isIn;
        }

        public virtual void OnPointerEnter(RaycastHit hitInfo)
        {
#if UNITY_EDITOR
            Debug.LogFormat("Pointer enter: {0}", hitInfo.collider.gameObject.name);
#endif

            if (m_OnPointerEnterAction != null)
            {
                m_OnPointerEnterAction(hitInfo);
            }

        GameObject highlightGO = hitInfo.collider.transform.gameObject;
        Hotspot hotspot = null;
        if (highlightGO.GetComponent<Hotspot>())
        {
            hotspot = highlightGO.GetComponent<Hotspot>();
        }

        if (!hotspot)
        {
            if (highlightGO.transform.parent != null)
            {
                hotspot = highlightGO.transform.parent.GetComponent<Hotspot>();
            }
        }

        if (!hotspot) return;

        //HighlightManager.Instance.FlashOutlineOn(hotspot.m_model, m_outLineColor1, m_outLineColor2);
        // 显示名称标签
    }

    public virtual void OnPointerHover(RaycastHit hitInfo)
    {
        //#if UNITY_EDITOR
        //        Debug.LogFormat("Pointer Hover: {0}", hitInfo.collider.gameObject.name);
        //#endif
        if (m_OnPointerHoverAction != null)
        {
            m_OnPointerHoverAction(hitInfo);
        }
        GameObject highlightGO = hitInfo.collider.transform.gameObject;
        Hotspot hotspot = null;
        if (highlightGO.GetComponent<Hotspot>())
        {
            hotspot = highlightGO.GetComponent<Hotspot>();
        }

        if (!hotspot)
        {
            if (highlightGO.transform.parent!=null)
            {
                hotspot = highlightGO.transform.parent.GetComponent<Hotspot>();
            }
        }

        if (!hotspot) return;

        //HighlightManager.Instance.FlashOutlineOn(hotspot.m_model, m_outLineColor1, m_outLineColor2);
        // 显示名称标签
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

            if (m_OnPointerExitAction != null)
            {
                m_OnPointerExitAction();
            }

            //HighlightManager.Instance.ClearHighlight();

        GameObject highlightGO = exitGameObject;
        Hotspot hotspot = null;
        if (highlightGO.GetComponent<Hotspot>())
        {
            hotspot = highlightGO.GetComponent<Hotspot>();
        }

        if (!hotspot)
        {
            if (highlightGO.transform.parent != null)
            {
                hotspot = highlightGO.transform.parent.GetComponent<Hotspot>();
            }
        }

        if (!hotspot) return;

        if (hotspot.m_highlightType == Hotspot.HighlightType.PointClick)
        {
            if (hotspot.m_isFreeHighlight)
            {
                //HighlightManager.Instance.FlashOutlineOff(hotspot.m_model);
            }
        }
        else
        {
            //HighlightManager.Instance.FlashOutlineOff(hotspot.m_model);
        }
    }

        public virtual void OnPointerDown(RaycastHit hitInfo, int button)
        {
#if UNITY_EDITOR
            Debug.LogFormat("Pointer [{0}] down on: {1}", button, hitInfo.collider.gameObject.name);
#endif
            if (m_OnPointerDownAction != null)
            {
                m_OnPointerDownAction(hitInfo, button);
            }

            GameObject highlightGO = hitInfo.collider.transform.gameObject;
            Hotspot hotspot = null;
            if (highlightGO.GetComponent<Hotspot>())
            {
                hotspot = highlightGO.GetComponent<Hotspot>();
            }
            else if (highlightGO.transform.parent.GetComponent<Hotspot>())
            {
                hotspot = hitInfo.transform.parent.GetComponent<Hotspot>();
            }
            if (button == 0 && hotspot != null)
            {
                if (hotspot)
                {
                    //不参与流程的热点
                    if (hotspot.m_type == Hotspot.DeviceType.Device)
                    {
                        hotspot.Operate();
                    }
                    else
                    {
                        //TaskManager.Instance.OperateHotspot(hotspot);
                    }
                }
            }
        }

        public virtual void OnPointerUp(RaycastHit hitInfo, int button)
        {
#if UNITY_EDITOR
            Debug.LogFormat("Pointer [{0}] up on: {1}", button, hitInfo.collider.gameObject.name);
#endif

            if (m_OnPointerUpAction != null)
            {
                m_OnPointerUpAction(hitInfo, button);
            }

        GameObject highlightGO = hitInfo.collider.transform.gameObject;
        Hotspot hotspot = null;
        if (highlightGO.GetComponent<Hotspot>())
        {
            hotspot = highlightGO.GetComponent<Hotspot>();
        }
        if (!hotspot)
        {
            if (highlightGO.transform.parent!=null)
            {
                hotspot = hitInfo.transform.parent.GetComponent<Hotspot>();
            }
        }

        if (!hotspot) return;

        if (hotspot.m_highlightType == Hotspot.HighlightType.PointClick)
        {
            //HighlightManager.Instance.FlashOutlineOff(hotspot.m_model);
        }
        // 判断鼠标是否处于拖动状态
        Vector3 deltaVec = Input.mousePosition - m_mouseDownPos;
        if (deltaVec.sqrMagnitude >= 4)
        {
            if (m_OnPointerEndDragAction != null)
            {
                m_OnPointerEndDragAction(hitInfo, button);
            }
#if UNITY_EDITOR
                Debug.LogFormat("Pointer [{0}] up with dragged.", button);
#endif
                return;
            }
        }

        public virtual void OnPointerDrag(RaycastHit hitInfo, int button)
        {
            if (m_OnPointerDragAction != null)
            {
                m_OnPointerDragAction(hitInfo, button);
            }

#if UNITY_EDITOR
            Debug.LogFormat("Pointer [{0}] Drag with dragged.", button);
#endif
        }

        public virtual void OnPointerDoubleClick(RaycastHit hitInfo, int button)
        {
#if UNITY_EDITOR
            Debug.LogFormat("Pointer [{0}] double click on: {1}", button, hitInfo.collider.gameObject.name);
#endif

            if (m_OnPointerDoubleClickAction != null)
            {
                m_OnPointerDoubleClickAction(hitInfo, button);
            }
        }

        public void Clear()
        {
            m_hoveredGO = null;
            m_hoverState = HoverState.NONE;

            m_clickCount = 0;
            m_isDetectingClick = false;
        }
    }
}