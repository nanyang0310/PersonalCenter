/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RemoteControl 
{
    private ICommand icommand;

    public Stack<UnityAction> undoFunctions = new Stack<UnityAction>();

    public void SetCommond(ICommand icommand)
    {
        this.icommand = icommand;
    }

    /// <summary>
    /// 执行
    /// </summary>
    public void OnCtrlBtnClicked()
    {
        if (icommand != null)
        {
            icommand.Execute();
            undoFunctions.Push(icommand.UnDo);
        }
    }

    /// <summary>
    /// 撤销
    /// </summary>
    public void OnUnDoBtnClicked()
    {
        if (undoFunctions.Count > 0)
        {
            undoFunctions.Pop().Invoke();
        }
    }
}

