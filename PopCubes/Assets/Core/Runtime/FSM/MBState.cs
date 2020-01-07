using UnityEngine;
using System.Collections;

namespace ZigZaggle.Core.FSM
{
    public class MBState : MonoBehaviour
    {
        private MBStateMachine stateMachine;
        public bool IsFirst => transform.GetSiblingIndex() == 0;
        public bool IsLast => transform.GetSiblingIndex() == transform.parent.childCount - 1;

        public MBStateMachine StateMachine
        {
            get
            {
                if (stateMachine != null) return stateMachine;
                stateMachine = transform.parent.GetComponent<MBStateMachine>();
                return stateMachine != null ? stateMachine : null;
            }
        }

        public void ChangeState(int childIndex)
        {
            StateMachine.ChangeState(childIndex);
        }

        public void ChangeState(GameObject state)
        {
            StateMachine.ChangeState(state.name);
        }

        public void ChangeState(string state)
        {
            if (StateMachine == null) return;
            StateMachine.ChangeState(state);
        }

        public GameObject Next()
        {
            return StateMachine.Next();
        }

        public GameObject Previous()
        {
            return StateMachine.Previous();
        }

        public void Exit()
        {
            StateMachine.Exit();
        }
    }
}