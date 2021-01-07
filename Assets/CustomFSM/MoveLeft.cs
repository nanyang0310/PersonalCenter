/*
/// 功能： 
/// 时间：
/// 版本：
*/

using UnityEngine;

public class MoveLeft : Handle
{
    public GameObject m_model;
    public MoveLeft(GameObject go,ObjBaseFSM objBaseFSM)
    {
        m_model = go;
        SetFSM(objBaseFSM);
    }

    public override void Execute()
    {
        Debug.Log("Move Left");
    }
}
