using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using ZigZaggle.Core.Events;

namespace ZigZaggle.Core.FSM
{
    public class MBStateMachine : MonoBehaviour
    {
        private bool initialized;
      
        [Header("FSM")]
        public bool unityEventsFolded;
        public bool allowReentryStates = false;
        public GameObject currentState;
        public GameObject defaultState;
        public GameObjectEvent onStateEntered;
        public GameObjectEvent onStateExited;
        
        
        [Tooltip("Should log messages be thrown during usage?")]
        public bool verbose = true;

        protected void Awake()
        {
            Initialize();
        }
        
        protected void Start()
        {
            if (Application.isPlaying && defaultState != null) ChangeState(defaultState.name);
        }

        protected virtual void Initialize()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        
        
        public GameObject Next()
        {
            if (currentState == null) return ChangeState(0);
            var currentIndex = currentState.transform.GetSiblingIndex();
            return currentIndex == transform.childCount - 1 ? currentState : ChangeState(++currentIndex);
        }
        
        public GameObject Previous()
        {
            if (currentState == null) return ChangeState(0);
            var currentIndex = currentState.transform.GetSiblingIndex();
            return currentIndex == 0 ? currentState : ChangeState(--currentIndex);
        }
        
        public void Exit()
        {
            if (currentState == null) return;
            Log("(-) " + name + " EXITED state: " + currentState.name);

            onStateExited?.Invoke(currentState);
            currentState.SetActive(false);
            currentState = null;
        }
        
        public GameObject ChangeState(int childIndex)
        {
            return childIndex <= transform.childCount - 1 ? ChangeState(transform.GetChild(childIndex).gameObject) : null;
        }
        
        public GameObject ChangeState(GameObject state)
        {
            if (currentState != null)
            {
                if (!allowReentryStates && state == currentState)
                {
                    return null;
                }
            }

            if (state.transform.parent != transform)
            {
                return null;
            }

            Exit();
            Enter(state);

            return currentState;
        }
        
        public GameObject ChangeState(string state)
        {
            var found = transform.Find(state);
            return !found ? null : ChangeState(found.gameObject);
        }

        private void Enter(GameObject state)
        {
            currentState = state;
            var index = currentState.transform.GetSiblingIndex();

            Log("(+) " + name + " ENTERED state: " + state.name);
            onStateEntered?.Invoke(currentState);
            currentState.SetActive(true);
        }

        private void Log(string message)
        {
            if (!verbose) return;
            Debug.Log(message, gameObject);
        }
    }
}