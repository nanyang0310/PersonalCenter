/*
/// 功能：
///     命令模式中载体基类，起一个组件的作用，完成与unity资源的交互
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCommandCarrier : MonoBehaviour
{
    //命令的名称
    public string[] m_commandNames;
    protected ICommand[] m_commands;
    protected CommandInvoker m_commandInvoker;

    protected virtual void Awake()
    {
        m_commandInvoker = new CommandInvoker();
        m_commands = new ICommand[m_commandNames.Length];
    }

    /// <summary>
    /// 添加命令
    /// </summary>
    /// <param name="index"></param>
    /// <param name="command"></param>
    protected virtual void AddCommand(int index, ICommand command)
    {
        m_commands[index] = command;
    }

    /// <summary>
    /// 发出命令
    /// </summary>
    /// <param name="index"></param>
    protected virtual void SetCommand(int index)
    {
        m_commandInvoker.SetCommond(m_commands[index]);
    }

    /// <summary>
    /// 发出命令
    /// </summary>
    /// <param name="commandName"></param>
    protected virtual void SetCommand(string commandName)
    {
        SetCommand(GetCommandIndex(commandName));
    }

    protected int GetCommandIndex(string commandName)
    {
        for (int i = 0; i < m_commandNames.Length; i++)
        {
            if (m_commandNames[i] == commandName)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    protected virtual void DoCommand()
    {
        m_commandInvoker.ActionExecute();
    }

    /// <summary>
    /// 撤销命令
    /// </summary>
    protected virtual void UnDoCommand()
    {
        m_commandInvoker.ActionUnDo();
    }
}
