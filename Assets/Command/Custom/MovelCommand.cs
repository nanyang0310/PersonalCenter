/*
/// 功能： 
///     移动命令
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace NY
{
    public class MovelCommand : ICommand
    {
        public enum MoveType
        {
            Target,//目标值  目标值的方式
            Translate,//平移 累加的方式
        }


        private Transform m_obj;
        private Vector3 m_target;
        private Vector3 m_defaultPos;
        private bool m_isDamping;
        private float m_dampingtimer;
        private MoveType m_moveType;

        /// <summary>
        /// 利用构造函数，实现参数的引用
        /// </summary>
        /// <param name="obj">移动的对象</param>
        /// <param name="target">目标值</param>
        /// <param name="isDamping">是否渐变，阻尼</param>
        /// <param name="Dampingtimer">渐变时间</param>
        public MovelCommand(Transform obj, Vector3 target, MoveType moveType = MoveType.Target, bool isDamping = false, float dampingtimer = 0.5f)
        {
            m_obj = obj;
            m_target = target;
            m_isDamping = isDamping;
            m_dampingtimer = dampingtimer;
            m_moveType = moveType;
            m_defaultPos = m_obj.localPosition;
        }

        public void Execute()
        {
            CommandManager.Instance.m_executeStartAction?.Invoke();

            m_obj.GetComponent<Collider>().enabled = false;

            switch (m_moveType)
            {
                case MoveType.Target:
                    if (m_isDamping)
                    {
                        m_obj.DOLocalMove(m_target, m_dampingtimer).SetEase(Ease.Linear).OnComplete(delegate 
                        {
                            m_obj.GetComponent<Collider>().enabled = true;
                            CommandManager.Instance.m_executeEndAction?.Invoke();
                        });
                    }
                    else
                    {
                        m_obj.localPosition = m_target;
                        m_obj.GetComponent<Collider>().enabled = true;
                        CommandManager.Instance.m_executeEndAction?.Invoke();
                    }
                    break;
                case MoveType.Translate:
                    if (m_isDamping)
                    {
                        Vector3 pos = m_obj.localPosition + m_target;
                        m_obj.DOLocalMove(pos, m_dampingtimer).SetEase(Ease.Linear).OnComplete(delegate 
                        {
                            m_obj.GetComponent<Collider>().enabled = true;
                            CommandManager.Instance.m_executeEndAction?.Invoke();
                        });
                    }
                    else
                    {
                        m_obj.localPosition += m_target;
                        m_obj.GetComponent<Collider>().enabled = true;
                        CommandManager.Instance.m_executeEndAction?.Invoke();
                    }
                    break;
                default:
                    break;
            }
        }

        public void UnDo()
        {
            switch (m_moveType)
            {
                case MoveType.Target:
                    if (m_isDamping)
                    {
                        m_obj.DOLocalMove(m_defaultPos, m_dampingtimer).SetEase(Ease.Linear);
                    }
                    else
                    {
                        m_obj.localPosition = m_defaultPos;
                    }
                    break;
                case MoveType.Translate:
                    if (m_isDamping)
                    {
                        Vector3 pos = m_obj.localPosition - m_target;
                        m_obj.DOLocalMove(pos, m_dampingtimer).SetEase(Ease.Linear);
                    }
                    else
                    {
                        m_obj.localPosition -= m_target;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
