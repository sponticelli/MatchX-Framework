using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using ZigZaggle.Core.FSM;

namespace ZigZaggle.Collapse.Components
{
    public class GridInputState : MBState
    {
        [Header("Camera")] 
        [SerializeField] private Camera mainCamera;
        
        private GridController controller;

        private void OnEnable()
        {
            controller = (GridController) StateMachine;
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }
        }

        private void Update()
        {
            CheckInput();
        }

        

        private void OnDisable()
        {
            
        }
        
        private void CheckInput()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null || !hit.collider.gameObject.CompareTag("Cell"))
            {
                return;
            }

            var cell = hit.collider.gameObject.GetComponent<Cell>();
            if (!cell)
            {
                return;
            }

            controller.selectedCell = cell;

            ToExecuteState();
        }

        private void ToExecuteState()
        {
            StateMachine.ChangeState("ExecuteMoveState");
        }
    }
}