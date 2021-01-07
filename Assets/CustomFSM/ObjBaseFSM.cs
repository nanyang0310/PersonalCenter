/*
/// 功能： 
/// 时间：
/// 版本：
*/

using UnityEngine;
using System.Collections.Generic;

public class ObjBaseFSM : MonoBehaviour
{
    public string[] m_stateName;
    public string m_currStateName;
    public List<Handle> m_allHandle = new List<Handle>();

    protected void IntoState(int index)
    {
        if (index < 0)
        {
            return;
        }
        m_currStateName = m_stateName[index];
        m_allHandle[index].Execute();
    }

    public virtual void IntoState(string stateName)
    {
        IntoState(GetStateIndex(stateName));
    }

    private int GetStateIndex(string stateName)
    {
        for (int i = 0; i < m_stateName.Length; i++)
        {
            if (m_stateName[i] == stateName)
            {
                return i;
            }
        }
        return -1;
    }
}

public abstract class Handle
{
    protected ObjBaseFSM m_objBaseFSM;

    public void SetFSM(ObjBaseFSM objBaseFSM)
    {
        this.m_objBaseFSM = objBaseFSM;
    }

    public abstract void Execute();
}

