using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NY
{
    public interface IState
    {
        void OnEnter(string stateName);
        void OnUpdate(string stateName);
        void OnExit(StateType stateType);
    }

    public enum StateType
    {
        DEFAULT,
        POP
    }
}
