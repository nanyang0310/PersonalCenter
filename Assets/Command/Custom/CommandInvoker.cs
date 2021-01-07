/*
/// 功能： 
///     
/// 时间：
/// 版本：
*/


using System.Collections.Generic;
using System;

public class CommandInvoker 
{
    private ICommand icommand;
    public Stack<Action> undoFunctions = new Stack<Action>();

    public void SetCommond(ICommand icommand)
    {
        this.icommand = icommand;
    }

    /// <summary>
    /// 执行
    /// </summary>
    public void ActionExecute()
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
    public void ActionUnDo()
    {
        if (undoFunctions.Count > 0)
        {
            undoFunctions.Pop().Invoke();
        }
    }
}
