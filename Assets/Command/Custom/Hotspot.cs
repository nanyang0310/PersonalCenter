using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace NY
{
    [DisallowMultipleComponent]
    public class Hotspot : MonoBehaviour
    {
        public enum DeviceType
        {
            Default,   //默认的 不参与流程  
            Device,    //设备 参与流程
        }

        public enum AnimationType
        {
            None,
            Animation,
            Dotween,
            Animator,
        }

        public enum MoveMentType
        {
            None,
            Translate,
            Rotation,
        }

        public enum HighlightType
        {
            PointEnter,
            PointClick,
        }

        public int m_index = -1;
        public AnimationType m_animationType;
        public DeviceType m_type;
        public MoveMentType m_movementType;
        public HighlightType m_highlightType;
        public bool m_isFreeHighlight = false;

        public string m_hotspotName;
        public string m_partName;
        public GameObject m_model;
        [Header("------------------------------------------------------------------------------------------------------------------")]
        public Animation m_partAnimation;
        public Animator m_partAnimator;
        public string m_partAniName;

        [SerializeField]
        protected int m_stateValue;
        public int StateValue
        {
            get { return m_stateValue; }
            set { m_stateValue = SetStateValue(value); }
        }
        [Header("【Dotween的动画数据】")]
        public List<Vector3> m_stateValueList;
        [Header("【Animator/Animation的动画数据】")]
        public List<string> m_stateDescribeList;
        [Header("【该热点是否参与后台的相关计算】")]
        public bool m_isNeedRun = false;
        public float m_tweenerTime = 0.5f;
        public bool m_isUpdateCollider = false;
        [Header("------------------------------------------------------------------------------------------------------------------")]
        public Transform m_camTargetTrans;
        public Transform m_camInitTrans;
        public float m_cameraDistance = 0.5f;
        public bool m_overrideCamOffsetAngle = false;
        public float m_cameraOffsetAngle = 0;

        public Transform m_camOpTargetParent;
        public Transform m_miniCamOpTarget;

        public Action<Hotspot> m_OperateStartAction;
        public Action<Hotspot> m_OperateEndtAction;

        protected virtual void Awake()
        {
            //m_type = DeviceType.Hotspot;
            if (!m_model)
            {
                m_model = this.gameObject;
            }
            if (!m_camTargetTrans)
            {
                m_camTargetTrans = transform;
            }

            InitState(m_stateValue);
        }

        public virtual string GetPartName()
        {
            string partName = m_partName.Replace("（", "(");
            int index = partName.IndexOf("(");
            if (index == -1)
            {
                return m_partName;
            }
            string name = m_partName.Substring(0, index);
            return name;
        }

        public virtual void Operate()
        {
            m_OperateStartAction?.Invoke(this);

            switch (m_type)
            {
                case DeviceType.Default:
                case DeviceType.Device:
                    switch (m_animationType)
                    {
                        case AnimationType.None:
                            break;
                        case AnimationType.Animation:
                            CustomPlayAnimationState();
                            break;
                        case AnimationType.Dotween:
                            m_stateValue++;
                            HotspotMovement(m_movementType);
                            break;
                        case AnimationType.Animator:
                            CustomInitAnimatorState();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 播放指定的动画
        /// </summary>
        /// <param name="aniName">动画名称</param>
        protected virtual void PlayAnimationState(string aniName)
        {
            if (!m_partAnimation || aniName.Length <= 0)
            {
                return;
            }

            AnimationState currState = m_partAnimation[aniName];
            if (!currState)
            {
                return;
            }

            this.GetComponent<Collider>().enabled = false;
            OperationManager.Instance.enabled = false;
            currState.speed = 1;
            m_partAnimation.Play(aniName);
            DOVirtual.DelayedCall(currState.length, () => OnTweenerPlayEndCallBack(true));
        }

        /// <summary>
        /// 根据状态描述列表中的数据，来回切换，只针对于2个动画状态的设备有用，如果多个，则需要重写
        /// </summary>
        protected virtual void CustomPlayAnimationState()
        {
            if (m_stateDescribeList.Count == 2)
            {
                m_stateValue++;
                if (m_stateValue >= m_stateDescribeList.Count)
                {
                    m_stateValue = 0;
                }
                PlayAnimationState(m_stateDescribeList[m_stateValue]);
            }
            else
            {
                PlayAnimationState(m_partAniName);
            }
        }

        /// <summary>
        /// 利用Dotween进行简单的自定义动画（旋转、平移）
        /// </summary>
        /// <param name="movementType"></param>
        /// <param name="isCustomStateValue">初始状态值</param>
        /// <param name="customStateValue"></param>
        public virtual void HotspotMovement(MoveMentType movementType, bool isCustomStateValue = false, int customStateValue = 0)
        {
            if (isCustomStateValue)
            {
                if (customStateValue >= m_stateValueList.Count || customStateValue < 0)
                {
                    Debug.LogError("自定义状态值错误");
                    OperationManager.Instance.enabled = true;
                    return;
                }
                m_stateValue = customStateValue;
            }
            else
            {
                if (m_stateValue >= m_stateValueList.Count)
                {
                    m_stateValue = 0;
                }
            }

            OperationManager.Instance.enabled = false;
            switch (movementType)
            {
                case MoveMentType.Translate:
                    m_model.transform.DOLocalMove(m_stateValueList[m_stateValue], m_tweenerTime).OnComplete(() => OnTweenerPlayEndCallBack(true));
                    if (m_isUpdateCollider)
                    {
                        this.transform.DOLocalMove(m_stateValueList[m_stateValue], m_tweenerTime);
                    }
                    break;
                case MoveMentType.Rotation:
                    m_model.transform.DOLocalRotate(m_stateValueList[m_stateValue], m_tweenerTime).OnComplete(() => OnTweenerPlayEndCallBack(true));
                    if (m_isUpdateCollider)
                    {
                        this.transform.DOLocalRotate(m_stateValueList[m_stateValue], m_tweenerTime);
                    }
                    break;
                default:
                    break;
            }
        }

        protected virtual int GetStateValue(string stateDescribe)
        {
            for (int i = 0; i < m_stateDescribeList.Count; i++)
            {
                if (m_stateDescribeList[i] == stateDescribe)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 动画结束时的回调
        /// </summary>
        /// <param name="isEnable"></param>
        protected virtual void OnTweenerPlayEndCallBack(bool isEnable)
        {
            this.GetComponent<Collider>().enabled = isEnable;
            OperationManager.Instance.enabled = true;

            if (m_OperateEndtAction != null)
            {
                m_OperateEndtAction(this);

                m_OperateEndtAction = null;
                m_OperateStartAction = null;
            }
        }

        public virtual bool InitAnimationState(string aniName)
        {
            if (!m_partAnimation || aniName.Length <= 0)
            {
                return false;
            }

            AnimationState currState = m_partAnimation[aniName];
            if (!currState)
            {
                return false;
            }

            currState.speed = 0;
            currState.normalizedSpeed = 0;
            m_partAnimation.Play(aniName);
            return true;
        }

        /// <summary>
        /// 根据状态描述列表中的数据，来回切换，只针对于2个动画状态的设备有用，如果多个，则需要重写
        /// </summary>
        protected virtual void CustomInitAnimatorState()
        {
            if (m_stateDescribeList.Count == 2)
            {
                m_stateValue++;
                if (m_stateValue >= m_stateDescribeList.Count)
                {
                    m_stateValue = 0;
                }
                InitAnimatorState(m_stateDescribeList[m_stateValue]);
            }
            else
            {
                InitAnimatorState(m_partAniName);
            }
        }

        protected virtual  void InitAnimatorState(string para)
        {
            //AnimatorClipInfo[] animatorClipInfos = m_partAnimator.GetCurrentAnimatorClipInfo(0);
            OperationManager.Instance.enabled = false;
            m_partAnimator.Play(para);
            DOVirtual.DelayedCall(GetClipLength(m_partAnimator), () => OnTweenerPlayEndCallBack(true));
        }

        public virtual bool InitAnitorState(string aniName)
        {
            if (!m_partAnimation || aniName.Length <= 0)
            {
                return false;
            }

            m_partAnimator.Play(aniName);
            m_partAnimator.Update(0);
            return true;
        }

        protected float GetClipLength(Animator m_animator)
        {
            if (m_animator == null)
            {
                return 0;
            }
            AnimationClip clip = m_animator.runtimeAnimatorController.animationClips[0];
            float speed = m_animator.GetCurrentAnimatorStateInfo(0).speed;
            //通过测试得知，速度改变是成平方的关系，比如speed为2，则比原速度快4倍
            return clip.length / (speed * speed);
        }

        protected virtual int SetStateValue(int value)
        {
            InitState(value);
            return value;
        }

        protected virtual void InitState(int value)
        {
            switch (m_type)
            {
                case DeviceType.Device:
                    switch (m_animationType)
                    {
                        case AnimationType.Dotween:
                            switch (m_movementType)
                            {
                                case MoveMentType.None:
                                    break;
                                case MoveMentType.Translate:
                                    m_model.transform.localPosition = m_stateValueList[value];
                                    break;
                                case MoveMentType.Rotation:
                                    m_model.transform.localEulerAngles = m_stateValueList[value];
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public class HotspotCompare : IComparer<Hotspot>
    {
        public int Compare(Hotspot x, Hotspot y)
        {
            if (x.m_index > y.m_index)
            {
                return 1;
            }
            else if (x.m_index < y.m_index)
            {
                return -1;
            }
            else//如果数字相同，则比较枚举值，枚举类型都对应一个数值默认从0开始。
            {
                return 0;//排面花色都相等
            }
        }
    }
}
