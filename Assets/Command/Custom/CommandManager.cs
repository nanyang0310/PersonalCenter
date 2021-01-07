/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System;

public class CommandManager : CSingleton<CommandManager>
{
    /// <summary>
    /// 执行命令 开始的监听
    /// </summary>
    public Action m_executeStartAction;

    /// <summary>
    /// 执行命令 结束的监听
    /// </summary>
    public Action m_executeEndAction;
}
