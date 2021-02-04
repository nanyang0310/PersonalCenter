// http://jxwgame.blog.51cto.com/943299/1608980
// http://jxwgame.blog.51cto.com/943299/1610360

using System;
using System.Collections.Generic;

/// <summary>
/// 状态接口
/// </summary>
public interface IState
{
    void OnEnter(string prevState);

    void OnExit(string nextState);

    void OnUpdate();
}

/// <summary>
/// 状态类，提供状态的基本操作以及事件触发。
/// </summary>
public class FSState
{
    protected FiniteStateMachine.EnterState mEnterDelegate;
    protected FiniteStateMachine.PushState mPushDelegate;
    protected FiniteStateMachine.PopState mPopDelegate;

    protected IState mStateObject;
    protected string mStateName;
    protected FiniteStateMachine mOwner;
    protected Dictionary<string, FSEvent> mTranslationEvents;

    public FSState(IState obj, FiniteStateMachine owner, string name,
        FiniteStateMachine.EnterState e, FiniteStateMachine.PushState pu, FiniteStateMachine.PopState po)
    {
        mStateObject = obj;
        mStateName = name;
        mOwner = owner;
        mEnterDelegate = e;
        mPushDelegate = pu;
        mPopDelegate = po;
        mTranslationEvents = new Dictionary<string, FSEvent>();
    }

    public IState StateObject
    {
        get
        {
            return mStateObject;
        }
    }

    public string StateName
    {
        get
        {
            return mStateName;
        }
    }

    /// <summary>
    /// 事件注册函数，将事件的名字和事件加入到Dictionary里面
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public FSEvent On(string eventName)
    {
        if (mTranslationEvents.ContainsKey(eventName))
        {
            return mTranslationEvents[eventName];
        }

        FSEvent newEvent = new FSEvent(eventName, null, this, mOwner, mEnterDelegate, mPushDelegate, mPopDelegate);
        mTranslationEvents.Add(eventName, newEvent);
        return newEvent;
    }

    /// <summary>
    /// 注册关联与状态相关的事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public FSState On<T>(string eventName, Func<T, bool> action)
    {
        FSEvent newEvent = new FSEvent(eventName, null, this, mOwner, mEnterDelegate, mPushDelegate, mPopDelegate);
        newEvent.mAction = delegate(object o1, object o2, object o3)
        {
            T param1;
            try { param1 = (T)o1; }
            catch { param1 = default(T); }
            action(param1);
            return true;
        };
        mTranslationEvents.Add(eventName, newEvent);
        return this;
    }

    public FSState On<T>(string eventName, Action<T> action)
    {
        FSEvent newEvent = new FSEvent(eventName, null, this, mOwner, mEnterDelegate, mPushDelegate, mPopDelegate);
        newEvent.mAction = delegate(object o1, object o2, object o3)
        {
            T param1;
            try { param1 = (T)o1; }
            catch { param1 = default(T); }
            action(param1);
            return true;
        };
        mTranslationEvents.Add(eventName, newEvent);
        return this;
    }

    public FSState On<T1, T2>(string eventName, Func<T1, T2, bool> action)
    {
        FSEvent newEvent = new FSEvent(eventName, null, this, mOwner, mEnterDelegate, mPushDelegate, mPopDelegate);
        newEvent.mAction = delegate(object o1, object o2, object o3)
        {
            T1 param1;
            T2 param2;
            try { param1 = (T1)o1; }
            catch { param1 = default(T1); }
            try { param2 = (T2)o2; }
            catch { param2 = default(T2); }
            action(param1, param2);
            return true;
        };
        mTranslationEvents.Add(eventName, newEvent);
        return this;
    }

    public FSState On<T1, T2>(string eventName, Action<T1, T2> action)
    {
        FSEvent newEvent = new FSEvent(eventName, null, this, mOwner, mEnterDelegate, mPushDelegate, mPopDelegate);
        newEvent.mAction = delegate(object o1, object o2, object o3)
        {
            T1 param1;
            T2 param2;
            try { param1 = (T1)o1; }
            catch { param1 = default(T1); }
            try { param2 = (T2)o2; }
            catch { param2 = default(T2); }
            action(param1, param2);
            return true;
        };
        mTranslationEvents.Add(eventName, newEvent);
        return this;
    }

    public FSState On<T1, T2, T3>(string eventName, Func<T1, T2, T3, bool> action)
    {
        FSEvent newEvent = new FSEvent(eventName, null, this, mOwner, mEnterDelegate, mPushDelegate, mPopDelegate);
        newEvent.mAction = delegate(object o1, object o2, object o3)
        {
            T1 param1;
            T2 param2;
            T3 param3;
            try { param1 = (T1)o1; }
            catch { param1 = default(T1); }
            try { param2 = (T2)o2; }
            catch { param2 = default(T2); }
            try { param3 = (T3)o3; }
            catch { param3 = default(T3); }
            action(param1, param2, param3);
            return true;
        };
        mTranslationEvents.Add(eventName, newEvent);
        return this;
    }

    public FSState On<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
        FSEvent newEvent = new FSEvent(eventName, null, this, mOwner, mEnterDelegate, mPushDelegate, mPopDelegate);
        newEvent.mAction = delegate(object o1, object o2, object o3)
        {
            T1 param1;
            T2 param2;
            T3 param3;
            try { param1 = (T1)o1; }
            catch { param1 = default(T1); }
            try { param2 = (T2)o2; }
            catch { param2 = default(T2); }
            try { param3 = (T3)o3; }
            catch { param3 = default(T3); }
            action(param1, param2, param3);
            return true;
        };
        mTranslationEvents.Add(eventName, newEvent);
        return this;
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="name"></param>
    public void Trigger(string name)
    {
        mTranslationEvents[name].Execute(null, null, null);
    }

    public void Trigger(string eventName, object param1)
    {
        mTranslationEvents[eventName].Execute(param1, null, null);
    }

    public void Trigger(string eventName, object param1, object param2)
    {
        mTranslationEvents[eventName].Execute(param1, param2, null);
    }

    public void Trigger(string eventName, object param1, object param2, object param3)
    {
        mTranslationEvents[eventName].Execute(param1, param2, param3);
    }
}