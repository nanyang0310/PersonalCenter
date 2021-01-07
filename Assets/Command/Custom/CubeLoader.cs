/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NY
{
    public class CubeLoader : BaseCommandCarrier
    {
        public Vector3 m_moveLeftVer3;
        public Vector3 m_moveRightVer3;
        private MovelCommand m_moveLeftCommand;
        private MovelCommand m_moveRightCommand;

        private void Start()
        {
            m_moveLeftCommand = new MovelCommand(this.transform, m_moveLeftVer3, MovelCommand.MoveType.Translate);
            AddCommand(0, m_moveLeftCommand);

            m_moveRightCommand = new MovelCommand(this.transform, m_moveRightVer3, MovelCommand.MoveType.Target, true, 2.0f);
            AddCommand(1, m_moveRightCommand);

            //OrderMenuManager.Instance.OrderAction += SelectOrderCallBack;
        }

        private void OnMouseDown()
        {
            //OrderMenuManager.Instance.m_orderMenuInfoDatas = m_commandNames.ToList();
            //OrderMenuManager.Instance.OnPointerDownAction(Input.mousePosition, 0);
        }

        private void SelectOrderCallBack(string commandName,int commandIndex)
        {
            SetCommand(commandIndex);
            DoCommand();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UnDoCommand();
            }
        }
    }
}

