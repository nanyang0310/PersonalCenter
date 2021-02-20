/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public abstract partial class MonoBehaviourSimplify : MonoBehaviour
    {
        List<MsgRecord> mMsgRecorder = new List<MsgRecord>();

        protected void RegisterMsg(string msgName, Action<object> onMsgReceived)
        {
            MessageCenter.Register(msgName, onMsgReceived);

            mMsgRecorder.Add(MsgRecord.Allocate(msgName, onMsgReceived));
        }


        private void OnDestroy()
        {
            OnBeforeDestroy();

            foreach (var msgRecord in mMsgRecorder)
            {
                MessageCenter.UnRegister(msgRecord.Name, msgRecord.OnMsgReceived);
                msgRecord.Recycle();
            }

            mMsgRecorder.Clear();
        }

        protected abstract void OnBeforeDestroy();

        private class MsgRecord
        {
            private static readonly Stack<MsgRecord> mMsgRecordPool = new Stack<MsgRecord>();

            public static MsgRecord Allocate(string msgName, Action<object> onMsgReceived)
            {
                MsgRecord retMsgRecord = null;
                retMsgRecord = mMsgRecordPool.Count > 0 ? mMsgRecordPool.Pop() : new MsgRecord();
                retMsgRecord.Name = msgName;
                retMsgRecord.OnMsgReceived = onMsgReceived;

                return retMsgRecord;
            }

            public void Recycle()
            {
                Name = null;
                OnMsgReceived = null;

                mMsgRecordPool.Push(this);
            }

            public string Name;

            public Action<object> OnMsgReceived;
        }
    }

}

