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
            Hotspot, //不参与步骤流程，可自由操作，无操作记录
            Device, //参与步骤流程，不能自由操作，有操作记录
            Group,//组
            NPC,
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

        public List<Vector3> m_stateValueList;
        public List<string> m_stateDescribeList;
        [Header("该热点是否参与后台的相关计算")]
        public bool m_isNeedRun = false;
        public float m_tweenerTime = 0.5f;
        public bool m_isUpdateCollider = false;

        public Transform m_camTargetTrans;
        public Transform m_camInitTrans;
        public float m_cameraDistance = 0.5f;
        public bool m_overrideCamOffsetAngle = false;
        public float m_cameraOffsetAngle = 0;

        public Transform m_camOpTargetParent;
        public Transform m_miniCamOpTarget;

        public Action m_customOperateStartAction;
        public Action m_customOperateEndtAction;

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
            switch (m_type)
            {
                case DeviceType.Hotspot:
                case DeviceType.Device:
                    switch (m_animationType)
                    {
                        case AnimationType.None:
                            break;
                        case AnimationType.Animation:
                            PlayAnimationState(m_partAniName);
                            break;
                        case AnimationType.Dotween:
                            m_stateValue++;
                            HotspotMovement(m_movementType);
                            break;
                        case AnimationType.Animator:
                            InitAnimatorState(m_partAniName);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public virtual void CustomOperate()
        {
            if (m_type == DeviceType.Device)
            {
                if (m_miniCamOpTarget != null)
                {
                    //CamerasManager.Instance.m_miniCamera.transform.position = m_miniCamOpTarget.position;
                    //CamerasManager.Instance.m_miniCamera.transform.rotation = m_miniCamOpTarget.rotation;
                    //CamerasManager.Instance.m_miniCamera.enabled = true;
                    //CamerasManager.Instance.SwitchToThirdPersonView(m_camTargetTrans, m_cameraDistance, true, m_overrideCamOffsetAngle, m_cameraOffsetAngle);
                }

                if (m_customOperateStartAction != null)
                {
                    m_customOperateStartAction();
                }

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
                        InitAnimatorState(m_partAniName);
                        break;
                    default:
                        break;
                }
            }
        }

        public void HotspotMovement(MoveMentType movementType, bool isCustomStateValue = false, int customStateValue = 0)
        {
            OperationManager.Instance.enabled = false;
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

            switch (movementType)
            {
                case MoveMentType.None:
                    break;
                case MoveMentType.Translate:
                    m_model.transform.DOLocalMove(m_stateValueList[m_stateValue], m_tweenerTime).OnStart(() => OnTweenerPlayCallBack(false)).OnComplete(() => OnTweenerPlayCallBack(true));

                    if (m_isUpdateCollider)
                    {
                        this.transform.DOLocalMove(m_stateValueList[m_stateValue], m_tweenerTime);
                    }
                    break;
                case MoveMentType.Rotation:
                    m_model.transform.DOLocalRotate(m_stateValueList[m_stateValue], m_tweenerTime).OnStart(() => OnTweenerPlayCallBack(false)).OnComplete(() => OnTweenerPlayCallBack(true));

                    if (m_isUpdateCollider)
                    {
                        this.transform.DOLocalRotate(m_stateValueList[m_stateValue], m_tweenerTime);
                    }
                    break;
                default:
                    break;
            }
        }

        public int GetStateValue(string stateDescribe)
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

        public void OnTweenerPlayCallBack(bool isEnable)
        {
            this.GetComponent<Collider>().enabled = isEnable;
            //HighlightManager.Instance.FlashOff(m_model);
            //if (isEnable && TaskManager.Instance != null)
            //{
            //    if (TaskManager.Instance.m_currentStep.m_hotspotName == m_hotspotName)
            //    {
            //        TaskManager.Instance.m_currentStep.m_isCompleted = true;
            //        //继续步骤
            //        TaskManager.Instance.GoToNextStep(TaskManager.Instance.m_currentStep);

            //        if (CamerasManager.Instance.m_lastCameraMode == CamerasManager.Instance.m_cameraController.GetMotor<com.ootii.Cameras.CameraMotor>("3rd Person Follow"))
            //        {
            //            CamerasManager.Instance.SwitchToThirdPersonFollow();
            //        }
            //    }
            //}

            OperationManager.Instance.enabled = true;
            if (m_customOperateEndtAction != null)
            {
                m_customOperateEndtAction();

                m_customOperateEndtAction = null;
                m_customOperateStartAction = null;
            }
        }

        public bool InitAnimationState(string aniName)
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

        public void PlayAnimationState(string aniName)
        {
            this.GetComponent<Collider>().enabled = false;
            if (!m_partAnimation || aniName.Length <= 0)
            {
                return;
            }

            AnimationState currState = m_partAnimation[aniName];
            if (!currState)
            {
                return;
            }
            OperationManager.Instance.enabled = false;
            currState.speed = 1;
            m_partAnimation.Play(aniName);
            DOVirtual.DelayedCall(currState.length, () => OnTweenerPlayCallBack(true));
        }

        public void CustomPlayAnimationState()
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

        public void InitAnimatorState(string para)
        {
            AnimatorClipInfo[] animatorClipInfos = m_partAnimator.GetCurrentAnimatorClipInfo(0);
            for (int i = 0; i < animatorClipInfos.Length; i++)
            {
                Debug.Log(animatorClipInfos[i].clip.name);
            }
            OperationManager.Instance.enabled = false;
            m_partAnimator.Play(para);
            DOVirtual.DelayedCall(GetClipLength(m_partAnimator), () => OnTweenerPlayCallBack(true));
        }

        public void CustomInitAnimatorState()
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


        public float GetClipLength(Animator m_animator)
        {
            if (m_animator == null)
            {
                return 0;
            }
            //string strAnimState = m_partAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
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
