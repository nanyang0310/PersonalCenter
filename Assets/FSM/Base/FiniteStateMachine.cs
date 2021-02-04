using System.Collections.Generic;


/// <summary>
/// ����״̬�������࣬��Ҫ�������Ǹ��ⲿ�ṩ���ýӿڲ��������״̬��
/// ����״̬�����ṩһ��ջ�����ڴ��ע���FSState��ͨ��Update����״̬���л�����ջ�Ĺ���Pop��Push������
/// </summary>
public class FiniteStateMachine
{
    public string Name { get; set; }

    // ״̬�л�ί��
    public delegate void EnterState(string stateName);
    public delegate void PushState(string stateName, string lastStateName);
    public delegate void PopState();

    // ע���״̬
    protected Dictionary<string, FSState> mStates;
    protected string mEntryPoint;

    // ״̬�л�ջ
    protected Stack<FSState> mStateStack;

    public FiniteStateMachine()
    {
        mStates = new Dictionary<string, FSState>();
        mStateStack = new Stack<FSState>();
        mEntryPoint = null;
    }

    /// <summary>
    /// ״̬ע��
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="stateObject"></param>
    public void Register(string stateName, IState stateObject)
    {
        if (!mStates.ContainsKey(stateName))
        {
            mStates.Add(stateName, new FSState(stateObject, this, stateName, Enter, Push, Pop));
        }
    }
    public void Unregister(string stateName)
    {
        mStates.Remove(stateName);
    }

    public void ClearAllStates()
    {
        mStates.Clear();
        mStateStack.Clear();
        mEntryPoint = null;
    }

    public void ClearAllOtherStates()
    {
        Dictionary<string, FSState> tmpDic = new Dictionary<string, FSState>(mStates);
        foreach (KeyValuePair<string, FSState> pair in tmpDic)
        {
            if (pair.Key != CurrentState.StateName)
            {
                mStates.Remove(pair.Key);
            }
        }
        tmpDic.Clear();
    }

    /// <summary>
    /// ״̬����
    /// </summary>
    public void Update()
    {
        if (mEntryPoint == null)
            return;

        if (CurrentState == null)
        {
            FSState fsState;
            if (mStates.TryGetValue(mEntryPoint, out fsState))
            {
                mStateStack.Push(mStates[mEntryPoint]);
                CurrentState.StateObject.OnEnter(null);
            }
        }

        if (CurrentState != null)
            CurrentState.StateObject.OnUpdate();
    }

    public FSState GetState(string stateName)
    {
        return mStates[stateName];
    }

    public void EntryPoint(string startName)
    {
        mEntryPoint = startName;
    }

    public FSState CurrentState
    {
        get
        {
            if (mStateStack.Count == 0)
                return null;
            return mStateStack.Peek();
        }
    }

    /// <summary>
    /// ״̬�л�
    /// </summary>
    /// <param name="stateName"></param>
    public void Enter(string stateName)
    {
        Push(stateName, Pop(stateName));
    }

    public void Push(string newState)
    {
        string lastName = null;
        if (mStateStack.Count > 1)
        {
            lastName = mStateStack.Peek().StateName;
        }
        Push(newState, lastName);
    }

    protected void Push(string stateName, string lastStateName)
    {
        mStateStack.Push(mStates[stateName]);
        mStateStack.Peek().StateObject.OnEnter(lastStateName);
    }

    public void Pop()
    {
        Pop(null);
    }

    protected string Pop(string newName)
    {
        FSState lastState = mStateStack.Peek();
        string newState = null;
        if (newName == null && mStateStack.Count > 1)
        {
            int index = 0;
            foreach (FSState item in mStateStack)
            {
                if (index++ == mStateStack.Count - 2)
                {
                    newState = item.StateName;
                }
            }
        }
        else
        {
            newState = newName;
        }
        string lastStateName = null;
        if (lastState != null)
        {
            lastStateName = lastState.StateName;
            lastState.StateObject.OnExit(newState);
        }
        mStateStack.Pop();
        return lastStateName;
    }

    /// <summary>
    /// ������ǰ״̬���¼�
    /// </summary>
    /// <param name="eventName"></param>
    public void Trigger(string eventName)
    {
        CurrentState.Trigger(eventName);
    }

    public void Trigger(string eventName, object param1)
    {
        CurrentState.Trigger(eventName, param1);
    }

    public void Trigger(string eventName, object param1, object param2)
    {
        CurrentState.Trigger(eventName, param1, param2);
    }

    public void Trigger(string eventName, object param1, object param2, object param3)
    {
        CurrentState.Trigger(eventName, param1, param2, param3);
    }
}