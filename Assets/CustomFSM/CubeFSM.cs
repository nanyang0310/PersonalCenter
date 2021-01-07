/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFSM : ObjBaseFSM
{
    //声明状态（命令？）
    MoveLeft moveLeft;

    private void Start()
    {
        Init();
        IntoState(m_stateName[0]);
    }

    public void Init()
    {
        moveLeft = new MoveLeft(this.gameObject, this);
        m_allHandle.Add(moveLeft);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IntoState(m_stateName[0]);
        }
    }
}
