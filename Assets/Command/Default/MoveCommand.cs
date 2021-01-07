using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector3 m_Offset;
    private Transform m_Object;

    public MoveCommand(Transform obj, Vector3 offset)
    {
        this.m_Object = obj;
        this.m_Offset = offset;
    }

    public void Execute()
    {
        m_Object.transform.position += m_Offset;
    }

    public void UnDo()
    {
        m_Object.transform.position -= m_Offset;
    }
}
